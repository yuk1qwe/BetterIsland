# Supported Devices / Поддерживаемые устройства

BetterIsland reads battery levels and charging statuses from connected wireless mice, keyboards, headsets, controllers, pens, and phones.

## ARDOR GAMING
- **Model**: Essence (ARDW-ES3395-BW)
- **Controller**: Areson Technology Corp / Compx JM03
- **IDs**:
  - `VID_25A7 PID_FA7C` — 2.4 GHz Wireless Receiver
  - `VID_25A7 PID_FA7B` — Wired USB / Charging
- **Interface**: `MI_01`, Vendor Usage Page
- **Features**: Battery percentage read, charging state detection, sleep mode graceful timeout handling.

---

## Supported connections / Поддерживаемые подключения

| Connection / Подключение | How it is read / Как считывается | Notes / Примечания |
| --- | --- | --- |
| Bluetooth | Windows Settings › Bluetooth & devices, BLE Battery Service | Connected devices |
| 2.4 GHz receiver / Cable | Brand-specific HID protocols (Compx, Areson, Nordic, etc.) | Mice & keyboards |
| Controllers | XInput, DirectInput, Sony DualSense HID, vendor reports | Gamepads |
| Phone | Windows Phone Link link card | Link battery status |

---

## Supported brands / Поддерживаемые бренды

### Mice / Мыши
- **Whole brand / Универсальный протокол**: Logitech, MCHOSE, ATK, Endgame Gear, VAXEE, Rapoo, Keychron, Finalmouse.
- **Known models / Поддерживаемые модели**:
  - **ARDOR GAMING**: Essence (ARDW-ES3395-BW)
  - **Razer, ASUS ROG, SteelSeries, Corsair, HyperX**
  - **VXE, VGN, Pulsar, LAMZU, G-Wolves, WLmouse, Ninjutso, LINGBAO, NZXT, HECATE, Dareu, Inphic, AJAZZ, Attack Shark, DELUX, Glorious, Darmoshark, EWEADN, AULA, Thunderobot, Scyrox, Cougar, Redragon, Hitscan, iRocks, Vancer, Incott, Ryunix**.

### Keyboards / Клавиатуры
- Logitech, Razer, ASUS ROG, MCHOSE, AULA, VGN, ATK, CHERRY, Corsair, Alienware Pro, URX, Lofree, MonsGeek, Akko, Epomaker, AJAZZ, RK, etc.

### Headsets / Гарнитуры
- Jabra, FIFINE, XIBERIA, Logitech, SteelSeries, HyperX, Corsair, Razer, Sony INZONE & PULSE Explore, ASUS ROG, ASTRO A50, JBL Quantum, ATK, Edifier, etc.

### Controllers / Геймпады
- Xbox, PlayStation (DualSense, DualSense Edge, DualShock 4), Flydigi, GameSir, 8BitDo, BETOP, Steam Controller, NVIDIA SHIELD.
