using java.util;
using java.lang;


namespace CraftyServer.Core
{
    public abstract class EntityPlayer : EntityLiving
    {
        public EntityPlayer(World world) : base(world)
        {
            inventory = new InventoryPlayer(this);
            field_9152_am = 0;
            score = 0;
            isSwinging = false;
            swingProgressInt = 0;
            damageRemainder = 0;
            fishEntity = null;
            personalCraftingInventory = new CraftingInventoryPlayerCB(inventory, !world.singleplayerWorld);
            currentCraftingInventory = personalCraftingInventory;
            yOffset = 1.62F;
            ChunkCoordinates chunkcoordinates = world.func_22078_l();
            setLocationAndAngles((double) chunkcoordinates.posX + 0.5D, chunkcoordinates.posY + 1,
                                 (double) chunkcoordinates.posZ + 0.5D, 0.0F, 0.0F);
            health = 20;
            entityType = "humanoid";
            field_9117_aI = 180F;
            fireResistance = 20;
            texture = "/mob/char.png";
        }

        protected override void entityInit()
        {
            base.entityInit();
            dataWatcher.addObject(16, Byte.valueOf((byte) 0));
        }

        public override void onUpdate()
        {
            if (isPlayerSleeping())
            {
                sleepTimer++;
                if (sleepTimer > 100)
                {
                    sleepTimer = 100;
                }
                if (!inBed())
                {
                    wakeUpPlayer(true, true);
                }
                else if (!worldObj.singleplayerWorld && worldObj.isDaytime())
                {
                    wakeUpPlayer(false, true);
                }
            }
            else if (sleepTimer > 0)
            {
                sleepTimer++;
                if (sleepTimer >= 110)
                {
                    sleepTimer = 0;
                }
            }
            base.onUpdate();
            if (!worldObj.singleplayerWorld && currentCraftingInventory != null &&
                !currentCraftingInventory.canInteractWith(this))
            {
                usePersonalCraftingInventory();
                currentCraftingInventory = personalCraftingInventory;
            }
            field_20047_ay = field_20050_aB;
            field_20046_az = field_20049_aC;
            field_20051_aA = field_20048_aD;
            double d = posX - field_20050_aB;
            double d1 = posY - field_20049_aC;
            double d2 = posZ - field_20048_aD;
            double d3 = 10D;
            if (d > d3)
            {
                field_20047_ay = field_20050_aB = posX;
            }
            if (d2 > d3)
            {
                field_20051_aA = field_20048_aD = posZ;
            }
            if (d1 > d3)
            {
                field_20046_az = field_20049_aC = posY;
            }
            if (d < -d3)
            {
                field_20047_ay = field_20050_aB = posX;
            }
            if (d2 < -d3)
            {
                field_20051_aA = field_20048_aD = posZ;
            }
            if (d1 < -d3)
            {
                field_20046_az = field_20049_aC = posY;
            }
            field_20050_aB += d*0.25D;
            field_20048_aD += d2*0.25D;
            field_20049_aC += d1*0.25D;
        }

        protected override bool canMove()
        {
            return health <= 0 || isPlayerSleeping();
        }

        public virtual void usePersonalCraftingInventory()
        {
            currentCraftingInventory = personalCraftingInventory;
        }

        public override void updateRidden()
        {
            base.updateRidden();
            field_9150_ao = field_9149_ap;
            field_9149_ap = 0.0F;
        }

        public override void updatePlayerActionState()
        {
            if (isSwinging)
            {
                swingProgressInt++;
                if (swingProgressInt == 8)
                {
                    swingProgressInt = 0;
                    isSwinging = false;
                }
            }
            else
            {
                swingProgressInt = 0;
            }
            swingProgress = (float) swingProgressInt/8F;
        }

        public override void onLivingUpdate()
        {
            if (worldObj.difficultySetting == 0 && health < 20 && (ticksExisted%20)*12 == 0)
            {
                heal(1);
            }
            inventory.decrementAnimations();
            field_9150_ao = field_9149_ap;
            base.onLivingUpdate();
            float f = MathHelper.sqrt_double(motionX*motionX + motionZ*motionZ);
            float f1 = (float) Math.atan(-motionY*0.20000000298023224D)*15F;
            if (f > 0.1F)
            {
                f = 0.1F;
            }
            if (!onGround || health <= 0)
            {
                f = 0.0F;
            }
            if (onGround || health <= 0)
            {
                f1 = 0.0F;
            }
            field_9149_ap += (f - field_9149_ap)*0.4F;
            field_9101_aY += (f1 - field_9101_aY)*0.8F;
            if (health > 0)
            {
                List list = worldObj.getEntitiesWithinAABBExcludingEntity(this, boundingBox.expand(1.0D, 0.0D, 1.0D));
                if (list != null)
                {
                    for (int i = 0; i < list.size(); i++)
                    {
                        Entity entity = (Entity) list.get(i);
                        if (!entity.isDead)
                        {
                            func_171_h(entity);
                        }
                    }
                }
            }
        }

