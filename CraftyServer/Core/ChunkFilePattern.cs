using java.io;
using java.util.regex;

//using java.util.regex.Pattern;

namespace CraftyServer.Core
{
    internal class ChunkFilePattern
        : FilenameFilter
    {
        public ChunkFilePattern()
        {
        }

        public bool accept(File file, string s)
        {
            Matcher matcher = field_22119_a.matcher(s);
            return matcher.matches();
        }

        public ChunkFilePattern(Empty2 empty2) : this()
        {
        }

        public static Pattern field_22119_a = Pattern.compile("c\\.(-?[0-9a-z]+)\\.(-?[0-9a-z]+)\\.dat");
    }
}