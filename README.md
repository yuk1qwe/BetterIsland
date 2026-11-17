<div align="center">

<img src="docs/icon.png" width="96" alt="BetterIsland icon">

# BetterIsland — Dynamic Island for Windows

**BetterIsland** is a fluid, modern Dynamic Island for Windows 10/11. It brings together your media player (including Yandex Music), wireless device batteries (with ARDOR GAMING Essence support), real-time system monitoring, AI coding quotas, clipboard history, LAN sync, and a file shelf into an elegant, glassmorphic morphing pill.

**BetterIsland** — плавная, современная «Динамическая плашка» (Dynamic Island) для Windows 10/11. Объединяет музыкальный плеер (с поддержкой Яндекс Музыки), батареи беспроводных устройств (включая мышь ARDOR GAMING Essence), мониторинг системы, AI-квоты, историю буфера обмена и файловую полку в единой жидкой пилюле с эффектом glassmorphism.

[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-10.0-purple.svg)](https://dotnet.microsoft.com/)
[![Platform](https://img.shields.io/badge/Platform-Windows%2010%20%7C%2011-0078D6.svg)](https://microsoft.com/windows)
[![Languages](https://img.shields.io/badge/Languages-EN%20%7C%20RU%20%7C%20ZH-indigo.svg)](#)

<img src="docs/en-US/images/hero.png" width="100%" alt="BetterIsland interface preview">

</div>

---

## English

### ✨ Features
- **Fluid Morphing Island**: Smooth expanding pill on hover (`CubicEaseOut`), spring expansion animations (~300ms), and seamless tab transitions.
- **Glassmorphism & Theming**: Modern dark theme with `#0B0B10`–`#12111A` palette, indigo/purple accents (`#7C5CFC`), and light/dark/system theme switching without restarts.
- **Media & Music Player**: Full Windows SMTC integration with priority support for **Yandex Music (Яндекс Музыка)**, Spotify, Apple Music, and more. Features animated rhythmic equalizer bars (5–7 bars), cover art, and inline playback controls.
- **Wireless Device Batteries**: Real-time battery and charging status for wireless mice, keyboards, headsets, and controllers.
  - Native support for **ARDOR GAMING Essence** (ARDW-ES3395-BW, VID 25A7 PID FA7C / FA7B, Areson / Compx JM03 controller) with graceful sleep timeout handling.
  - Support for Logitech, Pulsar, VGN, ATK, Razer, SteelSeries, Xbox, PlayStation, and many more.
- **System Monitoring**: Real-time CPU, GPU, RAM, disk, and network speed tracking directly in the pill or expanded panel.
- **AI Coding Tool Quotas**: Live tracking for Claude Code, Codex, Cursor, Antigravity, Kimi, DeepSeek, and other AI assistants.
- **Clipboard & File Shelf**: Clipboard history, LAN cross-device synchronization, and drag-and-drop file shelf.
- **Modular Architecture**: Toggle any module on or off in Settings (Music, Batteries, Monitor, AI, Clipboard, Sync, Shelf, Sensors, Weather).
- **Internationalization (i18n)**: Fully translated into **Russian** (Русский), **English**, and Chinese.

### 🚀 Building from Source
Prerequisites: Windows 10/11, .NET 10 SDK.

```bash
git clone https://github.com/yuk1qwe/BetterIsland.git
cd BetterIsland
dotnet build BetterIsland.sln -c Release
```

---

## Русский

### ✨ Возможности
- **Жидкая морфинг-пилюля**: Плавное расширение при наведении курсора (`CubicEaseOut`), пружинная анимация раскрытия (~300 мс) и переходы между вкладками с эффектом fade + slide.
- **Стеклянный дизайн (Glassmorphism)**: Глубокая тёмная тема (`#0B0B10`–`#12111A`), акценты цвета индиго/фиолетовый (`#7C5CFC`), переключение тем (Светлая / Тёмная / Системная) на лету без перезапуска приложения.
- **Музыкальный плеер и Яндекс Музыка**: Интеграция с Windows SMTC с приоритетом сессий **Яндекс Музыки** (`yandexmusic`, `yandex-music`), Spotify, Apple Music и др. Анимированные эквалайзер-полосы (5–7 полос), отображение обложки, названия и управление воспроизведением.
- **Батарея устройств**: Отображение уровня заряда и статуса зарядки для беспроводных мышей, клавиатур, гарнитур и геймпадов.
  - Встроенная поддержка мыши **ARDOR GAMING Essence** (ARDW-ES3395-BW, VID 25A7 PID FA7C / FA7B, чип Areson / Compx JM03) с корректной обработкой спящего режима.
  - Поддержка Logitech, Pulsar, VGN, ATK, Razer, SteelSeries, Xbox, DualSense и др.
- **Мониторинг системы**: Загрузка процессора, видеокарты, оперативной памяти, дисков и сетевой трафик.
- **AI-квоты**: Отслеживание остатка лимитов для Claude Code, Codex, Cursor, Antigravity, DeepSeek и др.
- **Буфер обмена и файловая полка**: История скопированного текста и картинок, синхронизация буфера по локальной сети, полка для временных файлов.
- **Система модулей**: Тогглы в настройках для включения/отключения любого модуля без удаления функционала.
- **Мультиязычность**: Полный перевод интерфейса на **русский**, **английский** и китайский языки.

### 🚀 Сборка проекта
Требования: Windows 10/11, .NET 10 SDK.

```bash
git clone https://github.com/yuk1qwe/BetterIsland.git
cd BetterIsland
dotnet build BetterIsland.sln -c Release
```

---

## 📄 License
BetterIsland is licensed under the Apache License 2.0. See [LICENSE](LICENSE) for details.
