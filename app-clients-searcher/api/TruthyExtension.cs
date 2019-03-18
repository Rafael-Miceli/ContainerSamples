namespace TruthyExtension
{
    public static class Extensions
    {
        public static bool ToTruthy(this string value) =>
            !string.IsNullOrEmpty(value)
            &&
            (   
                value != "0"
                && value.ToLower() != "false"
            );

        public static bool ToTruthy(this int value) =>
            value != 0;
    }
    
}