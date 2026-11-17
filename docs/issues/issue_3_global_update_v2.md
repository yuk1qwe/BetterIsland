# [Roadmap] BetterIsland 2.0 Global Evolution

- **Issue Type**: Architecture RFC & Milestone Roadmap
- **Target Release**: BetterIsland v2.0
- **Labels**: `roadmap`, `v2.0`, `architecture`, `rfc`, `major-update`
- **Status**: 🗺️ Planning & Design Phase

---

## 🌌 Vision & Mission

**BetterIsland 2.0** represents the next generational evolution of the Windows Dynamic Island. 

While v0.x established a robust foundation for media playback (including Yandex Music), peripheral battery monitoring (ARDOR GAMING, Logitech, Razer), system telemetry, and AI quotas, **version 2.0 aims to transform BetterIsland into an extensible desktop platform** that feels like an intrinsic, native part of Windows 11.

```
       ┌─────────────────────────────────────────────────────────────┐
       │                     BetterIsland 2.0                        │
       └─────────────────────────────────────────────────────────────┘
                                      │
         ┌─────────────────┬──────────┴──────────┬─────────────────┐
         ▼                 ▼                     ▼                 ▼
   [Widget Store]   [Win11 Taskbar]     [Adaptive Notch]    [Multi-Monitor]
  Modular Plugins   Mica & Docking       Pill / Notch /      Cursor Following
  & Community Hub   Shell Integration    Corner HUD Modes    & Multi-Display
```

---

## 🏛️ The Four Pillars of BetterIsland 2.0

### 1. 🧩 Modular Widget Store & Extensible Plugin Architecture

Instead of bundling every feature directly into the core monolithic executable, BetterIsland 2.0 will introduce a **modular plugin architecture** with an integrated, user-friendly Widget Store.

- **Plugin Runtime & Sandboxing**:
  - Open SDK for .NET 10 / C# or lightweight WebAssembly (WASM) / WebView2 widgets.
  - Strict security manifest declaring requested permissions (Network, System Sensors, File System, Media Controls).
- **In-App Widget Gallery**:
  - One-click install, update, configure, and uninstall directly within `Settings > Widget Store`.
  - Curated official catalog alongside community repositories.
- **Example Ecosystem Widgets**:
  - ⏱️ *Productivity*: Pomodoro timer, To-Do list (Todoist, Microsoft To-Do), calendar agenda.
  - 💬 *Communication*: Discord voice channel activity, Telegram unread badges, Twitch stream live alerts.
  - 🏠 *Smart Home*: Home Assistant toggle switches and room temperature sensors.
  - 📈 *Developer & Finance*: GitHub commit activity / PR notifications, currency and crypto price trackers.

---

### 2. 🪟 Deep Windows 11 Taskbar Integration & Native Shell Aesthetics

BetterIsland 2.0 will embrace the Windows 11 design language and shell integration options:

- **Docking Flexibility**:
  - **Top Bezel Anchor**: Classic Dynamic Island centered at the top edge of the screen.
  - **Taskbar Docking**: Seamlessly attach the island directly above the centered Windows 11 taskbar icons, functioning as an auxiliary taskbar intelligence hub.
  - **Taskbar Replacement Mode**: Option to integrate smoothly alongside third-party taskbar utilities (such as StartAllBack, TranslucentTB, and Windhawk).
- **Modern Windows 11 Materials**:
  - Native **Mica**, **Mica Alt**, and **Acrylic** backdrop blur using Windows Desktop Window Manager (`DwmSetWindowAttribute`).
  - Fluid drop shadows matching Windows 11 system flyouts.
- **Smart Window & Gaming Awareness**:
  - **True Fullscreen Suppression**: Automatically hide the island when DirectX/Vulkan games enter exclusive fullscreen or borderless gaming mode.
  - **Focus Assist Integration**: Respect Windows Do Not Disturb and presentation mode states.

---

### 3. 📱 Dynamic Notch vs. Floating Pill Modes

Monitors come in many form factors—from laptop displays with hardware webcam notches to ultra-wide curved desktop monitors. BetterIsland 2.0 will feature **adaptive geometry modes**:

