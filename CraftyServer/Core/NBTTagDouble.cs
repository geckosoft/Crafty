using java.io;
using java.lang;

namespace CraftyServer.Core
{
    public class NBTTagDouble : NBTBase
    {
        public double doubleValue;

        public NBTTagDouble()
        {
        }

        public NBTTagDouble(double d)
        {
            doubleValue = d;
        }

        public override void writeTagContents(DataOutput dataoutput)
        {
            dataoutput.writeDouble(doubleValue);
        }

        public override void readTagContents(DataInput datainput)
        {
            doubleValue = datainput.readDouble();
        }

        public override byte getType()
        {
            return 6;
        }

        public string toString()
        {
            return (new StringBuilder()).append("").append(doubleValue).toString();
        }
    }
}