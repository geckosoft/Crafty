using java.io;
using java.util;
using java.lang;

namespace CraftyServer.Core
{
    public class NBTTagList : NBTBase
    {
        public NBTTagList()
        {
            tagList = new ArrayList();
        }

        public override void writeTagContents(DataOutput dataoutput)
        {
            if (tagList.size() > 0)
            {
                tagType = ((NBTBase) tagList.get(0)).getType();
            }
            else
            {
                tagType = 1;
            }
            dataoutput.writeByte(tagType);
            dataoutput.writeInt(tagList.size());
            for (int i = 0; i < tagList.size(); i++)
            {
                ((NBTBase) tagList.get(i)).writeTagContents(dataoutput);
            }
        }

        public override void readTagContents(DataInput datainput)
        {
            tagType = datainput.readByte();
            int i = datainput.readInt();
            tagList = new ArrayList();
            for (int j = 0; j < i; j++)
            {
                NBTBase nbtbase = NBTBase.createTagOfType(tagType);
                nbtbase.readTagContents(datainput);
                tagList.add(nbtbase);
            }
        }

        public override byte getType()
        {
            return 9;
        }

        public string toString()
        {
            return
                (new StringBuilder()).append("").append(tagList.size()).append(" entries of type ").append(
                    NBTBase.getTagName(tagType)).toString();
        }

        public void setTag(NBTBase nbtbase)
        {
            tagType = nbtbase.getType();
            tagList.add(nbtbase);
        }

        public NBTBase tagAt(int i)
        {
            return (NBTBase) tagList.get(i);
        }

        public int tagCount()
        {
            return tagList.size();
        }

        private List tagList;
        private byte tagType;
    }
}