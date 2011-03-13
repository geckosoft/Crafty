using java.io;

namespace CraftyServer.Core
{
    public abstract class NBTBase
    {
        public NBTBase()
        {
            key = null;
        }

        public abstract void writeTagContents(DataOutput dataoutput);

        public abstract void readTagContents(DataInput datainput);

        public abstract byte getType();

        public string getKey()
        {
            if (key == null)
            {
                return "";
            }
            else
            {
                return key;
            }
        }

        public NBTBase setKey(string s)
        {
            key = s;
            return this;
        }

        public static NBTBase readTag(DataInput datainput)
        {
            byte byte0 = datainput.readByte();
            if (byte0 == 0)
            {
                return new NBTTagEnd();
            }
            else
            {
                NBTBase nbtbase = createTagOfType(byte0);
                nbtbase.key = datainput.readUTF();
                nbtbase.readTagContents(datainput);
                return nbtbase;
            }
        }

        public static void writeTag(NBTBase nbtbase, DataOutput dataoutput)
        {
            dataoutput.writeByte(nbtbase.getType());
            if (nbtbase.getType() == 0)
            {
                return;
            }
            else
            {
                dataoutput.writeUTF(nbtbase.getKey());
                nbtbase.writeTagContents(dataoutput);
                return;
            }
        }

        public static NBTBase createTagOfType(byte byte0)
        {
            switch (byte0)
            {
                case 0: // '\0'
                    return new NBTTagEnd();

                case 1: // '\001'
                    return new NBTTagByte();

                case 2: // '\002'
                    return new NBTTagShort();

                case 3: // '\003'
                    return new NBTTagInt();

                case 4: // '\004'
                    return new NBTTagLong();

                case 5: // '\005'
                    return new NBTTagFloat();

                case 6: // '\006'
                    return new NBTTagDouble();

                case 7: // '\007'
                    return new NBTTagByteArray();

                case 8: // '\b'
                    return new NBTTagString();

                case 9: // '\t'
                    return new NBTTagList();

                case 10: // '\n'
                    return new NBTTagCompound();
            }
            return null;
        }

        public static string getTagName(byte byte0)
        {
            switch (byte0)
            {
                case 0: // '\0'
                    return "TAG_End";

                case 1: // '\001'
                    return "TAG_Byte";

                case 2: // '\002'
                    return "TAG_Short";

                case 3: // '\003'
                    return "TAG_Int";

                case 4: // '\004'
                    return "TAG_Long";

                case 5: // '\005'
                    return "TAG_Float";

                case 6: // '\006'
                    return "TAG_Double";

                case 7: // '\007'
                    return "TAG_Byte_Array";

                case 8: // '\b'
                    return "TAG_String";

                case 9: // '\t'
                    return "TAG_List";

                case 10: // '\n'
                    return "TAG_Compound";
            }
            return "UNKNOWN";
        }

        private string key;
    }
}