        private void func_171_h(Entity entity)
        {
            entity.onCollideWithPlayer(this);
        }

        public override void onDeath(Entity entity)
        {
            base.onDeath(entity);
            setSize(0.2F, 0.2F);
            setPosition(posX, posY, posZ);
            motionY = 0.10000000149011612D;
            if (username.Equals("Notch"))
            {
                dropPlayerItemWithRandomChoice(new ItemStack(Item.appleRed, 1), true);
            }
            inventory.dropAllItems();
            if (entity != null)
            {
                motionX = -MathHelper.cos(((attackedAtYaw + rotationYaw)*3.141593F)/180F)*0.1F;
                motionZ = -MathHelper.sin(((attackedAtYaw + rotationYaw)*3.141593F)/180F)*0.1F;
            }
            else
            {
                motionX = motionZ = 0.0D;
            }
            yOffset = 0.1F;
        }

        public override void addToPlayerScore(Entity entity, int i)
        {
            score += i;
        }

        public void func_161_a()
        {
            dropPlayerItemWithRandomChoice(inventory.decrStackSize(inventory.currentItem, 1), false);
        }

        public void dropPlayerItem(ItemStack itemstack)
        {
            dropPlayerItemWithRandomChoice(itemstack, false);
        }

        public void dropPlayerItemWithRandomChoice(ItemStack itemstack, bool flag)
        {
            if (itemstack == null)
            {
                return;
            }
            EntityItem entityitem = new EntityItem(worldObj, posX,
                                                   (posY - 0.30000001192092896D) + (double) getEyeHeight(), posZ,
                                                   itemstack);
            entityitem.delayBeforeCanPickup = 40;
            float f = 0.1F;
            if (flag)
            {
                float f2 = rand.nextFloat()*0.5F;
                float f4 = rand.nextFloat()*3.141593F*2.0F;
                entityitem.motionX = -MathHelper.sin(f4)*f2;
                entityitem.motionZ = MathHelper.cos(f4)*f2;
                entityitem.motionY = 0.20000000298023224D;
            }
            else
            {
                float f1 = 0.3F;
                entityitem.motionX = -MathHelper.sin((rotationYaw/180F)*3.141593F)*
                                     MathHelper.cos((rotationPitch/180F)*3.141593F)*f1;
                entityitem.motionZ = MathHelper.cos((rotationYaw/180F)*3.141593F)*
                                     MathHelper.cos((rotationPitch/180F)*3.141593F)*f1;
                entityitem.motionY = -MathHelper.sin((rotationPitch/180F)*3.141593F)*f1 + 0.1F;
                f1 = 0.02F;
                float f3 = rand.nextFloat()*3.141593F*2.0F;
                f1 *= rand.nextFloat();
                entityitem.motionX += Math.cos(f3)*(double) f1;
                entityitem.motionY += (rand.nextFloat() - rand.nextFloat())*0.1F;
                entityitem.motionZ += Math.sin(f3)*(double) f1;
            }
            joinEntityItemWithWorld(entityitem);
        }

        public void joinEntityItemWithWorld(EntityItem entityitem)
        {
            worldObj.entityJoinedWorld(entityitem);
        }

        public float getCurrentPlayerStrVsBlock(Block block)
        {
            float f = inventory.getStrVsBlock(block);
            if (isInsideOfMaterial(Material.water))
            {
                f /= 5F;
            }
            if (!onGround)
            {
                f /= 5F;
            }
            return f;
        }

        public virtual bool canHarvestBlock(Block block)
        {
            return inventory.canHarvestBlock(block);
        }

        public override void readEntityFromNBT(NBTTagCompound nbttagcompound)
        {
            base.readEntityFromNBT(nbttagcompound);
            NBTTagList nbttaglist = nbttagcompound.getTagList("Inventory");
            inventory.readFromNBT(nbttaglist);
            dimension = nbttagcompound.getInteger("Dimension");
            sleeping = nbttagcompound.getBoolean("Sleeping");
            sleepTimer = nbttagcompound.getShort("SleepTimer");
            if (sleeping)
            {
                playerLocation = new ChunkCoordinates(MathHelper.floor_double(posX), MathHelper.floor_double(posY),
                                                      MathHelper.floor_double(posZ));
                wakeUpPlayer(true, true);
            }
        }

