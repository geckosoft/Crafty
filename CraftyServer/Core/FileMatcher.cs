using java.io;
using java.lang;
using java.util.regex;

namespace CraftyServer.Core
{
    internal class FileMatcher
        : Comparable
    {
        public FileMatcher(File file)
        {
            field_22209_a = file;
            Matcher matcher = ChunkFilePattern.field_22119_a.matcher(file.getName());
            if (matcher.matches())
            {
                field_22208_b = java.lang.Integer.parseInt(matcher.group(1), 36);
                field_22210_c = java.lang.Integer.parseInt(matcher.group(2), 36);
            }
            else
            {
                field_22208_b = 0;
                field_22210_c = 0;
            }
        }

        public int func_22206_a(FileMatcher filematcher)
        {
            int i = field_22208_b >> 5;
            int j = filematcher.field_22208_b >> 5;
            if (i == j)
            {
                int k = field_22210_c >> 5;
                int l = filematcher.field_22210_c >> 5;
                return k - l;
            }
            else
            {
                return i - j;
            }
        }

        public File func_22207_a()
        {
            return field_22209_a;
        }

        public int func_22205_b()
        {
            return field_22208_b;
        }

        public int func_22204_c()
        {
            return field_22210_c;
        }

        public virtual int CompareTo(System.Object obj)
        {
            return func_22206_a((FileMatcher) obj);
        }


        private File field_22209_a;
        private int field_22208_b;
        private int field_22210_c;
    }
}