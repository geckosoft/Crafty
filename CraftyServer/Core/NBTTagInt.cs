using java.io;
using java.lang;

namespace CraftyServer.Core
{
    public class NBTTagInt : NBTBase
    {
        public int intValue;

        public NBTTagInt()
        {
        }

        public NBTTagInt(int i)
        {
            intValue = i;
        }

        public override void writeTagContents(DataOutput dataoutput)
        {
            dataoutput.writeInt(intValue);
        }

        public override void readTagContents(DataInput datainput)
        {
            intValue = datainput.readInt();
        }

        public override byte getType()
        {
            return 3;
        }

        public string toString()
        {
            return (new StringBuilder()).append("").append(intValue).toString();
        }
    }
}