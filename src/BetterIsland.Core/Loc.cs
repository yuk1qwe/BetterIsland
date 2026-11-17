using System.Globalization;
using System.Text.RegularExpressions;

namespace BetterIsland.Core;

// The interface language. Each text is written as a pair where it is used: Loc.T("中文", "English").
// Russian is supported through runtime dictionary lookup, falling back to English.
public static class Loc
{
    public const string Chinese = "zh-CN", English = "en-US", Russian = "ru-RU";
    private static string _currentLanguage = Chinese;

    public static string Language => _currentLanguage;
    public static bool IsEnglish => _currentLanguage == English;
    public static bool IsRussian => _currentLanguage == Russian;
    public static bool IsChinese => _currentLanguage == Chinese;
    public static CultureInfo Culture => CultureInfo.GetCultureInfo(_currentLanguage);

    public static void Use(string language) => _currentLanguage = Normalize(language);

    public static string Normalize(string? language) => language switch
    {
        Russian => Russian,
        Chinese => Chinese,
        _ => English
    };

    public static string T(string chinese, string english)
    {
        if (IsRussian)
        {
            if (RussianMap.TryGetValue(english, out var ru) || RussianMap.TryGetValue(chinese, out ru))
                return ru;
            return english;
        }
        return IsEnglish ? english : chinese;
    }

    public static string Count(long count, string one, string many) =>
        IsRussian ? $"{count} {PluralRu(count, one, many)}" : $"{count} {(count == 1 ? one : many)}";

    private static string PluralRu(long count, string one, string many)
    {
        long n = Math.Abs(count);
        if (n % 10 == 1 && n % 100 != 11) return one;
        return many;
    }

    public static string SystemLanguage() => SystemLanguage(CultureInfo.CurrentUICulture);
    public static string SystemLanguage(CultureInfo culture) => culture.TwoLetterISOLanguageName switch
    {
        "ru" => Russian,
        "zh" => Chinese,
        _ => English
    };

    private static readonly Dictionary<string, string> Labels = new(StringComparer.Ordinal)
    {
        ["5 小时额度"] = "5-hour limit", ["每周额度"] = "Weekly limit", ["每月额度"] = "Monthly limit", ["主要额度"] = "Primary limit", ["次要额度"] = "Secondary limit",
        ["账期额度"] = "Billing period limit", ["套餐额度"] = "Plan limit",
        ["忙时每日额度"] = "Daily busy-hour limit", ["闲时每日额度"] = "Daily idle-hour limit", ["外部模型"] = "Outside models", ["内部模型"] = "Internal models",
        ["5 小时"] = "5-hour", ["每日"] = "Daily", ["每周"] = "Weekly", ["每月"] = "Monthly", ["主要"] = "Primary", ["次要"] = "Secondary",
        ["账期"] = "Billing period", ["套餐"] = "Plan", ["其他"] = "Other",
        ["Cursor 模型"] = "Cursor models", ["其他模型"] = "Other models", ["工具调用"] = "Tool calls",
        ["账户余额"] = "Account balance", ["密钥余额"] = "Key balance", ["今日扣费"] = "Charged today", ["本月扣费"] = "Charged this month", ["预算余额"] = "Budget left", ["密钥累计"] = "Key total",
        ["赠送余额"] = "Granted balance", ["充值余额"] = "Topped-up balance", ["冻结金额"] = "Frozen", ["现金余额"] = "Cash balance", ["代金券余额"] = "Voucher balance",["累计消费"] = "Total cost", ["加油包"] = "Extra usage", ["额外积分"] = "Extra credits", ["本月加油包"] = "Extra usage this month",
        ["次"] = "calls", ["积分"] = "credits", ["令牌"] = "tokens",
        ["网易云音乐"] = "NetEase Cloud Music", ["QQ 音乐"] = "QQ Music", ["酷狗音乐"] = "KuGou Music", ["爱奇艺"] = "iQIYI",
        ["Яндекс Музыка"] = "Яндекс Музыка",
    };

