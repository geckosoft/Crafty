using java.io;
using java.util.regex;

namespace CraftyServer.Core
{
    public class ChunkFolderPattern
        : FileFilter
    {
        public static Pattern field_22214_a = Pattern.compile("[0-9a-z]|([0-9a-z][0-9a-z])");

        public ChunkFolderPattern()
        {
        }

        public ChunkFolderPattern(Empty2 empty2) : this()
        {
        }

        #region FileFilter Members

        public bool accept(File file)
        {
            if (file.isDirectory())
            {
                Matcher matcher = field_22214_a.matcher(file.getName());
                return matcher.matches();
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}