        public override void writeEntityToNBT(NBTTagCompound nbttagcompound)
        {
            base.writeEntityToNBT(nbttagcompound);
            nbttagcompound.setTag("Inventory", inventory.writeToNBT(new NBTTagList()));
            nbttagcompound.setInteger("Dimension", dimension);
            nbttagcompound.setBoolean("Sleeping", sleeping);
            nbttagcompound.setShort("SleepTimer", (short) sleepTimer);
        }

        public virtual void displayGUIChest(IInventory iinventory)
        {
        }

        public virtual void displayWorkbenchGUI(int i, int j, int k)
        {
        }

        public virtual void onItemPickup(Entity entity, int i)
        {
        }

        public override float getEyeHeight()
        {
            return 0.12F;
        }

        public virtual void resetHeight()
        {
            yOffset = 1.62F;
        }

        public override bool attackEntityFrom(Entity entity, int i)
        {
            age = 0;
            if (health <= 0)
            {
                return false;
            }
            if (isPlayerSleeping())
            {
                wakeUpPlayer(true, true);
            }
            if ((entity is EntityMobs) || (entity is EntityArrow))
            {
                if (worldObj.difficultySetting == 0)
                {
                    i = 0;
                }
                if (worldObj.difficultySetting == 1)
                {
                    i = i/3 + 1;
                }
                if (worldObj.difficultySetting == 3)
                {
                    i = (i*3)/2;
                }
            }
            if (i == 0)
            {
                return false;
            }
            else
            {
                return base.attackEntityFrom(entity, i);
            }
        }

        public override void damageEntity(int i)
        {
            int j = 25 - inventory.getTotalArmorValue();
            int k = i*j + damageRemainder;
            inventory.damageArmor(i);
            i = k/25;
            damageRemainder = k%25;
            base.damageEntity(i);
        }

        public virtual void displayGUIFurnace(TileEntityFurnace tileentityfurnace)
        {
        }

        public virtual void displayGUIDispenser(TileEntityDispenser tileentitydispenser)
        {
        }

        public virtual void displayGUIEditSign(TileEntitySign tileentitysign)
        {
        }

        public virtual void useCurrentItemOnEntity(Entity entity)
        {
            if (entity.interact(this))
            {
                return;
            }
            ItemStack itemstack = getCurrentEquippedItem();
            if (itemstack != null && (entity is EntityLiving))
            {
                itemstack.useItemOnEntity((EntityLiving) entity);
                if (itemstack.stackSize <= 0)
                {
                    itemstack.func_577_a(this);
                    destroyCurrentEquippedItem();
                }
            }
        }

        public ItemStack getCurrentEquippedItem()
        {
            return inventory.getCurrentItem();
        }

        public void destroyCurrentEquippedItem()
        {
            inventory.setInventorySlotContents(inventory.currentItem, null);
        }

        public override double getYOffset()
        {
            return (double) (yOffset - 0.5F);
        }

        public virtual void swingItem()
        {
            swingProgressInt = -1;
            isSwinging = true;
        }

        public void attackTargetEntityWithCurrentItem(Entity entity)
        {
            int i = inventory.getDamageVsEntity(entity);
            if (i > 0)
            {
                entity.attackEntityFrom(this, i);
                ItemStack itemstack = getCurrentEquippedItem();
                if (itemstack != null && (entity is EntityLiving))
                {
                    itemstack.hitEntity((EntityLiving) entity);
                    if (itemstack.stackSize <= 0)
                    {
                        itemstack.func_577_a(this);
                        destroyCurrentEquippedItem();
                    }
                }
            }
        }

        public virtual void onItemStackChanged(ItemStack itemstack)
        {
        }

        public override void setEntityDead()
        {
            base.setEntityDead();
            personalCraftingInventory.onCraftGuiClosed(this);
            if (currentCraftingInventory != null)
            {
                currentCraftingInventory.onCraftGuiClosed(this);
            }
        }

        public override bool func_91_u()
        {
            return !sleeping && base.func_91_u();
        }

