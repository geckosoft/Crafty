using java.io;
using java.lang;

namespace CraftyServer.Core
{
    public class NBTTagString : NBTBase
    {
        public string stringValue;

        public NBTTagString()
        {
        }

        public NBTTagString(string s)
        {
            stringValue = s;
            if (s == null)
            {
                throw new IllegalArgumentException("Empty string not allowed");
            }
            else
            {
                return;
            }
        }

        public override void writeTagContents(DataOutput dataoutput)
        {
            dataoutput.writeUTF(stringValue);
        }

        public override void readTagContents(DataInput datainput)
        {
            stringValue = datainput.readUTF();
        }

        public override byte getType()
        {
            return 8;
        }

        public string toString()
        {
            return (new StringBuilder()).append("").append(stringValue).toString();
        }
    }
}