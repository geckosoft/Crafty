using java.io;

namespace CraftyServer.Core
{
    public class NBTTagEnd : NBTBase
    {
        public NBTTagEnd()
        {
        }

        public override void readTagContents(DataInput datainput)
        {
        }

        public override void writeTagContents(DataOutput dataoutput)
        {
        }

        public override byte getType()
        {
            return 0;
        }

        public string toString()
        {
            return "END";
        }
    }
}