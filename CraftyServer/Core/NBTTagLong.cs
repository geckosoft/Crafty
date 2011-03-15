using java.io;
using java.lang;

namespace CraftyServer.Core
{
    public class NBTTagLong : NBTBase
    {
        public long longValue;

        public NBTTagLong()
        {
        }

        public NBTTagLong(long l)
        {
            longValue = l;
        }

        public override void writeTagContents(DataOutput dataoutput)
        {
            dataoutput.writeLong(longValue);
        }

        public override void readTagContents(DataInput datainput)
        {
            longValue = datainput.readLong();
        }

        public override byte getType()
        {
            return 4;
        }

        public string toString()
        {
            return (new StringBuilder()).append("").append(longValue).toString();
        }
    }
}