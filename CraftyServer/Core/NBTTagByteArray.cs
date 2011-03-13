using java.io;
using java.lang;

namespace CraftyServer.Core
{
    public class NBTTagByteArray : NBTBase
    {
        public NBTTagByteArray()
        {
        }

        public NBTTagByteArray(byte[] abyte0)
        {
            byteArray = abyte0;
        }

        public override void writeTagContents(DataOutput dataoutput)
        {
            dataoutput.writeInt(byteArray.Length);
            dataoutput.write(byteArray);
        }

        public override void readTagContents(DataInput datainput)
        {
            int i = datainput.readInt();
            byteArray = new byte[i];
            datainput.readFully(byteArray);
        }

        public override byte getType()
        {
            return 7;
        }

        public string toString()
        {
            return (new StringBuilder()).append("[").append(byteArray.Length).append(" bytes]").toString();
        }

        public byte[] byteArray;
    }
}