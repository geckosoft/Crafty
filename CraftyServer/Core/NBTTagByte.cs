using java.io;
using java.lang;

namespace CraftyServer.Core
{
    public class NBTTagByte : NBTBase
    {
        public NBTTagByte()
        {
        }

        public NBTTagByte(byte byte0)
        {
            byteValue = byte0;
        }

        public override void writeTagContents(DataOutput dataoutput)
        {
            dataoutput.writeByte(byteValue);
        }

        public override void readTagContents(DataInput datainput)
        {
            byteValue = datainput.readByte();
        }

        public override byte getType()
        {
            return 1;
        }

        public string toString()
        {
            return (new StringBuilder()).append("").append(byteValue).toString();
        }

        public byte byteValue;
    }
}