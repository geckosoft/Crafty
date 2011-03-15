using java.io;
using java.lang;
using java.util.regex;
using Object = System.Object;

namespace CraftyServer.Core
{
    internal class FileMatcher
        : Comparable
    {
        private readonly int field_22208_b;
        private readonly File field_22209_a;
        private readonly int field_22210_c;

        public FileMatcher(File file)
        {
            field_22209_a = file;
            Matcher matcher = ChunkFilePattern.field_22119_a.matcher(file.getName());
            if (matcher.matches())
            {
                field_22208_b = Integer.parseInt(matcher.group(1), 36);
                field_22210_c = Integer.parseInt(matcher.group(2), 36);
            }
            else
            {
                field_22208_b = 0;
                field_22210_c = 0;
            }
        }

        #region Comparable Members

        public virtual int CompareTo(Object obj)
        {
            return func_22206_a((FileMatcher) obj);
        }

        #endregion

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
    }
}