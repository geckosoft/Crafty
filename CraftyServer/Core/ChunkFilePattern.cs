using java.io;
using java.util.regex;

//using java.util.regex.Pattern;

namespace CraftyServer.Core
{
    internal class ChunkFilePattern
        : FilenameFilter
    {
        public static Pattern field_22119_a = Pattern.compile("c\\.(-?[0-9a-z]+)\\.(-?[0-9a-z]+)\\.dat");

        public ChunkFilePattern()
        {
        }

        public ChunkFilePattern(Empty2 empty2) : this()
        {
        }

        #region FilenameFilter Members

        public bool accept(File file, string s)
        {
            Matcher matcher = field_22119_a.matcher(s);
            return matcher.matches();
        }

        #endregion
    }
}