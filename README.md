<div align="center">

<img src="docs/icon.png" width="96" height="96" alt="BetterIsland Logo">

# BetterIsland — Dynamic Island for Windows 10 & 11

### *A fluid, modernized desktop Dynamic Island for Windows with rich media, synchronized lyrics, battery telemetry, and deep OLED aesthetics.*

[![Fork](https://img.shields.io/badge/Fork%20of-BrimDeck%20by%20@im307-f38ba8?logo=github&style=flat-square)](https://github.com/im307/BrimDeck)
[![Release](https://img.shields.io/badge/Version-v0.2.0-89b4fa?style=flat-square)](https://github.com/yuk1qwe/BetterIsland/releases)
[![.NET](https://img.shields.io/badge/.NET-10.0-cba6f7?logo=dotnet&style=flat-square)](https://dotnet.microsoft.com/)
[![Platform](https://img.shields.io/badge/Platform-Windows%2010%20%7C%2011-0078D6?logo=windows&style=flat-square)](https://microsoft.com/windows)
[![License](https://img.shields.io/badge/License-Apache%202.0-a6e3a1?style=flat-square)](LICENSE)
[![Languages](https://img.shields.io/badge/Languages-EN%20%7C%20RU%20%7C%20ZH-fab387?style=flat-square)](#)

[**English**](#-english) • [**Русский**](#-русский) • [**Architecture / Архитектура**](#-architecture--архитектура) • [**Build & Publish / Сборка**](#-building--publishing--сборка-и-публикация) • [**Credits / Благодарности**](#-credits--благодарности)

<br>

<img src="docs/en-US/images/hero.png" width="100%" alt="BetterIsland Interface Preview">

</div>

---

> [!NOTE]
> **Community Fork Notice / Уведомление о форке**
>
> **EN:** **BetterIsland** is a modernized, actively maintained community fork of **[BrimDeck](https://github.com/im307/BrimDeck)** (originally crafted by **[@im307](https://github.com/im307)**). BetterIsland elevates the experience for modern Windows 10 and 11 environments (.NET 10), adding deep OLED pure black styling with glass highlight rims inspired by [islandapp.pro](https://islandapp.pro), native Yandex Music playback and multi-tier synchronized lyrics (LRCLIB + NetEase + Yandex API), battery telemetry for the ARDOR GAMING Essence wireless mouse, complete Russian localization, and responsive 60 FPS spring physics.
>
> **RU:** **BetterIsland** — это активно развиваемый и модернизированный форк проекта **[BrimDeck](https://github.com/im307/BrimDeck)** от **[@im307](https://github.com/im307)**. Мы оптимизировали приложение для Windows 10/11 и платформы .NET 10, внедрили глубокую OLED-чёрную тему со стеклянным ободком в духе [islandapp.pro](https://islandapp.pro), поддержку Яндекс Музыки с синхронизированным караоке-текстом (LRCLIB + NetEase + Яндекс Музыка), телеметрию заряда беспроводной мыши ARDOR GAMING Essence, 100% русификацию и плавные пружинные анимации 60 FPS.

---

## 🇬🇧 English

### 🌟 Key Highlights & What's New

#### 1. 🌑 True OLED Black & Glass Highlight Rim (*islandapp.pro inspired*)
- **Pure `#000000` Surface Palette**: Engineered specifically for OLED and Mini-LED panels with zero backlight bleed, infinite contrast, and maximum battery efficiency on laptops.
- **Glass Highlight Rim**: Elegant `0x22` (`#22FFFFFF`) translucent inner highlight border with layered glassmorphism depth (`IslandBorder.cs`), recreating the premium aesthetic of [islandapp.pro](https://islandapp.pro) and Apple Dynamic Island.
- **Custom Color Swatches**: Real-time color pickers in Settings (`Settings > Basics`) allowing you to personalize the island surface and accent colors on the fly without restarting the application.
- **Flicker-Free Shadow Caching**: Smooth shadow invalidation and settling timer in `MainWindow.Shadow.cs` to eliminate visual artifacts during rapid morphing.

#### 2. 🎵 Yandex Music Integration & Multi-Tier Synced Lyrics
- **First-Class SMTC Support**: Dedicated Windows System Media Transport Controls listener prioritizing active sessions from **Yandex Music (Яндекс Музыка)** desktop, PWA, and browser tabs alongside Spotify and Apple Music.
- **Cascading Synchronized Lyrics Engine**:
  1. **Tier 1 (LRCLIB)**: Millisecond-accurate timestamped `.lrc` lyrics from [LRCLIB](https://lrclib.net/) with intelligent fuzzy title/artist sanitization and cURL fallback.
  2. **Tier 2 (NetEase Cloud Music)**: Timestamped lyrics and automated pure music / instrumental detection.
  3. **Tier 3 (Yandex Music Supplement)**: Direct API fallback to `api.music.yandex.net/tracks/{id}/supplement` for official lyrics.
  4. **Tier 4 (Fallback Interpolation)**: Smooth line estimation for unsynchronized text based on current track progress.
- **Player Features**: Real-time 5–7 bar audio-reactive equalizer animation, album art fetching, scrubbable progress bar, media controls, and `LyricHold` clock jitter stabilization.

#### 3. 🖱️ ARDOR GAMING Essence Wireless Mouse Support
- **Hardware Telemetry**: Native HID decoding for the popular **ARDOR GAMING Essence** wireless mouse (model `ARDW-ES3395-BW`, Areson / Compx JM03 MCU).
- **Dual Connection Modes**:
  - `VID_25A7 PID_FA7C`: 2.4 GHz wireless USB dongle.
  - `VID_25A7 PID_FA7B`: USB-C wired cable / charging mode.
- **Smart Sleep Mode**: 17-byte HID query frames (`0x08 0x04 ... checksum 0x55`) with graceful sleep timeout handling. Displays an idiomatic **"Asleep"** indicator instead of falsely reporting 0% or dropping the device.
- **Comprehensive Ecosystem**: Also supports hundreds of mice, keyboards, headsets, and controllers from Logitech, Razer, Pulsar, VGN, ATK, SteelSeries, Xbox, and PlayStation.

#### 4. 🌐 Full Russian & English Localization (i18n)
- **100% Native Translations**: Complete localization of all UI views, monitor cards, hotkeys, status pills, and every setting in the options window via `Loc.cs`.
- **Idiomatic Terminology**: Carefully tuned translations for concepts like Notch (Вырез), Capsule (Пилюля), Indicator (Индикатор), AI Usage (AI-квоты), Device Battery (Батарея устройств), and Sleep (Спит).
- **Instant Language Switching**: Seamlessly change between English, Russian, and Chinese on the fly.

#### 5. 🪄 UI Polish & Window Behavior
- **Wordmark Wrapping Clamped**: Fixed brand title line-wrapping glitches ("Better\nIsland") in the sidebar by enforcing `TextWrapping.NoWrap` and widening navigation columns.
- **Responsive 60 FPS Spring Animations**: Retuned spring physics (`CubicEaseOut`) with ~220ms duration (down from 360ms) for snappy, instantaneous tactile feedback.
- **Always-On-Top & Pinning Options**:
  - `AlwaysKeepIsland`: Prevents the island from collapsing into an indicator line when other windows maximize (`Maximized = WindowBehavior.Normal`).
  - Taskbar mirror band rendering ensures the island stays visible above topmost fullscreen windows without constant z-order flickering.
  - Configurable expand triggers: Hover, Click, or Middle/Pointer positioning.

#### 6. 📊 System Monitor, AI Quotas & File Shelf
- **Hardware Monitoring**: Live CPU, GPU, RAM, disk I/O, and network speeds directly in the island or expanded flyout.
- **AI Coding Assistant Quotas**: Real-time remaining token & request tracking for Claude Code, Codex, Cursor, Antigravity, Kimi, and DeepSeek.
- **Productivity Tools**: Multi-item clipboard history, secure LAN clipboard synchronization, and temporary drag-and-drop file shelf.

---

## 🇷🇺 Русский

### 🌟 Основные возможности и нововведения

#### 1. 🌑 Настоящий OLED-чёрный и стеклянная окантовка (*в стиле islandapp.pro*)
- **Чистый глубокий цвет `#000000`**: Разработан специально для OLED и Mini-LED дисплеев — без засветов, с бесконечным контрастом и максимальной энергоэффективностью на ноутбуках.
- **Стеклянная окантовка (Glass Rim)**: Элегантный полупрозрачный внутренний блик `0x22` (`#22FFFFFF`) с объемным эффектом glassmorphism в `IslandBorder.cs`, вдохновленный [islandapp.pro](https://islandapp.pro) и эстетикой Apple Dynamic Island.
- **Кастомная палитра цветов**: Интерактивные селекторы цветов в настройках (`Настройки > Основные`) для тонкой настройки цвета подложки и акцентов на лету без перезапуска приложения.
- **Динамические тени без мерцания**: Оптимизированный таймер затухания и кэширование теней в `MainWindow.Shadow.cs` устраняют любые артефакты при быстром раскрытии.

#### 2. 🎵 Яндекс Музыка и многоуровневые синхронизированные субтитры
- **Приоритет сессий Яндекс Музыки**: Выделенный обработчик Windows SMTC автоматически определяет и приоритизирует воспроизведение из приложения **Яндекс Музыка**, PWA или браузера.
- **Каскадный движок синхронизированных субтитров (караоке)**:
  1. **Уровень 1 (LRCLIB)**: Построчный поиск текста с таймингами с точностью до миллисекунд через [LRCLIB](https://lrclib.net/), умная очистка названий и cURL-шлюз.
  2. **Уровень 2 (NetEase Cloud Music)**: Поиск в базе NetEase и автоматическое определение инструментальных треков («♪»).
  3. **Уровень 3 (API Яндекс Музыки)**: Прямой запрос к `api.music.yandex.net/tracks/{id}/supplement` для получения официального текста песни.
  4. **Уровень 4 (Интерполяция)**: Плавное позиционирование несинхронизированного текста по времени трека.
- **Функции плеера**: Анимированный 5–7 полосный звуковой эквалайзер, загрузка обложек, интерактивный таймлайн с перемоткой, кнопки управления треками и сглаживание джиттера `LyricHold`.

#### 3. 🖱️ Поддержка беспроводной мыши ARDOR GAMING Essence
- **Прямая телеметрия по HID**: Нативное декодирование отчетов для популярной мыши **ARDOR GAMING Essence** (модель `ARDW-ES3395-BW`, чип Areson / Compx JM03).
- **Два режима работы**:
  - `VID_25A7 PID_FA7C`: Беспроводной 2.4 ГГц USB-ресивер.
  - `VID_25A7 PID_FA7B`: Проводное подключение по USB-C и режим зарядки.
- **Умный спящий режим**: 17-байтные HID-фреймы (`0x08 0x04 ... контрольная сумма 0x55`). Корректная обработка тайм-аутов со статусом **«Спит»** вместо ложного сброса на 0% или потери устройства.
- **Широкая поддержка брендов**: Также поддерживаются сотни мышей, клавиатур и гарнитур Logitech, Razer, Pulsar, VGN, ATK, SteelSeries, Xbox, DualSense и др.

#### 4. 🌐 Полная русская и английская локализация
- **100% перевод интерфейса**: Все экраны, карточки мониторинга, горячие клавиши и все страницы окна настроек переведены на естественный русский язык в `Loc.cs`.
- **Грамотная терминология**: Адаптированные термины: Вырез (Notch), Пилюля (Capsule), Индикатор, AI-квоты, Батарея устройств, Спит и др.
- **Мгновенное переключение языка**: Смена языка (Русский / English / Chinese) прямо из настроек на лету.

#### 5. 🪄 Полировка интерфейса и поведение окна
- **Фикс переноса названия**: Название «BetterIsland» в заголовке настроек больше не переносится на две строки благодаря `TextWrapping.NoWrap` и расширенной боковой панели.
- **Пружинные анимации 60 FPS**: Настроенная физика `CubicEaseOut` и сокращенная длительность (~220 мс) для мгновенного и плавного отклика.
- **Поведение окна поверх всех (Always-on-Top)**:
  - Режим `AlwaysKeepIsland`: Остров не превращается в тонкую линию при разворачивании других окон на весь экран (`Maximized = WindowBehavior.Normal`).
  - Зеркалирование в верхней полосе задач для отображения поверх других окон «поверх всех».
  - Настройка триггеров раскрытия: наведение курсора, клик, центрирование или следование за мышью.

#### 6. 📊 Системный монитор, AI-квоты и файловая полка
- **Мониторинг железа**: Загрузка процессора, видеокарты, памяти, дисков и сети прямо на плашке или в развернутом окне.
- **Квоты AI-ассистентов**: Отслеживание расхода токенов и запросов в реальном времени для Claude Code, Codex, Cursor, Antigravity, Kimi и DeepSeek.
- **Удобные утилиты**: История буфера обмена, синхронизация буфера по локальной сети (LAN) и временная полка для файлов.

---

## 🏗️ Architecture / Архитектура

BetterIsland is architected as a modular, high-performance .NET 10 solution split across clean domain layers:

```
BetterIsland.sln
├── src/
│   ├── BetterIsland/           # Main WPF Application (.NET 10-windows)
│   │   ├── Views & Windows/    # MainWindow (Island), SettingsWindow, ShelfWindow
│   │   ├── Native/             # Win32 hooks, SMTC listeners, HID monitors, Taskbar mirror
│   │   ├── Assets/             # High-res vectors, embedded icons, typography
│   │   └── PanelTheme.cs       # Glassmorphism shaders, OLED rim brushes, color presets
│   │
│   ├── BetterIsland.Core/      # Core Domain & Logic (.NET 10)
│   │   ├── Brands/             # Hardware HID decoders (Ardor, Logitech, Razer, Pulsar, etc.)
│   │   ├── Lyrics.cs           # Cascading lyrics engine (LRCLIB, NetEase, Yandex Music)
│   │   ├── Loc.cs              # Full multilingual translation engine (EN, RU, ZH)
│   │   ├── ClipboardSync/      # Encrypted LAN peer-to-peer clipboard synchronization
│   │   └── Settings.cs         # Serialized preferences, window rules & profile management
│   │
│   └── BetterIsland.Sensors/   # Hardware Telemetry Layer (.NET 10)
│       └── LibreHardwareMonitor abstraction (GPU/CPU temps, clocks, fan RPM, wattages)
│
└── tests/                      # Automated Test Suites
    ├── BetterIsland.Tests/     # Core unit tests
    ├── BetterIsland.MediaTests/# SMTC media and lyrics parsing tests
    └── BetterIsland.WindowsTests/# WPF window layout, DPI, and interaction checks
```

---

## 🛠️ Building & Publishing / Сборка и публикация

### Prerequisites / Требования
- **OS**: Windows 10 (Build 19041+) or Windows 11 (x64 / ARM64).
- **SDK**: [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).
- **IDE**: Visual Studio 2026, JetBrains Rider, or VS Code with C# Dev Kit.

### 1. Clone & Build / Клонирование и компиляция
```bash
git clone https://github.com/yuk1qwe/BetterIsland.git
cd BetterIsland

# Restore and build the solution in Release mode
dotnet build BetterIsland.sln -c Release
```

### 2. Run Tests / Запуск тестов
```bash
dotnet test tests/BetterIsland.Tests/BetterIsland.Tests.csproj
```

### 3. Production Publish / Публикация релиза
To create an optimized, framework-dependent binary ready for distribution:

```powershell
dotnet publish src\BetterIsland\BetterIsland.csproj `
  -c Release `
  -r win-x64 `
  --no-self-contained `
  -o publish
```

The published application executable and assets will be output to:
📁 `d:\BetterIsland\publish\BetterIsland.exe`

---

## ⌨️ Quick Shortcuts / Горячие клавиши

| Shortcut | Description (EN) | Описание (RU) |
|---|---|---|
| `Alt + ~` | Toggle island expand / collapse | Развернуть / свернуть остров |
| `Alt + V` | Open clipboard history | Открыть историю буфера обмена |
| `Alt + S` | Open file shelf | Открыть файловую полку |
| `Right Click` | Open BetterIsland settings | Открыть настройки BetterIsland |
| `Scroll Wheel` | Switch island tabs / Adjust volume | Переключение вкладок / Громкость |

---

## 💖 Credits & Благодарности

- **Original Project**: **[BrimDeck](https://github.com/im307/BrimDeck)** by **[@im307](https://github.com/im307)**. Deepest gratitude to @im307 and all BrimDeck contributors for laying the foundation of this desktop island experience.
- **Visual Design Inspiration**: **[islandapp.pro](https://islandapp.pro)** — for pioneering pure OLED aesthetics, subtle rim reflections, and fluid island interactions.
- **Lyrics Providers**: [LRCLIB](https://lrclib.net/) by @tranxuanthang, NetEase Cloud Music, and Yandex Music.
- **Hardware Telemetry**: Powered in part by [LibreHardwareMonitor](https://github.com/LibreHardwareMonitor/LibreHardwareMonitor).

---

## 📄 License

BetterIsland is licensed under the **Apache License 2.0**. See the [LICENSE](LICENSE) file for complete terms.
All upstream copyrights from BrimDeck remain with their respective authors.
