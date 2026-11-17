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

        // Compact styles
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
        ["Asleep"] = "Спящий режим",
        ["Compact"] = "Компактный",
        ["Standard"] = "Стандартный",
        ["Spacious"] = "Просторный",
        ["Keep style"] = "Сохранять стиль",
        ["As indicator"] = "Как индикатор",
        ["Hide"] = "Скрывать",
        ["Title"] = "Название",
        ["Lyrics"] = "Текст песни",
        ["None"] = "Нет",
        ["Both"] = "Оба",
        ["Text"] = "Текст",
        ["Cover"] = "Обложка",
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