        public virtual bool goToSleep(int i, int j, int k)
        {
            if (isPlayerSleeping() || !isEntityAlive())
            {
                return false;
            }
            if (worldObj.worldProvider.field_6167_c)
            {
                return false;
            }
            if (worldObj.isDaytime())
            {
                return false;
            }
            if (Math.abs(posX - (double) i) > 3D || Math.abs(posY - (double) j) > 2D || Math.abs(posZ - (double) k) > 3D)
            {
                return false;
            }
            setSize(0.2F, 0.2F);
            yOffset = 0.2F;
            if (worldObj.blockExists(i, j, k))
            {
                int l = worldObj.getBlockMetadata(i, j, k);
                int i1 = BlockBed.func_22019_c(l);
                float f = 0.5F;
                float f1 = 0.5F;
                switch (i1)
                {
                    case 0: // '\0'
                        f1 = 0.9F;
                        break;

                    case 2: // '\002'
                        f1 = 0.1F;
                        break;

                    case 1: // '\001'
                        f = 0.1F;
                        break;

                    case 3: // '\003'
                        f = 0.9F;
                        break;
                }
                func_22059_e(i1);
                setPosition((float) i + f, (float) j + 0.9375F, (float) k + f1);
            }
            else
            {
                setPosition((float) i + 0.5F, (float) j + 0.9375F, (float) k + 0.5F);
            }
            sleeping = true;
            sleepTimer = 0;
            playerLocation = new ChunkCoordinates(i, j, k);
            motionX = motionZ = motionY = 0.0D;
            if (!worldObj.singleplayerWorld)
            {
                worldObj.updateAllPlayersSleepingFlag();
            }
            return true;
        }

        private void func_22059_e(int i)
        {
            field_22066_z = 0.0F;
            field_22067_A = 0.0F;
            switch (i)
            {
                case 0: // '\0'
                    field_22067_A = -1.8F;
                    break;

                case 2: // '\002'
                    field_22067_A = 1.8F;
                    break;

                case 1: // '\001'
                    field_22066_z = 1.8F;
                    break;

                case 3: // '\003'
                    field_22066_z = -1.8F;
                    break;
            }
        }

        public virtual void wakeUpPlayer(bool flag, bool flag1)
        {
            setSize(0.6F, 1.8F);
            resetHeight();
            ChunkCoordinates chunkcoordinates = playerLocation;
            if (chunkcoordinates != null &&
                worldObj.getBlockId(chunkcoordinates.posX, chunkcoordinates.posY, chunkcoordinates.posZ) ==
                Block.bed.blockID)
            {
                BlockBed.func_22022_a(worldObj, chunkcoordinates.posX, chunkcoordinates.posY, chunkcoordinates.posZ,
                                      false);
                ChunkCoordinates chunkcoordinates1 = BlockBed.func_22021_g(worldObj, chunkcoordinates.posX,
                                                                           chunkcoordinates.posY, chunkcoordinates.posZ,
                                                                           0);
                setPosition((float) chunkcoordinates1.posX + 0.5F, (float) chunkcoordinates1.posY + yOffset + 0.1F,
                            (float) chunkcoordinates1.posZ + 0.5F);
            }
            sleeping = false;
            if (!worldObj.singleplayerWorld && flag1)
            {
                worldObj.updateAllPlayersSleepingFlag();
            }
            if (flag)
            {
                sleepTimer = 0;
            }
            else
            {
                sleepTimer = 100;
            }
        }

        private bool inBed()
        {
            return worldObj.getBlockId(playerLocation.posX, playerLocation.posY, playerLocation.posZ) ==
                   Block.bed.blockID;
        }

        public override bool isPlayerSleeping()
        {
            return sleeping;
        }

        public bool isPlayerFullyAsleep()
        {
            return sleeping && sleepTimer >= 100;
        }

        public void func_22061_a(string s)
        {
        }

        public InventoryPlayer inventory;
        public CraftingInventoryCB personalCraftingInventory;
        public CraftingInventoryCB currentCraftingInventory;
        public byte field_9152_am;
        public int score;
        public float field_9150_ao;
        public float field_9149_ap;
        public bool isSwinging;
        public int swingProgressInt;
        public string username;
        public int dimension;
        public double field_20047_ay;
        public double field_20046_az;
        public double field_20051_aA;
        public double field_20050_aB;
        public double field_20049_aC;
        public double field_20048_aD;
        private bool sleeping;
        private ChunkCoordinates playerLocation;
        private int sleepTimer;
        public float field_22066_z;
        public float field_22067_A;
        private int damageRemainder;
        public EntityFish fishEntity;
    }
}