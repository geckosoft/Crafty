using java.io;
using java.lang;
using java.util;

namespace CraftyServer.Core
{
    public class NBTTagCompound : NBTBase
    {
        private readonly Map tagMap;

        public NBTTagCompound()
        {
            tagMap = new HashMap();
        }

        public override void writeTagContents(DataOutput dataoutput)
        {
            NBTBase nbtbase;
            for (Iterator iterator = tagMap.values().iterator();
                 iterator.hasNext();
                 writeTag(nbtbase, dataoutput))
            {
                nbtbase = (NBTBase) iterator.next();
            }

            dataoutput.writeByte(0);
        }

        public override void readTagContents(DataInput datainput)
        {
            tagMap.clear();
            NBTBase nbtbase;
            for (; (nbtbase = readTag(datainput)).getType() != 0; tagMap.put(nbtbase.getKey(), nbtbase))
            {
            }
        }

        public override byte getType()
        {
            return 10;
        }

        public void setTag(string s, NBTBase nbtbase)
        {
            tagMap.put(s, nbtbase.setKey(s));
        }

        public void setByte(string s, byte byte0)
        {
            tagMap.put(s, (new NBTTagByte(byte0)).setKey(s));
        }

        public void setShort(string s, short word0)
        {
            tagMap.put(s, (new NBTTagShort(word0)).setKey(s));
        }

        public void setInteger(string s, int i)
        {
            tagMap.put(s, (new NBTTagInt(i)).setKey(s));
        }

        public void setLong(string s, long l)
        {
            tagMap.put(s, (new NBTTagLong(l)).setKey(s));
        }

        public void setFloat(string s, float f)
        {
            tagMap.put(s, (new NBTTagFloat(f)).setKey(s));
        }

        public void setDouble(string s, double d)
        {
            tagMap.put(s, (new NBTTagDouble(d)).setKey(s));
        }

        public void setString(string s, string s1)
        {
            tagMap.put(s, (new NBTTagString(s1)).setKey(s));
        }

        public void setByteArray(string s, byte[] abyte0)
        {
            tagMap.put(s, (new NBTTagByteArray(abyte0)).setKey(s));
        }

        public void setCompoundTag(string s, NBTTagCompound nbttagcompound)
        {
            tagMap.put(s, nbttagcompound.setKey(s));
        }

        public void setBoolean(string s, bool flag)
        {
            setByte(s, ((byte) (flag ? 1 : 0)));
        }

        public bool hasKey(string s)
        {
            return tagMap.containsKey(s);
        }

        public byte getByte(string s)
        {
            if (!tagMap.containsKey(s))
            {
                return 0;
            }
            else
            {
                return ((NBTTagByte) tagMap.get(s)).byteValue;
            }
        }

        public short getShort(string s)
        {
            if (!tagMap.containsKey(s))
            {
                return 0;
            }
            else
            {
                return ((NBTTagShort) tagMap.get(s)).shortValue;
            }
        }

        public int getInteger(string s)
        {
            if (!tagMap.containsKey(s))
            {
                return 0;
            }
            else
            {
                return ((NBTTagInt) tagMap.get(s)).intValue;
            }
        }

        public long getLong(string s)
        {
            if (!tagMap.containsKey(s))
            {
                return 0L;
            }
            else
            {
                return ((NBTTagLong) tagMap.get(s)).longValue;
            }
        }

        public float getFloat(string s)
        {
            if (!tagMap.containsKey(s))
            {
                return 0.0F;
            }
            else
            {
                return ((NBTTagFloat) tagMap.get(s)).floatValue;
            }
        }

        public double getDouble(string s)
        {
            if (!tagMap.containsKey(s))
            {
                return 0.0D;
            }
            else
            {
                return ((NBTTagDouble) tagMap.get(s)).doubleValue;
            }
        }

        public string getString(string s)
        {
            if (!tagMap.containsKey(s))
            {
                return "";
            }
            else
            {
                return ((NBTTagString) tagMap.get(s)).stringValue;
            }
        }

        public byte[] getByteArray(string s)
        {
            if (!tagMap.containsKey(s))
            {
                return new byte[0];
            }
            else
            {
                return ((NBTTagByteArray) tagMap.get(s)).byteArray;
            }
        }

        public NBTTagCompound getCompoundTag(string s)
        {
            if (!tagMap.containsKey(s))
            {
                return new NBTTagCompound();
            }
            else
            {
                return (NBTTagCompound) tagMap.get(s);
            }
        }

        public NBTTagList getTagList(string s)
        {
            if (!tagMap.containsKey(s))
            {
                return new NBTTagList();
            }
            else
            {
                return (NBTTagList) tagMap.get(s);
            }
        }

        public bool getBoolean(string s)
        {
            return getByte(s) != 0;
        }

        public string toString()
        {
            return (new StringBuilder()).append("").append(tagMap.size()).append(" entries").toString();
        }
    }
}