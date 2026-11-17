# [Feature & Polish] Color pickers for custom theme, device management & animation tuning

- **Issue Type**: Feature Request & UI/UX Polish
- **Target Components**: `BetterIsland / SettingsWindow.cs`, `DevicesView.cs`, `Animations / IslandMotion`
- **Labels**: `enhancement`, `ui/ux`, `settings`, `customization`, `polish`
- **Status**: 📋 Planned

---

## 📌 Executive Summary

To make BetterIsland feel completely native to every user's personal desktop aesthetic and hardware setup, this issue tracks three interrelated quality-of-life enhancements:
1. **Interactive Custom Color Pickers**: Full-spectrum HEX/HSV picker for island surface background and accent colors with OLED black presets and transparency controls.
2. **Device Battery Management**: Capability to reorder, hide, rename, and set custom battery alert thresholds for connected wireless peripherals.
3. **Spring Physics & Refresh Rate (FPS) Tuning**: Fine-grained sliders for animation duration, spring bounce/damping, and high-refresh-rate display synchronization (60Hz / 120Hz / 144Hz+).

---

## 🎨 Feature 1: Advanced Color Pickers & Theme Customization

### Current State (v0.2.0)
BetterIsland v0.2.0 added basic surface and accent color swatches in Settings (`Settings > Basics`), allowing users to click a swatch and set a custom hex value such as `#000000` (OLED black).

### Proposed Enhancements
- **Full Visual Color Picker Flyout**:
  - Interactive HSV color gradient canvas with hue strip and alpha/transparency slider.
  - Direct HEX text field with instant validation (e.g., `#000000`, `#7C5CFC`, `#0A84FF`).
  - RGB & HSL numerical input boxes.
- **Curated Palette Presets**:
  - ⬛ **OLED Pure Black**: `#000000` (Zero power consumption on OLED panels).
  - 🌌 **Deep Obsidian**: `#0B0B10` (BetterIsland default signature dark).
  - 🌙 **Midnight Slate**: `#12111A` (Subtle purple-gray tint).
  - 🪟 **Windows 11 Acrylic**: `#202020` with custom opacity.
  - ⚡ **Cyberpunk Neon**: `#7C5CFC` accent with high-contrast borders.
- **Material Effects**:
  - Background opacity slider (from $50\%$ translucent glassmorphic blur to $100\%$ solid opaque).
  - Border stroke thickness and outline color customization.
- **Theme Preset Export/Import**:
  - Ability to save the configured color scheme to a `.json` file in `%APPDATA%\BetterIsland\Themes` or share with the community.

```
+------------------------------------------------------+
| [Palette]  [HSV Square]                              |
|            Hue: [====|==============]                |
|            Alpha: [=========|=======]  85%           |
| HEX: [#000000]   RGB: (0, 0, 0)                      |
| Quick Presets: [OLED Black] [Deep Slate] [Indigo]    |
+------------------------------------------------------+
```

---

## 🖱️ Feature 2: Peripheral Device Management & Reordering

### Current State
`DevicesView.cs` automatically detects and lists wireless mice, keyboards, headsets, controllers, and phones (including the newly added ARDOR GAMING Essence). However, devices are listed in the order discovered by the Windows HID subsystem, and inactive or duplicate devices cannot be hidden.

### Proposed Enhancements
1. **Hide / Unhide Peripherals**:
   - Add a visibility toggle next to each detected device in `Settings > Device batteries`.
   - Hidden devices will not occupy space in the collapsed island pill or expanded panel.
2. **Reordering via Drag-and-Drop or Up/Down Buttons**:
   - Allow users to prioritize which device shows up first when multiple battery indicators are present.
   - Example: Keep the primary gaming mouse first, followed by keyboard, headset, and phone.
3. **Custom Aliases / Renaming**:
   - Allow users to rename cryptic hardware strings (e.g., `2.4G Wireless Receiver` $\to$ `Essence Mouse (Desk)`).
4. **Per-Device Battery Warning Thresholds**:
   - Configurable low-battery warning ($10\%$, $15\%$, $20\%$) and critical warning ($5\%$) thresholds with custom accent colors.

```
[Settings > Device batteries > Device Order & Visibility]
┌────────────────────────────────────────────────────────┐
│ [≡] [👁️] ARDOR GAMING Essence (Wireless)     [ 85% ]    │
│ [≡] [👁️] Custom 75% Mechanical Keyboard      [ 70% ]    │
│ [≡] [👁️] Wireless Gaming Headset             [ 40% ]    │
│ [≡] [🚫] Virtual Xbox Controller (Hidden)    [ --- ]    │
└────────────────────────────────────────────────────────┘
```

---

## ⚡ Feature 3: Spring Physics & Animation Refresh Rate Tuning

### Current State
Island expansion and collapse use fixed transition parameters:
- Duration: $\approx 300\text{ ms}$.
- Easing: `CubicEaseOut` / simulated damping.
- Framerate: Bound to default WPF render loop dispatch.

On high-refresh-rate gaming monitors (120Hz, 144Hz, 240Hz, 360Hz), users want the island animations to feel buttery-smooth, while laptop users prioritize battery efficiency.

### Proposed Enhancements
1. **Animation Dynamics Presets & Sliders**:
   - **Preset Modes**:
     - *Snappy (180 ms)*: Fast, minimal bounce, immediate response.
     - *Natural (300 ms)*: Balanced Apple-style fluid damping (current default).
     - *Playful / Bouncy (420 ms)*: Expressive spring physics with subtle overshoot.
   - **Custom Physics Sliders**:
     - Expansion Duration: $100\text{ ms} - 600\text{ ms}$.
     - Spring Stiffness ($k$) & Damping Ratio ($\zeta$).
2. **Display Refresh Rate Synchronization**:
   - Detect primary monitor refresh rate via DirectX / DXGI or `EnumDisplaySettings`.
   - Render animation frames aligned with VSync to eliminate micro-stuttering.
3. **Power-Saving / Battery Mode**:
   - Auto-detect when running on battery power (via Windows Power Status API).
   - Option to lock animation framerate to 60 FPS or disable backdrop blur when unplugged to extend laptop battery life.

---

## 🛠️ Implementation Checklist

- [ ] **Color Picker Component**:
  - [ ] Implement or integrate a lightweight, zero-dependency WPF HSV color picker flyout in `SettingsThemes.cs`.
  - [ ] Wire HEX input box with live validation and `#000000` quick button.
  - [ ] Persist settings in `DeckSettings.CustomSurfaceColor` and `DeckSettings.CustomAccentColor`.
- [ ] **Device Management**:
  - [ ] Extend `DeviceEntry` model with `IsUserHidden`, `CustomDisplayName`, and `DisplayPriority`.
  - [ ] Add reorderable list view in `SettingsWindow.cs` under the Device Battery tab.
  - [ ] Update `DevicesView.cs` to filter and sort devices according to user preferences.
- [ ] **Animation Settings**:
  - [ ] Add `AnimationDurationMs` and `SpringDampingRatio` properties to `DeckSettings`.
  - [ ] Expose sliders in `Settings > Basics > Interactions & Animations`.
  - [ ] Add battery-saver throttle toggle (`ReduceAnimationsOnBattery`).

---

## 🤝 Feedback & Discussion

What color themes or device customization features would you find most useful? Share your thoughts below!
