namespace TruthyExtension
{
    public static class Extensions
    {
        public static bool ToTruthy(this string value) =>
            !string.IsNullOrEmpty(value) && bool.Parse(value);
    }
    
}