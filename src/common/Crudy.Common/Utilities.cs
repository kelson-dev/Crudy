using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crudy.Common
{
    public static class Utilities
    {
        public static string PluralizeCamelCase(this string text)
        {
            var (prefix, lastWord) = text.SpliltLastWordOfCamelCase();

            bool IsUpperBaseVowelInvariant(char c) =>
                    c == 'A'
                 || c == 'E'
                 || c == 'I'
                 || c == 'O'
                 || c == 'U';

            (string, string) GetTrailingTextToReplaceToPluralize(string word)
            {
                switch (word.ToUpperInvariant())
                {
                    case "MAN":
                        return ("an", "en");
                    case "WOMAN":
                        return ("an", "en");
                    case "CHILD":
                        return ("", "ren");
                    case "TOOTH":
                        return ("ooth", "eeth");
                    case "FOOT":
                        return ("oot", "eet");
                    case "MOUSE":
                        return ("ouse", "ice");
                    case "BELIEF":
                        return ("", "s");
                    default:
                        break;
                }
                if (text[^1] == 'Y' && !IsUpperBaseVowelInvariant(text[^2]))
                    return (text[^1..], "ies");
                else if (text.EndsWith("us")
                    || text.EndsWith("ss")
                    || text.EndsWith("x")
                    || text.EndsWith("ch")
                    || text.EndsWith("sh"))
                    return ("", "es");
                else if (text.EndsWith("s"))
                    return ("", "");
                else if (text.EndsWith("f"))
                    return (text[^1..], "ves");
                else if (text.EndsWith("fe"))
                    return (text[^2..], "ves");
                else
                    return ("", "s");
            }

            (string replace, string with) = GetTrailingTextToReplaceToPluralize(lastWord);

            return prefix + lastWord[..^(replace.Length)] + with;
        }

        public static (string before, string last) SpliltLastWordOfCamelCase(this string text)
        {
            for (int i = text.Length - 1; i > 0; i--)
            {
                if (char.IsUpper(text[i]))
                {
                    return (text.Substring(0, i), text.Substring(i));
                }
            }
            return (string.Empty, text);
        }
    }
}
