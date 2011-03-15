using java.util;

namespace CraftyServer.Core
{
    public class EntityPainting : Entity
    {
        public EnumArt art;
        public int direction;
        private int field_452_ad;
        public int xPosition;
        public int yPosition;
        public int zPosition;

        public EntityPainting(World world) : base(world)
        {
            field_452_ad = 0;
            direction = 0;
            yOffset = 0.0F;
            setSize(0.5F, 0.5F);
        }

        public EntityPainting(World world, int i, int j, int k, int l) : this(world)
        {
            xPosition = i;
            yPosition = j;
            zPosition = k;
            var arraylist = new ArrayList();
            EnumArt[] aenumart = EnumArt.values();
            int i1 = aenumart.Length;
            for (int j1 = 0; j1 < i1; j1++)
            {
                EnumArt enumart = aenumart[j1];
                art = enumart;
                func_179_a(l);
                if (onValidSurface())
                {
                    arraylist.add(enumart);
                }
            }

            if (arraylist.size() > 0)
            {
                art = (EnumArt) arraylist.get(rand.nextInt(arraylist.size()));
            }
            func_179_a(l);
        }

        protected override void entityInit()
        {
        }

        public void func_179_a(int i)
        {
            direction = i;
            prevRotationYaw = rotationYaw = i*90;
            float f = art.sizeX;
            float f1 = art.sizeY;
            float f2 = art.sizeX;
            if (i == 0 || i == 2)
            {
                f2 = 0.5F;
            }
            else
            {
                f = 0.5F;
            }
            f /= 32F;
            f1 /= 32F;
            f2 /= 32F;
            float f3 = xPosition + 0.5F;
            float f4 = yPosition + 0.5F;
            float f5 = zPosition + 0.5F;
            float f6 = 0.5625F;
            if (i == 0)
            {
                f5 -= f6;
            }
            if (i == 1)
            {
                f3 -= f6;
            }
            if (i == 2)
            {
                f5 += f6;
            }
            if (i == 3)
            {
                f3 += f6;
            }
            if (i == 0)
            {
                f3 -= func_180_c(art.sizeX);
            }
            if (i == 1)
            {
                f5 += func_180_c(art.sizeX);
            }
            if (i == 2)
            {
                f3 += func_180_c(art.sizeX);
            }
            if (i == 3)
            {
                f5 -= func_180_c(art.sizeX);
            }
            f4 += func_180_c(art.sizeY);
            setPosition(f3, f4, f5);
            float f7 = -0.00625F;
            boundingBox.setBounds(f3 - f - f7, f4 - f1 - f7, f5 - f2 - f7, f3 + f + f7, f4 + f1 + f7, f5 + f2 + f7);
        }

        private float func_180_c(int i)
        {
            if (i == 32)
            {
                return 0.5F;
            }
            return i != 64 ? 0.0F : 0.5F;
        }

        public override void onUpdate()
        {
            if (field_452_ad++ == 100 && !worldObj.singleplayerWorld)
            {
                field_452_ad = 0;
                if (!onValidSurface())
                {
                    setEntityDead();
                    worldObj.entityJoinedWorld(new EntityItem(worldObj, posX, posY, posZ, new ItemStack(Item.painting)));
                }
            }
        }

        public bool onValidSurface()
        {
            if (worldObj.getCollidingBoundingBoxes(this, boundingBox).size() > 0)
            {
                return false;
            }
            int i = art.sizeX/16;
            int j = art.sizeY/16;
            int k = xPosition;
            int l = yPosition;
            int i1 = zPosition;
            if (direction == 0)
            {
                k = MathHelper.floor_double(posX - (art.sizeX/32F));
            }
            if (direction == 1)
            {
                i1 = MathHelper.floor_double(posZ - (art.sizeX/32F));
            }
            if (direction == 2)
            {
                k = MathHelper.floor_double(posX - (art.sizeX/32F));
            }
            if (direction == 3)
            {
                i1 = MathHelper.floor_double(posZ - (art.sizeX/32F));
            }
            l = MathHelper.floor_double(posY - (art.sizeY/32F));
            for (int j1 = 0; j1 < i; j1++)
            {
                for (int k1 = 0; k1 < j; k1++)
                {
                    Material material;
                    if (direction == 0 || direction == 2)
                    {
                        material = worldObj.getBlockMaterial(k + j1, l + k1, zPosition);
                    }
                    else
                    {
                        material = worldObj.getBlockMaterial(xPosition, l + k1, i1 + j1);
                    }
                    if (!material.isSolid())
                    {
                        return false;
                    }
                }
            }

            List list = worldObj.getEntitiesWithinAABBExcludingEntity(this, boundingBox);
            for (int l1 = 0; l1 < list.size(); l1++)
            {
                if (list.get(l1) is EntityPainting)
                {
                    return false;
                }
            }

            return true;
        }

        public override bool canBeCollidedWith()
        {
            return true;
        }

        public override bool attackEntityFrom(Entity entity, int i)
        {
            if (!isDead && !worldObj.singleplayerWorld)
            {
                setEntityDead();
                setBeenAttacked();
                worldObj.entityJoinedWorld(new EntityItem(worldObj, posX, posY, posZ, new ItemStack(Item.painting)));
            }
            return true;
        }

        public override void writeEntityToNBT(NBTTagCompound nbttagcompound)
        {
            nbttagcompound.setByte("Dir", (byte) direction);
            nbttagcompound.setString("Motive", art.title);
            nbttagcompound.setInteger("TileX", xPosition);
            nbttagcompound.setInteger("TileY", yPosition);
            nbttagcompound.setInteger("TileZ", zPosition);
        }

        public override void readEntityFromNBT(NBTTagCompound nbttagcompound)
        {
            direction = nbttagcompound.getByte("Dir");
            xPosition = nbttagcompound.getInteger("TileX");
            yPosition = nbttagcompound.getInteger("TileY");
            zPosition = nbttagcompound.getInteger("TileZ");
            string s = nbttagcompound.getString("Motive");
            EnumArt[] aenumart = EnumArt.values();
            int i = aenumart.Length;
            for (int j = 0; j < i; j++)
            {
                EnumArt enumart = aenumart[j];
                if (enumart.title.Equals(s))
                {
                    art = enumart;
                }
            }

            if (art == null)
            {
                art = EnumArt.Kebab;
            }
            func_179_a(direction);
        }
    }
}