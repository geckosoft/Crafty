using java.io;
using java.lang;

namespace CraftyServer.Core
{
    public class FontAllowedCharacters
    {
        public FontAllowedCharacters()
        {
        }

        private static string getAllowedCharacters()
        {
            string s = "";
            try
            {
                BufferedReader bufferedreader =
                    new BufferedReader(
                        new InputStreamReader(
                            ((Class) (typeof (FontAllowedCharacters))).getResourceAsStream("/font.txt"), "UTF-8"));
                string s1 = "";
                do
                {
                    string s2;
                    if ((s2 = bufferedreader.readLine()) == null)
                    {
                        break;
                    }
                    if (!s2.StartsWith("#"))
                    {
                        s = (new StringBuilder()).append(s).append(s2).toString();
                    }
                } while (true);
                bufferedreader.close();
            }
            catch (Exception exception)
            {
            }
            return s;
        }

        public static string allowedCharacters = getAllowedCharacters();

        public static char[] field_22175_b = {
                                                 '/', '\n', '\r', '\t', '\0', '\f', '`', '?', '*', '\\',
                                                 '<', '>', '|', '"', ':'
                                             };
    }
}