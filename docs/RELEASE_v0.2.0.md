# BetterIsland v0.2.0 — Dynamic Island Windows Experience

[![Version](https://img.shields.io/badge/Release-v0.2.0-blue.svg)](https://github.com/yuk1qwe/BetterIsland/releases/tag/v0.2.0)
[![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)](https://dotnet.microsoft.com/)
[![Platform](https://img.shields.io/badge/Platform-Windows%2010%20%7C%2011-0078D6.svg)](https://microsoft.com/windows)
[![License](https://img.shields.io/badge/License-Apache%202.0-green.svg)](LICENSE)
[![Languages](https://img.shields.io/badge/Languages-EN%20%7C%20RU%20%7C%20ZH-indigo.svg)](#)

---

## 🌟 Overview

**BetterIsland v0.2.0** marks a major leap forward in bringing a fluid, responsive, and deeply integrated **Dynamic Island experience to Windows 10 and 11**. 

This release introduces comprehensive support for **Yandex Music (Яндекс Музыка)**, dedicated hardware battery telemetry for the **ARDOR GAMING Essence** wireless mouse, an intelligent multi-provider **synchronized karaoke lyrics engine** powered by LRCLIB and NetEase, pure **OLED black styling**, a full **Russian localization**, and crucial UI polish.

---

## 🚀 What's New in v0.2.0

### 🎵 1. Yandex Music (Яндекс Музыка) First-Class Integration
- **SMTC Session Priority**: The media daemon automatically prioritizes active sessions from `yandexmusic`, `yandex-music`, and the Yandex Music web client via Windows System Media Transport Controls.
- **Rhythmic Equalizer**: Smoothly animated equalizer bars (5–7 reactive bars) dynamically visualize playback state.
- **Artwork & Playback Navigation**: Full album art extraction, track seeking timeline, play/pause, next/previous controls, and liked track toggling directly from the island pill and expanded panel.

### 🎤 2. Multi-Provider Synchronized Lyrics (LRCLIB + NetEase + Yandex)
- **Cascading Fallback Engine**:
  1. **Tier 1 (Synchronized)**: Queries [LRCLIB](https://lrclib.net/) and NetEase Cloud Music (`music.163.com`) for millisecond-accurate time-tagged LRC lyrics with fuzzy title/artist matching and duration filtering.
  2. **Tier 2 (Official Metadata)**: Falls back to the Yandex Music API (`/tracks/{id}/supplement`) for official plain text lyrics.
  3. **Tier 3 (Interpolation)**: For unsynchronized lyrics, line position is intelligently estimated based on track playback progress (`Position / Duration`).
- **LyricHold & Jitter Resistance**: Micro-rewinds and clock jitter from SMTC reports are stabilized using `LyricHold`, avoiding annoying visual glitches.
- **Instrumental Detection**: Automatically detects instrumental tracks (`pureMusic` / credits-only) and displays a clean `♪` indicator.
- **Resilient cURL Transport**: Integrated fallback fetcher for LRCLIB requests to bypass strict TLS/HTTP handshakes.

### 🖱️ 3. ARDOR GAMING Essence Wireless Battery Telemetry
- **Hardware Protocol Support**: Direct USB HID report decoding for the popular **ARDOR GAMING Essence** wireless gaming mouse (model `ARDW-ES3395-BW`).
- **Dual-Mode Connectivity**:
  - `VID_25A7 PID_FA7C` — 2.4 GHz Wireless Receiver.
  - `VID_25A7 PID_FA7B` — USB-C Wired / Charging Mode.
- **Areson / Compx JM03 Controller Support**: Decodes battery percentage, active charging state, and implements graceful sleep timeout handling to prevent ghost disconnects when the mouse enters power-saving sleep mode.

### 🎨 4. Pure OLED Black Styling & Color Pickers
- **True OLED `#000000` Mode**: Configurable panel surface color allowing users with OLED and Mini-LED monitors to enjoy zero backlight bleed and infinite contrast.
- **Custom Accent & Surface Color Pickers**: Real-time color swatches in Settings (`Settings > Basics`) for island background, accent highlights, and text elements with instant live update without app restart.

### 🌐 5. Comprehensive Russian Localization (Русский язык)
- **Full UI Translation**: Every single screen, module, setting, tooltip, and hotkey dialog has been translated into natural Russian.
- **Instant Language Switching**: Toggle between Russian (Русский), English, and Chinese on the fly.

### 🛠️ 6. Polish & Bug Fixes
- **Wordmark Wrapping Fix**: Resolved an issue where the "BetterIsland" header in Settings could wrap onto multiple lines ("Better\nIsland") due to global text-wrapping styles. Wordmark is now strictly clamped (`TextWrapping.NoWrap`).
- **Memory Optimization**: Leveraged `ArrayPool<byte>` shared buffers for low-allocation string and payload processing.
- **Improved Settings Navigation**: Unified module toggle switches (Music, Device Battery, System Monitor, AI Quotas, File Shelf, Sensors Service, Weather).

---

## 📋 Full Changelog

| Component | Change Summary |
|---|---|
| **Media Player** | Added Yandex Music SMTC handler, equalizer bar animations, and media control buttons |
| **Lyrics Engine** | Added LRCLIB integration with fuzzy search + cURL fallback; NetEase 163 synchronized lyrics; Yandex supplement fallback; `LyricHold` smoothing |
| **Battery Monitor** | Added ARDOR GAMING Essence (ARDW-ES3395-BW, VID 25A7 PID FA7C/FA7B) with Areson/Compx JM03 HID decoding and sleep state handling |
| **Themes & UI** | Added pure OLED black `#000000` surface option, custom color picker swatches in Settings, and fixed wordmark line wrap |
| **Localization** | Added complete Russian language dictionary (`Loc.cs`) covering all settings, monitor cards, and device names |
| **Performance** | Optimized HTTP pooling, decreased idle CPU consumption, enhanced Windows 11 rounded window clipping |

---

## 💖 Credits & Acknowledgments

BetterIsland is built upon the wonderful foundation of **BrimDeck** created by **[@im307](https://github.com/im307)** ([im307/BrimDeck](https://github.com/im307/BrimDeck)).

We extend our sincere appreciation and gratitude to **@im307** and the BrimDeck community for pioneering this desktop Dynamic Island architecture for Windows and sharing their brilliant work with the open-source ecosystem under the Apache 2.0 License.

---

## 📥 Download & Installation

### Windows Installer (Recommended)
1. Download `BetterIsland-0.2.0-Setup.exe` from the Assets below.
2. Run the installer and follow the on-screen instructions.
3. Launch BetterIsland from the Start Menu or System Tray.

### Portable ZIP
1. Download `BetterIsland-0.2.0-win-x64.zip`.
2. Extract the archive into a folder of your choice (e.g., `C:\Tools\BetterIsland`).
3. Run `BetterIsland.exe`.

### System Requirements
- **OS**: Windows 10 (Build 19041+) or Windows 11 (all versions).
- **Runtime**: .NET 10 Desktop Runtime (x64) (bundled in installer).
- **Architecture**: x64 / ARM64 (emulated).
