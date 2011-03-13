using java.io;
using java.util.zip;

namespace CraftyServer.Core
{
    public class CompressedStreamTools
    {
        public CompressedStreamTools()
        {
        }

        public static NBTTagCompound func_770_a(InputStream inputstream)
        {
            DataInputStream datainputstream = new DataInputStream(new GZIPInputStream(inputstream));
            try
            {
                NBTTagCompound nbttagcompound = func_774_a(datainputstream);
                return nbttagcompound;
            }
            finally
            {
                datainputstream.close();
            }
        }

        public static void writeGzippedCompoundToOutputStream(NBTTagCompound nbttagcompound, OutputStream outputstream)
        {
            DataOutputStream dataoutputstream = new DataOutputStream(new GZIPOutputStream(outputstream));
            try
            {
                func_771_a(nbttagcompound, dataoutputstream);
            }
            finally
            {
                dataoutputstream.close();
            }
        }

        public static NBTTagCompound func_774_a(DataInput datainput)
        {
            NBTBase nbtbase = NBTBase.readTag(datainput);
            if (nbtbase is NBTTagCompound)
            {
                return (NBTTagCompound) nbtbase;
            }
            else
            {
                throw new IOException("Root tag must be a named compound tag");
            }
        }

        public static void func_771_a(NBTTagCompound nbttagcompound, DataOutput dataoutput)
        {
            NBTBase.writeTag(nbttagcompound, dataoutput);
        }
    }
}