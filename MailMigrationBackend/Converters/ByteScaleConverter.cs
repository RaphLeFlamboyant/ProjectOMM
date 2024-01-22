namespace MailMigrationBackend.Converters;

public static class ByteScaleConverter
{
    private static int gigabyteInBytes = 1073741824;
    
    public static double ConvertByteToGigabyte(int bytes)
    {
        var result = (double)bytes / gigabyteInBytes;
        return result;
    }
}