```
Mode A: Top Notch (Bezel Droop)     Mode B: Floating Pill (Detached)
┌───────────\_____/───────────┐    ┌─────────────────────────────┐
│                             │    │        ( ● BetterIsland ● ) │
│                             │    │                             │
│                             │    │                             │

Mode C: Taskbar Docked             Mode D: Mini Corner HUD
┌─────────────────────────────┐    ┌─────────────────────────────┐
│                             │    │                        [⚡75%]│
│                             │    │                             │
│       [ ♫ Yandex Music ]    │    │                             │
│ [ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ]   │    │                             │
└─────────────────────────────┘    └─────────────────────────────┘
```

1. **Bezel Notch Mode**:
   - The island attaches directly to the upper monitor bezel, morphing out of the physical frame like macOS / iOS.
   - Ideal for laptops with screen cutouts or users with top-mounted webcams.
2. **Floating Pill Mode**:
   - The signature BetterIsland floating capsule with configurable margin from screen edges, pill height, and corner radius.
3. **Corner HUD Bubble**:
   - Minimizes into a discrete micro-pill docked in the upper-right or lower-right screen corner, showing essential status (e.g. battery or current song) without obscuring the center.
4. **Contextual Fluid Transitions**:
   - Smoothly transforms its shape in response to events (e.g., expanding into a wide notification capsule on incoming media or low battery, and gracefully retracting to a minimal dot).

---

### 4. 🖥️ Comprehensive Multi-Monitor Support

Multi-monitor power users need seamless multi-display workflows:

- **Display Selection Modes**:
  - **Primary Display Only**: Stays locked to the designated main monitor.
  - **Follow Active Cursor / Focus**: Smoothly teleports or animates the island to the monitor where the user is currently working.
  - **Independent Multi-Display Islands**: Run independent instances on each connected monitor with separate widget layouts:
    - *Monitor 1 (Gaming/Work)*: System telemetry (CPU, GPU temps, RAM, network).
    - *Monitor 2 (Media/Chat)*: Yandex Music player, lyrics stream, and peripheral battery status.
- **Per-Monitor DPI & Resolution Scaling**:
  - Independent DPI scaling handling (e.g., $150\%$ 4K main display alongside a $100\%$ 1080p secondary monitor).
  - Robust display topology change handling (surviving monitor sleep, HDMI/DisplayPort disconnects, and hot-plugging without UI crash).

---

## 🏗️ Technical Architecture Overhaul

To support these ambitions, BetterIsland 2.0 will explore key structural architectural improvements:

| Layer | Current Architecture (v0.2.0) | v2.0 Evolution Target |
|---|---|---|
| **Process Model** | Single monolithic WPF application | Decoupled background service (`BetterIsland.Daemon`) + Lightweight UI overlay host (`BetterIsland.Shell`) |
| **Rendering Engine** | WPF (`PresentationFramework`) | WinUI 3 / Windows App SDK or DirectComposition with hardware acceleration |
| **Plugin API** | Hardcoded internal modules | Isolated plugin assembly loader (`AssemblyLoadContext`) with schema contracts |
| **Configuration** | Flat JSON settings | Typed configuration store with hot-reload and cloud/local backup profiles |

---

## 🗓️ Release Phases & Roadmap Milestones

### Phase 1: Foundation & Display Architecture (v2.0-alpha)
- [ ] Decouple core hardware/sensor services into a clean background service architecture.
- [ ] Implement multi-monitor management engine (cursor following + per-display instances).
- [ ] Introduce the Adaptive Notch vs. Floating Pill geometry engine.

### Phase 2: Shell Integration & Widget Engine (v2.0-beta)
- [ ] Implement Windows 11 Taskbar docking mode.
- [ ] Add native Mica / Acrylic DWM backdrop materials.
- [ ] Finalize the public Widget Plugin API and test harness.
- [ ] Build developer documentation and boilerplate widget templates.

### Phase 3: General Availability (v2.0-GA)
- [ ] Launch the built-in Widget Gallery with initial launch widgets.
- [ ] Comprehensive community testing and bug bash.
- [ ] Public v2.0 launch with updated documentation in English, Russian, and Chinese.

---

## 💬 Community Brainstorming

BetterIsland is driven by its community! 
- What widgets would you build or like to see in the Widget Store?
- Which docking mode (top notch vs. taskbar pill) fits your daily desk workflow best?

Drop your comments, suggestions, and feature wishes below! 🚀
