using java.io;
using java.lang;

namespace CraftyServer.Core
{
    public class NBTTagShort : NBTBase
    {
        public short shortValue;

        public NBTTagShort()
        {
        }

        public NBTTagShort(short word0)
        {
            shortValue = word0;
        }

        public override void writeTagContents(DataOutput dataoutput)
        {
            dataoutput.writeShort(shortValue);
        }

        public override void readTagContents(DataInput datainput)
        {
            shortValue = datainput.readShort();
        }

        public override byte getType()
        {
            return 2;
        }

        public string toString()
        {
            return (new StringBuilder()).append("").append(shortValue).toString();
        }
    }
}