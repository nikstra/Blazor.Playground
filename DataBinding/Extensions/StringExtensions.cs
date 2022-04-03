namespace DataBinding.Extensions
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string @string) =>
            string.Create(@string.Length, @string, (c, s) =>
            {
                c[0] = char.ToUpper(s[0]);
                s[1..].CopyTo(c[1..]);
            });
    }
}