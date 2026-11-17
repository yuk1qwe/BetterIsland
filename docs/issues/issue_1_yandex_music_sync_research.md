# [Research] Direct synchronized karaoke lyrics extraction from Yandex Music desktop/web client

- **Issue Type**: Research & Architecture Exploration
- **Target Component**: `BetterIsland.Core / Lyrics.cs` & `LyricsService`
- **Labels**: `enhancement`, `research`, `yandex-music`, `lyrics-engine`
- **Status**: 🔬 Open / In Progress

---

## 🎯 Objective

Enable **millisecond-accurate synchronized karaoke lyrics** for **Yandex Music (Яндекс Музыка)** tracks in BetterIsland's dynamic island pill and expanded music panel, matching the fluid experience available for Apple Music, NetEase, and Spotify.

---

## 🔍 Context & Current Limitations

In **BetterIsland v0.2.0**, media playback tracking works seamlessly with Yandex Music via Windows System Media Transport Controls (SMTC) by detecting `yandexmusic.exe` and web browser sessions.

However, displaying synchronized lyrics currently relies on a multi-tier fallback architecture:
1. **Tier 1 (LRCLIB & NetEase)**: BetterIsland searches LRCLIB (`https://lrclib.net`) and NetEase Cloud Music (`https://music.163.com`). If an accurate match with time tags is found, synchronized lyrics display perfectly.
2. **Tier 2 (Official Yandex Supplement API)**: If Tier 1 has no time-tagged LRC, BetterIsland falls back to:
   ```http
   GET https://api.music.yandex.net/tracks/{trackId}/supplement
   ```
   This endpoint only returns static, unsynchronized text (`fullLyrics` or `lyrics`), with no timing markers.
3. **Tier 3 (Progress Fraction Interpolation)**: For plain text lyrics, line position is approximated via playback progress:
   $$\text{Index} = \text{clamp}\left(\left\lfloor \frac{\text{Position}}{\text{Duration}} \times N \right\rfloor, 0, N - 1\right)$$
   This heuristic drifts significantly on tracks with extended instrumental intros, guitar solos, or spoken bridges.

Meanwhile, both the **official Yandex Music Desktop client** (Electron) and the **modern web app** (`music.yandex.ru`) feature rich, line-by-line and syllable-by-syllable synchronized karaoke text for a vast catalog of Russian and international tracks.

We want to tap into this data stream cleanly and reliably.

---

## 🔬 Technical Background

### 1. Windows SMTC Limitations
Windows SMTC provides:
- Track Title, Artist, Album Title
- Album Art stream
- Playback Status (Playing, Paused, Stopped)
- Timeline position and total duration

Windows SMTC **does not** transmit lyric payloads or karaoke timestamps. Any synchronized lyrics must be obtained through an auxiliary API or IPC channel.

### 2. Yandex Music Architecture
- **Desktop Application**: An Electron wrapper executing React/TypeScript web bundles inside Chromium.
- **Web App**: Uses internal Redux/MobX stores and calls internal backend services.
- **Known APIs**:
  - Public mobile API: `https://api.music.yandex.net/`
  - Web client private endpoints: `https://music.yandex.ru/api/v2.1/...` or internal `/handlers/...`
  - Mobile sync lyrics service: Provides synchronized lyrics for Yandex Music on iOS/Android.

---

## 🛠️ Proposed Approaches for Investigation

```
+-------------------------------------------------------------+
|                Yandex Music Synced Lyrics                   |
+-------------------------------------------------------------+
                              |
      +-----------------------+-----------------------+
      |                       |                       |
      v                       v                       v
[Approach A: REST]      [Approach B: CDP/IPC]   [Approach C: LRCLIB Bridge]
Private Sync Endpoint    Electron DevTools /     Community Cache &
Mobile / Web v2.1 API    Local Web Extension     Upload Pipeline
```

### Approach A: Reverse Engineering Yandex Private Sync REST Endpoints
*Feasibility: High | Maintenance: Medium | User Friction: Zero*

