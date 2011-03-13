using java.util;
using java.lang;

namespace CraftyServer.Core
{
    public class EntityList
    {
        public EntityList()
        {
        }

        private static void addMapping(Class class1, string s, int i)
        {
            stringToClassMapping.put(s, class1);
            classToStringMapping.put(class1, s);
            IDtoClassMapping.put(java.lang.Integer.valueOf(i), class1);
            classToIDMapping.put(class1, java.lang.Integer.valueOf(i));
        }

        public static Entity createEntityInWorld(string s, World world)
        {
            Entity entity = null;
            try
            {
                Class class1 = (Class) stringToClassMapping.get(s);
                if (class1 != null)
                {
                    entity = (Entity) class1.getConstructor(new Class[]
                                                            {
                                                                typeof (World)
                                                            }).newInstance(new object[]
                                                                           {
                                                                               world
                                                                           });
                }
            }
            catch (Exception exception)
            {
                exception.printStackTrace();
            }
            return entity;
        }

        public static Entity createEntityFromNBT(NBTTagCompound nbttagcompound, World world)
        {
            Entity entity = null;
            try
            {
                Class class1 = (Class) stringToClassMapping.get(nbttagcompound.getString("id"));
                if (class1 != null)
                {
                    entity = (Entity) class1.getConstructor(new Class[]
                                                            {
                                                                typeof (World)
                                                            }).newInstance(new object[]
                                                                           {
                                                                               world
                                                                           });
                }
            }
            catch (Exception exception)
            {
                exception.printStackTrace();
            }
            if (entity != null)
            {
                entity.readFromNBT(nbttagcompound);
            }
            else
            {
                java.lang.System.@out.println(
                    (new StringBuilder()).append("Skipping Entity with id ").append(nbttagcompound.getString("id")).
                        toString());
            }
            return entity;
        }

        public static int getEntityID(Entity entity)
        {
            return ((java.lang.Integer) classToIDMapping.get((Class) entity.GetType())).intValue();
        }

        public static string getEntityString(Entity entity)
        {
            return (string) classToStringMapping.get((Class) entity.GetType());
        }

        private static Map stringToClassMapping = new HashMap();
        private static Map classToStringMapping = new HashMap();
        private static Map IDtoClassMapping = new HashMap();
        private static Map classToIDMapping = new HashMap();

        static EntityList()
        {
            addMapping(typeof (EntityArrow), "Arrow", 10);
            addMapping(typeof (EntitySnowball), "Snowball", 11);
            addMapping(typeof (EntityItem), "Item", 1);
            addMapping(typeof (EntityPainting), "Painting", 9);
            addMapping(typeof (EntityLiving), "Mob", 48);
            addMapping(typeof (EntityMobs), "Monster", 49);
            addMapping(typeof (EntityCreeper), "Creeper", 50);
            addMapping(typeof (EntitySkeleton), "Skeleton", 51);
            addMapping(typeof (EntitySpider), "Spider", 52);
            addMapping(typeof (EntityZombieSimple), "Giant", 53);
            addMapping(typeof (EntityZombie), "Zombie", 54);
            addMapping(typeof (EntitySlime), "Slime", 55);
            addMapping(typeof (EntityGhast), "Ghast", 56);
            addMapping(typeof (EntityPigZombie), "PigZombie", 57);
            addMapping(typeof (EntityPig), "Pig", 90);
            addMapping(typeof (EntitySheep), "Sheep", 91);
            addMapping(typeof (EntityCow), "Cow", 92);
            addMapping(typeof (EntityChicken), "Chicken", 93);
            addMapping(typeof (EntitySquid), "Squid", 94);
            addMapping(typeof (EntityTNTPrimed), "PrimedTnt", 20);
            addMapping(typeof (EntityFallingSand), "FallingSand", 21);
            addMapping(typeof (EntityMinecart), "Minecart", 40);
            addMapping(typeof (EntityBoat), "Boat", 41);
        }
    }
}