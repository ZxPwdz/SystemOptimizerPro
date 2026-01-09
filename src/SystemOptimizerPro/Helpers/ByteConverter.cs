namespace SystemOptimizerPro.Helpers;

public static class ByteConverter
{
    private static readonly string[] SizeSuffixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };

    public static string ToReadableSize(ulong bytes, int decimalPlaces = 1)
    {
        if (bytes == 0)
            return "0 B";

        int magnitude = (int)Math.Floor(Math.Log(bytes, 1024));
        magnitude = Math.Min(magnitude, SizeSuffixes.Length - 1);

        double adjustedSize = bytes / Math.Pow(1024, magnitude);

        return string.Format("{0:N" + decimalPlaces + "} {1}", adjustedSize, SizeSuffixes[magnitude]);
    }

    public static string ToReadableSize(long bytes, int decimalPlaces = 1)
    {
        if (bytes < 0)
            return "-" + ToReadableSize((ulong)Math.Abs(bytes), decimalPlaces);
        return ToReadableSize((ulong)bytes, decimalPlaces);
    }

    public static double BytesToGigabytes(ulong bytes)
    {
        return bytes / (1024.0 * 1024.0 * 1024.0);
    }

    public static double BytesToMegabytes(ulong bytes)
    {
        return bytes / (1024.0 * 1024.0);
    }

    public static ulong GigabytesToBytes(double gigabytes)
    {
        return (ulong)(gigabytes * 1024 * 1024 * 1024);
    }

    public static ulong MegabytesToBytes(double megabytes)
    {
        return (ulong)(megabytes * 1024 * 1024);
    }
}
