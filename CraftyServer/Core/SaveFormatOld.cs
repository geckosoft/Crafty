using java.io;
using java.lang;

namespace CraftyServer.Core
{
    public class SaveFormatOld
        : ISaveFormat
    {
        protected File field_22106_a;

        public SaveFormatOld(File file)
        {
            field_22106_a = file;
        }

        #region ISaveFormat Members

        public virtual bool func_22102_a(string s)
        {
            return false;
        }

        public virtual bool func_22101_a(string s, IProgressUpdate iprogressupdate)
        {
            return false;
        }

        #endregion

        public WorldInfo func_22103_b(string s)
        {
            var file = new File(field_22106_a, s);
            if (!file.exists())
            {
                return null;
            }
            var file1 = new File(file, "level.dat");
            if (file1.exists())
            {
                try
                {
                    NBTTagCompound nbttagcompound = CompressedStreamTools.func_770_a(new FileInputStream(file1));
                    NBTTagCompound nbttagcompound1 = nbttagcompound.getCompoundTag("Data");
                    return new WorldInfo(nbttagcompound1);
                }
                catch (Exception exception)
                {
                    exception.printStackTrace();
                }
            }
            return null;
        }

        protected static void func_22104_a(File[] afile)
        {
            for (int i = 0; i < afile.Length; i++)
            {
                if (afile[i].isDirectory())
                {
                    func_22104_a(afile[i].listFiles());
                }
                afile[i].delete();
            }
        }

        public virtual ISaveHandler func_22105_a(string s, bool flag)
        {
            return new PlayerNBTManager(field_22106_a, s, flag);
        }
    }
}