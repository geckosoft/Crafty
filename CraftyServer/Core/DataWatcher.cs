using java.io;
using java.lang;
using java.util;
using Exception = System.Exception;

namespace CraftyServer.Core
{
    public class DataWatcher
    {
        private static readonly HashMap dataTypes;
        private readonly Map watchedObjects = new HashMap();
        private bool objectChanged;

        static DataWatcher()
        {
            dataTypes = new HashMap();
            dataTypes.put(typeof (Byte), Integer.valueOf(0));
            dataTypes.put(typeof (sbyte), Integer.valueOf(0));
            dataTypes.put(typeof (Short), Integer.valueOf(1));
            dataTypes.put(typeof (Integer), Integer.valueOf(2));
            dataTypes.put(typeof (Float), Integer.valueOf(3));
            dataTypes.put(typeof (String), Integer.valueOf(4));
            dataTypes.put(typeof (ItemStack), Integer.valueOf(5));
            dataTypes.put(typeof (ChunkCoordinates), Integer.valueOf(6));
        }

        public void addObject(int i, object obj)
        {
            var integer = (Integer) dataTypes.get(obj.GetType());
            if (integer == null)
            {
                throw new IllegalArgumentException(
                    (new StringBuilder()).append("Unknown data type: ").append(obj.GetType()).toString());
            }
            if (i > 31)
            {
                throw new IllegalArgumentException(
                    (new StringBuilder()).append("Data value id is too big with ").append(i).append("! (Max is ").append
                        (31).append(")").toString());
            }
            if (watchedObjects.containsKey(Integer.valueOf(i)))
            {
                throw new IllegalArgumentException(
                    (new StringBuilder()).append("Duplicate id value for ").append(i).append("!").toString());
            }
            else
            {
                var watchableobject = new WatchableObject(integer.intValue(), i, obj);
                watchedObjects.put(Integer.valueOf(i), watchableobject);
                return;
            }
        }

        public byte getWatchableObjectByte(int i)
        {
            try
            {
                return ((Byte) ((WatchableObject) watchedObjects.get(Integer.valueOf(i))).getObject()).byteValue();
            }
            catch (Exception ex)
            {
                return (byte) ((WatchableObject) watchedObjects.get(Integer.valueOf(i))).getObject();
            }
        }

        public sbyte getWatchableObjectSByte(int i)
        {
            return (sbyte) ((WatchableObject) watchedObjects.get(Integer.valueOf(i))).getObject();
        }

        public void updateObject(int i, object obj)
        {
            var watchableobject = (WatchableObject) watchedObjects.get(Integer.valueOf(i));
            if (!obj.Equals(watchableobject.getObject()))
            {
                watchableobject.setObject(obj);
                watchableobject.setWatching(true);
                objectChanged = true;
            }
        }

        public bool hasObjectChanged()
        {
            return objectChanged;
        }

        public static void writeObjectsInListToStream(List list, DataOutputStream dataoutputstream)
        {
            if (list != null)
            {
                WatchableObject watchableobject;
                for (Iterator iterator = list.iterator();
                     iterator.hasNext();
                     writeWatchableObject(dataoutputstream, watchableobject))
                {
                    watchableobject = (WatchableObject) iterator.next();
                }
            }
            dataoutputstream.writeByte(127);
        }

        public ArrayList getChangedObjects()
        {
            ArrayList arraylist = null;
            if (objectChanged)
            {
                Iterator iterator = watchedObjects.values().iterator();
                do
                {
                    if (!iterator.hasNext())
                    {
                        break;
                    }
                    var watchableobject = (WatchableObject) iterator.next();
                    if (watchableobject.getWatching())
                    {
                        watchableobject.setWatching(false);
                        if (arraylist == null)
                        {
                            arraylist = new ArrayList();
                        }
                        arraylist.add(watchableobject);
                    }
                } while (true);
            }
            objectChanged = false;
            return arraylist;
        }

        public void writeWatchableObjects(DataOutputStream dataoutputstream)
        {
            WatchableObject watchableobject;
            for (Iterator iterator = watchedObjects.values().iterator();
                 iterator.hasNext();
                 writeWatchableObject(dataoutputstream, watchableobject))
            {
                watchableobject = (WatchableObject) iterator.next();
            }

            dataoutputstream.writeByte(127);
        }

