using System.Text;
using java.lang;

namespace CraftyServer.Core
{
    public class FontAllowedCharacters
    {
        public static string allowedCharacters = getAllowedCharacters();

        public static char[] field_22175_b = {
                                                 '/', '\n', '\r', '\t', '\0', '\f', '`', '?', '*', '\\',
                                                 '<', '>', '|', '"', ':'
                                             };

        private static string getAllowedCharacters()
        {
            try
            {

                string s = System.IO.File.ReadAllText("font.txt", Encoding.UTF8);

                return s;
            }catch(Exception ex)
            {
                return string.Empty;
            }
        }
    }
}