Research whether Yandex exposes an endpoint that returns line-tagged lyrics in JSON or LRC format:
- **Investigation Points**:
  - Inspect network traffic when clicking the "Текст песни с синхронизацией" (Karaoke) button in the desktop/web client.
  - Test known mobile API endpoints:
    ```http
    GET https://api.music.yandex.net/tracks/{trackId}/lyrics
    GET https://api.music.yandex.net/tracks/{trackId}/lyrics-sync
    Headers:
      User-Agent: YandexMusic/2024.x (Android / iOS)
      Authorization: OAuth <token> (or anonymous session)
    ```
  - Analyze the JSON payload structure. Typically, Yandex represents sync lyrics as:
    ```json
    {
      "result": {
        "lines": [
          {
            "startTimeMs": 12400,
            "endTimeMs": 15800,
            "text": "Строка песни...",
            "words": [
              { "startTimeMs": 12400, "endTimeMs": 13100, "text": "Строка" },
              { "startTimeMs": 13200, "endTimeMs": 15800, "text": "песни..." }
            ]
          }
        ]
      }
    }
    ```
  - **Key Question**: Does this endpoint require an active Yandex Plus subscription token, or does it accept public/unauthenticated requests (similar to `/supplement`)?

### Approach B: Electron Desktop Client IPC / CDP Hooking
*Feasibility: Medium | Maintenance: Low-Medium | User Friction: Low*

Because the official desktop app is built on Electron:
- **Chrome DevTools Protocol (CDP)**:
  - If the desktop app is launched with `--remote-debugging-port=xxxx`, BetterIsland can attach a lightweight WebSocket client to evaluate JavaScript in the renderer context:
    ```javascript
    // Extract current player state & active synced lyrics line
    window.externalAPI.getCurrentTrack();
    ```
- **Local Cache Inspection**:
  - Determine if the desktop app caches synchronized lyrics locally in `%APPDATA%\YandexMusic` or `%LOCALAPPDATA%\Programs\YandexMusic` (LevelDB, IndexedDB, or SQLite).
- **In-Memory Pattern Scanning**:
  - Scan the memory space of `yandexmusic.exe` for the active LRC buffer if other approaches prove too fragile.

### Approach C: Lightweight Web Companion / Browser Extension
*Feasibility: High | Maintenance: Low | User Friction: Moderate*

For users listening via browser (`music.yandex.ru`, Chrome, Edge, Yandex Browser):
- Provide an optional, open-source userscript or extension.
- The script hooks into the web player's event listeners:
  ```javascript
  externalAPI.on(externalAPI.EVENT_TRACK, () => { ... });
  ```
- Broadcasts the active line or full synchronized lyric payload to BetterIsland's local HTTP listener (`http://127.0.0.1:41337/api/lyrics`).

### Approach D: Crowd-Sourced LRCLIB Synchronization Bridge
*Feasibility: Very High | Maintenance: Low | User Friction: Zero*

- BetterIsland already features first-class LRCLIB integration.
- For tracks where Yandex does not provide open lyrics, create a streamlined flow allowing users or automated scripts to submit verified LRC files to [LRCLIB.net](https://lrclib.net/).
- Once submitted, LRCLIB immediately serves synchronized timestamps to all BetterIsland users globally.

---

## 🔒 Security, Privacy & TOS Guidelines

1. **No Password Harvesting**: BetterIsland must never prompt for, handle, or store user passwords. If user tokens are needed for Yandex Plus tracks, use official OAuth flows or protect cookies using Windows Data Protection API (DPAPI).
2. **Safe Rate Limiting**: All network requests must include exponential backoff and adhere to sensible cache headers (storing results in memory for up to 80 tracks as already implemented in `LyricsService.cs`).
3. **No Audio Scraping**: The research focuses strictly on textual lyrics and timing metadata. BetterIsland does not download, rip, or bypass audio stream protections.

---

## 📋 Research Action Items

- [ ] **Packet Inspection**: Capture HTTPS traffic of the official Yandex Music Desktop app (v5.x+) during karaoke playback using MITM tools (mitmproxy / Fiddler) with custom root certificates.
- [ ] **Endpoint Documentation**: Document the exact URL, query parameters, authorization headers, and response structure for karaoke lyrics.
- [ ] **Token Authentication Matrix**: Verify whether unauthenticated requests return sync timestamps or if a `Session_id` / OAuth token is required.
- [ ] **C# Converter**: Write a parser in `BetterIsland.Core` to convert Yandex millisecond JSON lines into `IReadOnlyList<LyricLine>`.
- [ ] **Benchmark**: Compare timing accuracy between LRCLIB, NetEase, and Yandex direct sync.

---

## 💬 Discussion & Contributions

If you have insights into the internal Yandex Music API, have captured sample network responses, or would like to test the prototype parser, please reply below or submit a PR against `BetterIsland.Core/Lyrics.cs`!