        private static void writeWatchableObject(DataOutputStream dataoutputstream, WatchableObject watchableobject)
        {
            int i = (watchableobject.getObjectType() << 5 | watchableobject.getDataValueId() & 0x1f) & 0xff;
            dataoutputstream.writeByte(i);
            switch (watchableobject.getObjectType())
            {
                case 0: // '\0'
                    try
                    {
                        dataoutputstream.writeByte(((Byte) watchableobject.getObject()).byteValue());
                    }
                    catch
                    {
                        dataoutputstream.writeByte((sbyte) (watchableobject.getObject()));
                    }
                    break;

                case 1: // '\001'
                    dataoutputstream.writeShort(((Short) watchableobject.getObject()).shortValue());
                    break;

                case 2: // '\002'
                    dataoutputstream.writeInt(((Integer) watchableobject.getObject()).intValue());
                    break;

                case 3: // '\003'
                    dataoutputstream.writeFloat(((Float) watchableobject.getObject()).floatValue());
                    break;

                case 4: // '\004'
                    dataoutputstream.writeUTF((string) watchableobject.getObject());
                    break;

                case 5: // '\005'
                    var itemstack = (ItemStack) watchableobject.getObject();
                    dataoutputstream.writeShort(itemstack.getItem().shiftedIndex);
                    dataoutputstream.writeByte(itemstack.stackSize);
                    dataoutputstream.writeShort(itemstack.getItemDamage());
                    // fall through (cant.. c# ...)
                    var chunkcoordinates2 = (ChunkCoordinates) watchableobject.getObject();
                    dataoutputstream.writeInt(chunkcoordinates2.posX);
                    dataoutputstream.writeInt(chunkcoordinates2.posY);
                    dataoutputstream.writeInt(chunkcoordinates2.posZ);

                    break;
                case 6: // '\006'
                    var chunkcoordinates = (ChunkCoordinates) watchableobject.getObject();
                    dataoutputstream.writeInt(chunkcoordinates.posX);
                    dataoutputstream.writeInt(chunkcoordinates.posY);
                    dataoutputstream.writeInt(chunkcoordinates.posZ);
                    break;
            }
        }

        public static List readWatchableObjects(DataInputStream datainputstream)
        {
            ArrayList arraylist = null;
            for (byte byte0 = datainputstream.readByte(); byte0 != 127; byte0 = datainputstream.readByte())
            {
                if (arraylist == null)
                {
                    arraylist = new ArrayList();
                }
                int i = (byte0 & 0xe0) >> 5;
                int j = byte0 & 0x1f;
                WatchableObject watchableobject = null;
                switch (i)
                {
                    case 0: // '\0'
                        watchableobject = new WatchableObject(i, j, Byte.valueOf(datainputstream.readByte()));
                        break;

                    case 1: // '\001'
                        watchableobject = new WatchableObject(i, j, Short.valueOf(datainputstream.readShort()));
                        break;

                    case 2: // '\002'
                        watchableobject = new WatchableObject(i, j, Integer.valueOf(datainputstream.readInt()));
                        break;

                    case 3: // '\003'
                        watchableobject = new WatchableObject(i, j, Float.valueOf(datainputstream.readFloat()));
                        break;

                    case 4: // '\004'
                        watchableobject = new WatchableObject(i, j, datainputstream.readUTF());
                        break;

                    case 5: // '\005'
                        short word0 = datainputstream.readShort();
                        byte byte1 = datainputstream.readByte();
                        short word1 = datainputstream.readShort();
                        watchableobject = new WatchableObject(i, j, new ItemStack(word0, byte1, word1));

                        // fall through (not.. c#..)

                        int k2 = datainputstream.readInt();
                        int l2 = datainputstream.readInt();
                        int i12 = datainputstream.readInt();
                        watchableobject = new WatchableObject(i, j, new ChunkCoordinates(k2, l2, i12));

                        break;
                    case 6: // '\006'
                        int k = datainputstream.readInt();
                        int l = datainputstream.readInt();
                        int i1 = datainputstream.readInt();
                        watchableobject = new WatchableObject(i, j, new ChunkCoordinates(k, l, i1));
                        break;
                }
                arraylist.add(watchableobject);
            }

            return arraylist;
        }
    }
}