using java.io;
using java.lang;

namespace CraftyServer.Core
{
    public class NBTTagFloat : NBTBase
    {
        public NBTTagFloat()
        {
        }

        public NBTTagFloat(float f)
        {
            floatValue = f;
        }

        public override void writeTagContents(DataOutput dataoutput)
        {
            dataoutput.writeFloat(floatValue);
        }

        public override void readTagContents(DataInput datainput)
        {
            floatValue = datainput.readFloat();
        }

        public override byte getType()
        {
            return 5;
        }

        public string toString()
        {
            return (new StringBuilder()).append("").append(floatValue).toString();
        }

        public float floatValue;
    }
}