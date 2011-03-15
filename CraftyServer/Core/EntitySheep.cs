using java.lang;
using java.util;

namespace CraftyServer.Core
{
    public class EntitySheep : EntityAnimals
    {
        public static float[,] field_21071_a = new[,]
                                               {
                                                   {
                                                       1.0F, 1.0F, 1.0F
                                                   }, {
                                                          0.95F, 0.7F, 0.2F
                                                      }, {
                                                             0.9F, 0.5F, 0.85F
                                                         }, {
                                                                0.6F, 0.7F, 0.95F
                                                            }, {
                                                                   0.9F, 0.9F, 0.2F
                                                               }, {
                                                                      0.5F, 0.8F, 0.1F
                                                                  }, {
                                                                         0.95F, 0.7F, 0.8F
                                                                     }, {
                                                                            0.3F, 0.3F, 0.3F
                                                                        }, {
                                                                               0.6F, 0.6F, 0.6F
                                                                           }, {
                                                                                  0.3F, 0.6F, 0.7F
                                                                              }, {
                                                                                     0.7F, 0.4F, 0.9F
                                                                                 }, {
                                                                                        0.2F, 0.4F, 0.8F
                                                                                    }, {
                                                                                           0.5F, 0.4F, 0.3F
                                                                                       }, {
                                                                                              0.4F, 0.5F, 0.2F
                                                                                          }, {
                                                                                                 0.8F, 0.3F, 0.3F
                                                                                             }, {
                                                                                                    0.1F, 0.1F, 0.1F
                                                                                                }
                                               };

        public EntitySheep(World world)
            : base(world)
        {
            texture = "/mob/sheep.png";
            setSize(0.9F, 1.3F);
        }

        protected override void entityInit()
        {
            base.entityInit();
            dataWatcher.addObject(16, new Byte(0));
        }

        public override bool attackEntityFrom(Entity entity, int i)
        {
            if (!worldObj.singleplayerWorld && !func_21069_f_() && (entity is EntityLiving))
            {
                setSheared(true);
                int j = 1 + rand.nextInt(3);
                for (int k = 0; k < j; k++)
                {
                    EntityItem entityitem = entityDropItem(new ItemStack(Block.cloth.blockID, 1, getFleeceColor()), 1.0F);
                    entityitem.motionY += rand.nextFloat()*0.05F;
                    entityitem.motionX += (rand.nextFloat() - rand.nextFloat())*0.1F;
                    entityitem.motionZ += (rand.nextFloat() - rand.nextFloat())*0.1F;
                }
            }
            return base.attackEntityFrom(entity, i);
        }

        public override void writeEntityToNBT(NBTTagCompound nbttagcompound)
        {
            base.writeEntityToNBT(nbttagcompound);
            nbttagcompound.setBoolean("Sheared", func_21069_f_());
            nbttagcompound.setByte("Color", (byte) getFleeceColor());
        }

        public override void readEntityFromNBT(NBTTagCompound nbttagcompound)
        {
            base.readEntityFromNBT(nbttagcompound);
            setSheared(nbttagcompound.getBoolean("Sheared"));
            setFleeceColor(nbttagcompound.getByte("Color"));
        }

        protected override string getLivingSound()
        {
            return "mob.sheep";
        }

        protected override string getHurtSound()
        {
            return "mob.sheep";
        }

        protected override string getDeathSound()
        {
            return "mob.sheep";
        }

        public int getFleeceColor()
        {
            return dataWatcher.getWatchableObjectByte(16) & 0xf;
        }

        public void setFleeceColor(int i)
        {
            byte byte0 = dataWatcher.getWatchableObjectByte(16);
            dataWatcher.updateObject(16, Byte.valueOf((byte) (byte0 & 0xf0 | i & 0xf)));
        }

        public bool func_21069_f_()
        {
            return (dataWatcher.getWatchableObjectByte(16) & 0x10) != 0;
        }

        public void setSheared(bool flag)
        {
            byte byte0 = dataWatcher.getWatchableObjectByte(16);
            if (flag)
            {
                dataWatcher.updateObject(16, Byte.valueOf((byte) (byte0 | 0x10)));
            }
            else
            {
                dataWatcher.updateObject(16, Byte.valueOf((byte) (byte0 & 0xffffffef)));
            }
        }

        public static int func_21066_a(Random random)
        {
            int i = random.nextInt(100);
            if (i < 5)
            {
                return 15;
            }
            if (i < 10)
            {
                return 7;
            }
            return i >= 15 ? 0 : 8;
        }
    }
}