using java.util;
using java.lang;

namespace CraftyServer.Core
{
    public class TileEntity
    {
        public TileEntity()
        {
        }

        private static void addMapping(Class class1, string s)
        {
            if (classToNameMap.containsKey(s))
            {
                throw new IllegalArgumentException((new StringBuilder()).append("Duplicate id: ").append(s).toString());
            }
            else
            {
                nameToClassMap.put(s, class1);
                classToNameMap.put(class1, s);
                return;
            }
        }

        public virtual void readFromNBT(NBTTagCompound nbttagcompound)
        {
            xCoord = nbttagcompound.getInteger("x");
            yCoord = nbttagcompound.getInteger("y");
            zCoord = nbttagcompound.getInteger("z");
        }

        public virtual void writeToNBT(NBTTagCompound nbttagcompound)
        {
            string s = (string) classToNameMap.get((Class) GetType());
            if (s == null)
            {
                throw new RuntimeException(
                    (new StringBuilder()).append(GetType()).append(" is missing a mapping! This is a bug!").toString());
            }
            else
            {
                nbttagcompound.setString("id", s);
                nbttagcompound.setInteger("x", xCoord);
                nbttagcompound.setInteger("y", yCoord);
                nbttagcompound.setInteger("z", zCoord);
                return;
            }
        }

        public virtual void updateEntity()
        {
        }

        public static TileEntity createAndLoadEntity(NBTTagCompound nbttagcompound)
        {
            TileEntity tileentity = null;
            try
            {
                Class class1 = (Class) nameToClassMap.get(nbttagcompound.getString("id"));
                if (class1 != null)
                {
                    tileentity = (TileEntity) class1.newInstance();
                }
            }
            catch (Exception exception)
            {
                exception.printStackTrace();
            }
            if (tileentity != null)
            {
                tileentity.readFromNBT(nbttagcompound);
            }
            else
            {
                java.lang.System.@out.println(
                    (new StringBuilder()).append("Skipping TileEntity with id ").append(nbttagcompound.getString("id")).
                        toString());
            }
            return tileentity;
        }

        public virtual void onInventoryChanged()
        {
            if (worldObj != null)
            {
                worldObj.func_515_b(xCoord, yCoord, zCoord, this);
            }
        }

        public virtual Packet getDescriptionPacket()
        {
            return null;
        }


        private static Map nameToClassMap = new HashMap();
        private static Map classToNameMap = new HashMap();
        public World worldObj;
        public int xCoord;
        public int yCoord;
        public int zCoord;

        static TileEntity()
        {
            addMapping(typeof (TileEntityFurnace), "Furnace");
            addMapping(typeof (TileEntityChest), "Chest");
            addMapping(typeof (TileEntityDispenser), "Trap");
            addMapping(typeof (TileEntitySign), "Sign");
            addMapping(typeof (TileEntityMobSpawner), "MobSpawner");
            addMapping(typeof (TileEntityNote), "Music");
        }
    }
}