    private static readonly Dictionary<string, string> RussianMap = new(StringComparer.OrdinalIgnoreCase)
    {
        // Navigation & Titles
        ["General"] = "Основные",
        ["Collapsed view"] = "Компактный вид",
        ["Expanded view"] = "Развернутый вид",
        ["Quotas and usage"] = "Квоты и статистика",
        ["Model prices"] = "Цены моделей",
        ["Style"] = "Стиль",
        ["Players"] = "Плееры",
        ["About BetterIsland"] = "О программе BetterIsland",
        ["Panel theme"] = "Темы панели",
        ["Display and readings"] = "Отображение и датчики",
        ["Enhanced sensors"] = "Служба датчиков",
        ["Shortcuts"] = "Горячие клавиши",
        ["Display and devices"] = "Устройства и батарея",
        ["Clipboard"] = "Буфер обмена",
        ["File transfer"] = "Файловая полка",
        ["Quick actions"] = "Быстрые действия",
        ["Displays and profiles"] = "Мониторы и профили",
        ["Page layout"] = "Разметка страниц",
        ["Clipboard sync"] = "Синхронизация буфера",
        ["Basics"] = "Базовые настройки",
        ["AI usage"] = "AI-квоты",
        ["System monitor"] = "Мониторинг системы",
        ["Shelf"] = "Полка",
        ["Device batteries"] = "Батарея устройств",
        ["Music"] = "Музыка",
        ["About"] = "О программе",
        ["Modules"] = "Модули",
        ["BetterIsland Settings"] = "Настройки BetterIsland",

        // Modules
        ["Music player"] = "Музыкальный плеер",
        ["Device battery"] = "Батарея устройств",
        ["System monitor (CPU/GPU/RAM/disk/network)"] = "Мониторинг системы (CPU/GPU/RAM/диск/сеть)",
        ["AI quotas (Claude, Codex, Cursor, etc.)"] = "AI-квоты (Claude Code, Codex, Cursor и др.)",
        ["Clipboard history"] = "История буфера обмена",
        ["Clipboard sync across devices"] = "Синхронизация буфера между устройствами",
        ["File shelf"] = "Файловая полка",
        ["BetterIsland.Sensors background service"] = "Служба BetterIsland.Sensors (фоновый сервис)",
        ["Weather"] = "Погода",
        ["Enable or disable modules"] = "Включение и отключение встроенных модулей",

        // General page
        ["Startup and resources"] = "Автозапуск и ресурсы",
        ["Start with Windows"] = "Запускать вместе с Windows",
        ["Show tray icon"] = "Показывать значок в трее",
        ["Resource use"] = "Использование ресурсов",
        ["Exit BetterIsland"] = "Выйти из BetterIsland",
        ["Exit"] = "Выход",
        ["Appearance and language"] = "Внешний вид и язык",
        ["Theme"] = "Тема оформления",
        ["System"] = "Системная",
        ["Light"] = "Светлая",
        ["Dark"] = "Тёмная",
        ["Language"] = "Язык",
        ["Scale"] = "Масштаб",
        ["Background transparency"] = "Прозрачность фона",
        ["Interface font"] = "Шрифт интерфейса",
        ["Default"] = "По умолчанию",
        ["Apply font to settings window"] = "Применять шрифт к окну настроек",
        ["Font preview"] = "Предпросмотр шрифта",
        ["Restore defaults"] = "Сбросить по умолчанию",
        ["Restore"] = "Восстановить",
        ["Minimize"] = "Свернуть",
        ["Maximize"] = "Развернуть",
        ["Close settings"] = "Закрыть настройки",

        // Compact styles & behaviors
        ["Notch"] = "Вырез (Notch)",
        ["Capsule"] = "Пилюля (Capsule)",
        ["Indicator"] = "Индикатор",
        ["Pages shown"] = "Отображаемые страницы",
        ["Page shown on opening"] = "Страница при открытии",
        ["Last viewed page"] = "Последняя открытая",
        ["music"] = "музыка",
        ["system monitor"] = "мониторинг системы",
        ["clipboard and file transfer"] = "буфер и файлы",
        ["device batteries"] = "батарея устройств",
        ["Show music"] = "Показывать музыку",
        ["Show system monitor"] = "Показывать мониторинг системы",
        ["Show clipboard and file transfer"] = "Показывать буфер и файлы",
        ["Show device batteries"] = "Показывать батарею устройств",
        ["Show AI usage"] = "Показывать AI-квоты",
        ["Right-click opens settings"] = "Правый клик открывает настройки",
        ["Expand on"] = "Способ раскрытия",
        ["Hover"] = "Наведение курсора",
        ["Click"] = "Клик",
        ["Expand at"] = "Позиция раскрытия",
        ["Middle"] = "По центру экрана",
        ["Pointer"] = "Следовать за курсором",
        ["Mouse wheel"] = "Колесо мыши",
        ["Switch pages"] = "Переключение вкладок",
        ["Volume"] = "Регулировка громкости",
        ["Nothing"] = "Ничего не делать",
        ["Keep style"] = "Обычный (всегда на экране)",
        ["As indicator"] = "Тонкая полоска",
        ["Hide"] = "Скрывать",
        ["Follow"] = "Как в глобальных",
        ["When windows are maximized"] = "Когда окно развернуто",
        ["When a window is maximized"] = "При развернутом окне",
        ["When an app is in borderless full screen"] = "Полноэкранный режим без рамок",
        ["When an app is in exclusive full screen"] = "Эксклюзивный полноэкранный режим",
        ["When a window overlaps BetterIsland"] = "При перекрытии окна BetterIsland",
        ["Also check windows behind"] = "Проверять окна позади",
        ["App rules"] = "Правила приложений",
        ["Compatibility"] = "Совместимость",
        ["Keep on top"] = "Всегда наверху",
        ["Raise now"] = "Выдвинуть на передний план",
        ["Raise once"] = "Однократно на передний план",
        ["Allow window capture"] = "Разрешить захват окна (OBS)",
        ["Window behavior"] = "Поведение окна",
        ["Keep above other windows"] = "Поверх других окон",
        ["Expand delay"] = "Задержка раскрытия",
        ["Collapse delay"] = "Задержка сворачивания",
        ["Animations"] = "Анимации",
        ["Animation duration"] = "Длительность анимаций",
        ["Show app rules"] = "Показать правила приложений",
        ["Add an app rule"] = "Добавить правило для приложения",
        ["Maximized"] = "Развернуто",
        ["Borderless full screen"] = "Полноэкранный без рамок",
        ["Exclusive full screen"] = "Эксклюзивный полноэкранный",
        ["No apps to add"] = "Нет приложений для добавления",
        ["Choose an app to exempt"] = "Выбрать приложение для исключения",
        ["Keep island always on screen (never collapse to line)"] = "Всегда держать остров на экране (не сворачивать в полоску)",

        // Players & Music
        ["Playback sources"] = "Источники воспроизведения",
        ["Ignored apps"] = "Игнорируемые приложения",
        ["NetEase Cloud Music"] = "NetEase Cloud Music",
        ["Turn on \"开启 SMTC\" in NetEase Cloud Music's 设置 › 系统 to control playback."] = "Включите SMTC в настройках NetEase Cloud Music для управления воспроизведением.",
        ["Always show \"Enable full control\""] = "Всегда показывать «Включить полный контроль»",
        ["Enable full control when it starts"] = "Автоматически включать полный контроль при запуске",
        ["Always enable full control"] = "Всегда включать полный контроль",
        ["NetEase Cloud Music is not installed"] = "NetEase Cloud Music не установлен",
        ["KuGou Music"] = "KuGou Music",
        ["Check \"支持系统播放控件，如锁屏界面\" in KuGou's 设置 › 常规设置 › 播放 to show the track and control playback."] = "Включите системные элементы управления в настройках KuGou для управления воспроизведением.",
        ["KuGou Music is not installed"] = "KuGou Music не установлен",
        ["Apple Music"] = "Apple Music",
        ["QQ Music"] = "QQ Music",
        ["Needs no setup."] = "Настройка не требуется (поддерживается нативно).",
        ["Show playback progress"] = "Показывать прогресс воспроизведения",
        ["Music now playing"] = "Сейчас играет",
        ["Quota rings"] = "Кольца квот",
        ["Equalizer"] = "Эквалайзер",
        ["File transfer"] = "Передача файлов",
        ["Device batteries"] = "Батарея устройств",
        ["No devices to show"] = "Нет устройств для отображения",
        ["Looking for devices…"] = "Поиск устройств…",
        ["No wireless devices connected. They show here once connected."] = "Нет подключенных беспроводных устройств.",
        ["Hide from island page"] = "Скрыть со страницы островка",
        ["Hide from collapsed notch"] = "Скрыть из свернутого вида",
        ["Device settings…"] = "Настройки устройств…",
        ["Rescan devices"] = "Повторно сканировать устройства",
        ["Battery thresholds"] = "Пороги заряда батареи",
        ["Warning threshold"] = "Порог предупреждения",
        ["Critical threshold"] = "Порог критического заряда",
        ["When collapsed"] = "В свернутом виде",
        ["Show figures"] = "Показывать процент цифрами",
        ["Devices"] = "Устройства",
        ["Collapsed preview"] = "Предпросмотр в свернутом виде",
        ["The ring's color while the battery is normal."] = "Цвет индикатора при нормальном заряде.",
        ["The ring's color while charging."] = "Цвет индикатора при зарядке.",
        ["The color at or under the warning threshold."] = "Цвет при уровне ниже порога предупреждения.",
        ["The color at or under the critical threshold."] = "Цвет при критически низком заряде.",
        ["Turned off, nothing is sent to the device. It turns off by itself if the device misbehaves while read."] = "При отключении опросы не отправляются устройству.",
        ["Nothing is playing"] = "Сейчас ничего не играет",

        // Navigation & Sections
        ["Basics"] = "Основные",
        ["AI usage"] = "AI-квоты",
        ["System monitor"] = "Системный монитор",
        ["Shelf"] = "Полка и буфер",
        ["Device batteries"] = "Батарея устройств",
        ["Music"] = "Музыка",
        ["About"] = "О программе",
        ["Shortcuts"] = "Горячие клавиши",
        ["More themes"] = "Другие темы",
        ["Custom themes"] = "Пользовательские темы",
        ["Panel theme"] = "Темы островка",
        ["Choose island surface color (click for color picker, e.g. #000000 OLED pure black)"] = "Точный цвет фона островка (нажмите для выбора цвета или ввода HEX, например #000000)",
        ["Choose accent color (keyboard focus, highlights, etc.)"] = "Акцентный цвет (кнопки, индикаторы, фокус)",
        ["Panel surface"] = "Фон островка",
        ["Primary text"] = "Основной текст",
        ["Secondary text"] = "Вторичный текст",
        ["Tertiary text"] = "Дополнительный текст",
        ["Accent (keyboard focus, chosen dates)"] = "Акцентный цвет",
        ["Errors and paused sync"] = "Ошибки",
        ["Liked song"] = "Любимый трек",
        ["Default clock color"] = "Цвет часов",
        ["Default equalizer color"] = "Цвет эквалайзера",
        ["Dividers"] = "Разделители",
        ["Progress track"] = "Фон прогресса",
        ["Pop-up surface"] = "Всплывающие окна",
        ["Outlines"] = "Контуры",
        ["Scroll bar"] = "Полоса прокрутки",
        ["Shadow or glow"] = "Тень",
        ["Tooltip surface"] = "Фон подсказок",
        ["Tooltip border"] = "Рамка подсказок",
        ["General"] = "Основные",
        ["Collapsed view"] = "Свернутый вид",
        ["Expanded view"] = "Развернутый вид",
        ["Quotas and usage"] = "Квоты и статистика",
        ["Model prices"] = "Цены моделей",
        ["Style"] = "Стиль",
        ["Players"] = "Плееры",
        ["About BetterIsland"] = "О BetterIsland",
        ["Display and readings"] = "Экраны и показания",
        ["Enhanced sensors"] = "Датчики оборудования",
        ["Display and devices"] = "Экраны и устройства",
        ["Clipboard"] = "Буфер обмена",
        ["File transfer"] = "Передача файлов",
        ["Quick actions"] = "Быстрые действия",
        ["Displays and profiles"] = "Экраны и профили",
        ["Page layout"] = "Разметка страницы",
        ["Clipboard sync"] = "Синхронизация буфера",

        // State & Action
        ["Installed"] = "Установлено",
        ["Not installed"] = "Не установлено",
        ["Install"] = "Установить",
        ["Uninstall"] = "Удалить",
        ["Learn more ↗"] = "Подробнее ↗",
        ["Reading now"] = "Текущие показания",
        ["Sensor service"] = "Служба датчиков",
        ["Step 1: PawnIO driver"] = "Шаг 1: Драйвер PawnIO",
        ["Step 2: Sensor service"] = "Шаг 2: Служба датчиков",
        ["Save"] = "Сохранить",
        ["Cancel"] = "Отмена",
        ["Apply"] = "Применить",
        ["Connected"] = "Подключено",
        ["Disconnected"] = "Отключено",
        ["Charging"] = "Зарядка",
        ["Asleep"] = "В режиме сна",
        ["No battery reported"] = "Нет данных о батарее",
        ["Battery low"] = "Низкий заряд",
        ["Getting low"] = "Заряд на исходе",
        ["Playing"] = "Воспроизведение",
        ["Paused"] = "Пауза",
        ["Stopped"] = "Остановлено",
        ["Unknown state"] = "Неизвестное состояние",
        ["Switch playback source"] = "Источник воспроизведения",
        ["Turn on SMTC in NetEase to control playback here"] = "Включите SMTC в плеере для управления",
        ["Compact"] = "Компактный",
        ["Standard"] = "Стандартный",
        ["Spacious"] = "Просторный",
        ["Title"] = "Название",
        ["Lyrics"] = "Текст песни",
        ["None"] = "Нет",
        ["Both"] = "Оба",
        ["Text"] = "Текст",
        ["Cover"] = "Обложка",
        ["Color picker"] = "Выбор цвета",
        ["Color value"] = "Значение цвета",
        ["Color "] = "Цвет ",
        ["OK"] = "ОК",
        ["Default"] = "По умолчанию",
        ["Add app"] = "Добавить приложение",
        ["Quota source"] = "Источник квоты",
        ["Usage app"] = "Приложение статистики",
        ["Display name"] = "Отображаемое имя",
        ["Warning"] = "Предупреждение",
        ["Critical"] = "Тревога",
        ["Show"] = "Показ",
        ["Remove app"] = "Удалить приложение",
        ["Remove"] = "Удалить",
    };

    public static string Label(string text)
    {
        if (text.Length == 0) return text;
        if (text.Contains('·')) return string.Join(" · ", text.Split('·').Select(part => Label(part.Trim())));
        if (IsRussian)
        {
            if (RussianMap.TryGetValue(text, out var ru)) return ru;
            if (Labels.TryGetValue(text, out var en) && RussianMap.TryGetValue(en, out ru)) return ru;
        }
        if (Labels.TryGetValue(text, out var english)) return IsEnglish ? english : text;
        if (Regex.Match(text, @"^(\d+) 分钟(额度)?$") is { Success: true } minutes)
            return IsRussian ? $"{minutes.Groups[1].Value} мин." : (minutes.Groups[1].Value + "-minute" + (minutes.Groups[2].Success ? " limit" : ""));
        if (Regex.Match(text, @"^(\d+) 小时(额度)?$") is { Success: true } hours)
            return IsRussian ? $"{hours.Groups[1].Value} ч." : (hours.Groups[1].Value + "-hour" + (hours.Groups[2].Success ? " limit" : ""));
        if (text.EndsWith(" 每周", StringComparison.Ordinal))
            return IsRussian ? (text[..^3] + " еженедельно") : (text[..^3] + " weekly");
        return text;
    }
}
