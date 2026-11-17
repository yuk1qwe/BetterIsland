namespace BetterIsland.Core;

// ARDOR GAMING Essence mouse (model ARDW-ES3395-BW) on Areson / Compx JM03 controller:
// - Receiver (2.4 GHz): 25A7:FA7C
// - Cable / Charging: 25A7:FA7B
// On interface MI_01, vendor usage page (0xFF02 or vendor-defined), 17-byte reports:
// Query: output report 0x08, command 0x04, checksum making all 17 bytes sum to 0x55.
// Reply: input report 0x09 or 0x08, command 0x04, percent at byte 6, charging flag at byte 7.
// When the mouse is asleep or disconnected, byte 6 is 0 or report times out.
[BrandFamilies]
public static class ArdorMice
{
    public const int Vendor = 0x25A7, Receiver = 0xFA7C, Cable = 0xFA7B;
    public static readonly int[] Products = [Receiver, Cable];

    public static byte[] Request()
    {
        var frame = new byte[17];
        frame[0] = 0x08;
        frame[1] = 0x04;
        int sum = 0;
        for (int i = 0; i < 16; i++) sum += frame[i];
        frame[16] = (byte)(0x55 - sum);
        return frame;
    }

    public static BatteryRead? Battery(byte[] r)
    {
        if (r.Length < 17 || r[0] is not (0x08 or 0x09) || r[1] != 0x04 || r[6] > 100) return null;
        int sum = 0;
        for (int i = 0; i < 17; i++) sum += r[i];
        if ((byte)sum != 0x55) return null;
        bool echo = true;
        for (int i = 2; i < 16; i++) echo &= r[i] == 0;
        if (echo) return null;
        if (r[6] == 0) return new(null, Asleep: true);
        return new(r[6], r[7] == 1);
    }

    public static BatteryRead? CableBattery(byte[] r)
    {
        var read = Battery(r);
        return read is null ? null : read.Value with { Charging = true };
    }

    public static bool Takes(HidShape shape) =>
        shape.Vendor == Vendor && Products.Contains(shape.Product)
        && (shape.Interface is 1 or -1)
        && MouseShapes.VendorPage(shape);

    public static readonly BrandWay ReceiverWay = new("ardor.battery", WayTransport.Output, Request(), Battery)
    {
        TimeoutMs = 500,
        Attempts = 1
    };

    public static readonly BrandWay CableWay = new("ardor.battery", WayTransport.Output, Request(), CableBattery)
    {
        TimeoutMs = 500,
        Attempts = 1
    };

    public static IEnumerable<BrandFamily> Families =>
    [
        new("ARDOR GAMING", Takes, shape => [shape.Product == Cable ? CableWay : ReceiverWay])
        {
            Kind = _ => DeviceKind.Mouse,
            Prefix = "ardor",
            DisplayName = _ => "ARDOR GAMING Essence"
        }
    ];
}
