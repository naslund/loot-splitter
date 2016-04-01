namespace LootSplitter
{
    public static class Extensions
    {
        public static bool IsParsableToLongAndGreaterThanOrEqualToZero(this string input)
        {
            long output;
            if (long.TryParse(input, out output) == false)
                return false;
            return output >= 0;
        }
    }
}
