using CraftyServer.Server;
using java.util;

namespace CraftyServer.Core
{
    public class EntityPlayerMP : EntityPlayer
                                  , ICrafting
    {
        private readonly ItemStack[] playerInventory = {
                                                           null, null, null, null, null
                                                       };

        private int currentWindowId;
        private int field_15004_bw;
        public Set field_420_ah;
        public double field_9154_e;
        public double field_9155_d;
        public bool isChangingQuantityOnly;
        public ItemInWorldManager itemInWorldManager;
        private int lastHealth;
        public List loadedChunks;
        public MinecraftServer mcServer;
        public NetServerHandler playerNetServerHandler;

        public EntityPlayerMP(MinecraftServer minecraftserver, World world, string s,
                              ItemInWorldManager iteminworldmanager)
            : base(world)
        {
            loadedChunks = new LinkedList();
            field_420_ah = new HashSet();
            lastHealth = unchecked((int) 0xfa0a1f01);
            field_15004_bw = 60;
            currentWindowId = 0;
            ChunkCoordinates chunkcoordinates = world.func_22078_l();
            int i = chunkcoordinates.posX;
            int j = chunkcoordinates.posZ;
            int k = chunkcoordinates.posY;
            if (!world.worldProvider.field_4306_c)
            {
                i += rand.nextInt(20) - 10;
                k = world.findTopSolidBlock(i, j);
                j += rand.nextInt(20) - 10;
            }
            setLocationAndAngles(i + 0.5D, k, j + 0.5D, 0.0F, 0.0F);
            mcServer = minecraftserver;
            stepHeight = 0.0F;
            iteminworldmanager.thisPlayer = this;
            username = s;
            itemInWorldManager = iteminworldmanager;
            yOffset = 0.0F;
        }

        #region ICrafting Members

        public void updateCraftingInventorySlot(CraftingInventoryCB craftinginventorycb, int i, ItemStack itemstack)
        {
            if (craftinginventorycb.getSlot(i) is SlotCrafting)
            {
                return;
            }
            if (isChangingQuantityOnly)
            {
                return;
            }
            else
            {
                playerNetServerHandler.sendPacket(new Packet103(craftinginventorycb.windowId, i, itemstack));
                return;
            }
        }

        public void updateCraftingInventory(CraftingInventoryCB craftinginventorycb, List list)
        {
            playerNetServerHandler.sendPacket(new Packet104(craftinginventorycb.windowId, list));
            playerNetServerHandler.sendPacket(new Packet103(-1, -1, inventory.getItemStack()));
        }

        public void updateCraftingInventoryInfo(CraftingInventoryCB craftinginventorycb, int i, int j)
        {
            playerNetServerHandler.sendPacket(new Packet105(craftinginventorycb.windowId, i, j));
        }

        #endregion

        public void func_20057_k()
        {
            currentCraftingInventory.onCraftGuiOpened(this);
        }

        public override ItemStack[] getInventory()
        {
            return playerInventory;
        }

        public override void resetHeight()
        {
            yOffset = 0.0F;
        }

        public override float getEyeHeight()
        {
            return 1.62F;
        }

        public override void onUpdate()
        {
            itemInWorldManager.func_328_a();
            field_15004_bw--;
            currentCraftingInventory.updateCraftingMatrix();
            for (int i = 0; i < 5; i++)
            {
                ItemStack itemstack = getEquipmentInSlot(i);
                if (itemstack != playerInventory[i])
                {
                    mcServer.entityTracker.sendPacketToTrackedPlayers(this,
                                                                      new Packet5PlayerInventory(entityId, i, itemstack));
                    playerInventory[i] = itemstack;
                }
            }
        }

        public ItemStack getEquipmentInSlot(int i)
        {
            if (i == 0)
            {
                return inventory.getCurrentItem();
            }
            else
            {
                return inventory.armorInventory[i - 1];
            }
        }

        public override void onDeath(Entity entity)
        {
            inventory.dropAllItems();
        }

        public override bool attackEntityFrom(Entity entity, int i)
        {
            if (field_15004_bw > 0)
            {
                return false;
            }
            if (!mcServer.pvpOn)
            {
                if (entity is EntityPlayer)
                {
                    return false;
                }
                if (entity is EntityArrow)
                {
                    var entityarrow = (EntityArrow) entity;
                    if (entityarrow.owner is EntityPlayer)
                    {
                        return false;
                    }
                }
            }
            return base.attackEntityFrom(entity, i);
        }

        public override void heal(int i)
        {
            base.heal(i);
        }

        public void onUpdateEntity(bool flag)
        {
            base.onUpdate();
            if (flag && !loadedChunks.isEmpty())
            {
                var chunkcoordintpair = (ChunkCoordIntPair) loadedChunks.get(0);
                if (chunkcoordintpair != null)
                {
                    bool flag1 = false;
                    if (playerNetServerHandler.getNumChunkDataPackets() < 2)
                    {
                        flag1 = true;
                    }
                    if (flag1)
                    {
                        loadedChunks.remove(chunkcoordintpair);
                        playerNetServerHandler.sendPacket(new Packet51MapChunk(chunkcoordintpair.chunkXPos*16, 0,
                                                                               chunkcoordintpair.chunkZPos*16, 16, 128,
                                                                               16, mcServer.worldMngr));
                        List list = mcServer.worldMngr.getTileEntityList(chunkcoordintpair.chunkXPos*16, 0,
                                                                         chunkcoordintpair.chunkZPos*16,
                                                                         chunkcoordintpair.chunkXPos*16 + 16, 128,
                                                                         chunkcoordintpair.chunkZPos*16 + 16);
                        for (int i = 0; i < list.size(); i++)
                        {
                            getTileEntityInfo((TileEntity) list.get(i));
                        }
                    }
                }
            }
            if (health != lastHealth)
            {
                playerNetServerHandler.sendPacket(new Packet8(health));
                lastHealth = health;
            }
        }

        private void getTileEntityInfo(TileEntity tileentity)
        {
            if (tileentity != null)
            {
                Packet packet = tileentity.getDescriptionPacket();
                if (packet != null)
                {
                    playerNetServerHandler.sendPacket(packet);
                }
            }
        }

        public override void onLivingUpdate()
        {
            base.onLivingUpdate();
        }

        public override void onItemPickup(Entity entity, int i)
        {
            if (!entity.isDead)
            {
                if (entity is EntityItem)
                {
                    mcServer.entityTracker.sendPacketToTrackedPlayers(entity,
                                                                      new Packet22Collect(entity.entityId, entityId));
                }
                if (entity is EntityArrow)
                {
                    mcServer.entityTracker.sendPacketToTrackedPlayers(entity,
                                                                      new Packet22Collect(entity.entityId, entityId));
                }
            }
            base.onItemPickup(entity, i);
            currentCraftingInventory.updateCraftingMatrix();
        }

        public override void swingItem()
        {
            if (!isSwinging)
            {
                swingProgressInt = -1;
                isSwinging = true;
                mcServer.entityTracker.sendPacketToTrackedPlayers(this, new Packet18ArmAnimation(this, 1));
            }
        }

        public void func_22068_s()
        {
        }

        public override bool goToSleep(int i, int j, int k)
        {
            if (base.goToSleep(i, j, k))
            {
                mcServer.entityTracker.sendPacketToTrackedPlayers(this, new Packet17Sleep(this, 0, i, j, k));
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void wakeUpPlayer(bool flag, bool flag1)
        {
            if (isPlayerSleeping())
            {
                mcServer.entityTracker.sendPacketToTrackedPlayersAndTrackedEntity(this,
                                                                                  new Packet18ArmAnimation(this, 3));
            }
            base.wakeUpPlayer(flag, flag1);
            playerNetServerHandler.teleportTo(posX, posY, posZ, rotationYaw, rotationPitch);
        }

        public override void mountEntity(Entity entity)
        {
            base.mountEntity(entity);
            playerNetServerHandler.sendPacket(new Packet39(this, ridingEntity));
            playerNetServerHandler.teleportTo(posX, posY, posZ, rotationYaw, rotationPitch);
        }

        public override
            void updateFallState(double d, bool flag)
        {
        }

        public void handleFalling(double d, bool flag)
        {
            base.updateFallState(d, flag);
        }

        private void getNextWidowId()
        {
            currentWindowId = currentWindowId%100 + 1;
        }

        public override void displayWorkbenchGUI(int i, int j, int k)
        {
            getNextWidowId();
            playerNetServerHandler.sendPacket(new Packet100(currentWindowId, 1, "Crafting", 9));
            currentCraftingInventory = new CraftingInventoryWorkbenchCB(inventory, worldObj, i, j, k);
            currentCraftingInventory.windowId = currentWindowId;
            currentCraftingInventory.onCraftGuiOpened(this);
        }

        public override void displayGUIChest(IInventory iinventory)
        {
            getNextWidowId();
            playerNetServerHandler.sendPacket(new Packet100(currentWindowId, 0, iinventory.getInvName(),
                                                            iinventory.getSizeInventory()));
            currentCraftingInventory = new CraftingInventoryChestCB(inventory, iinventory);
            currentCraftingInventory.windowId = currentWindowId;
            currentCraftingInventory.onCraftGuiOpened(this);
        }

        public override void displayGUIFurnace(TileEntityFurnace tileentityfurnace)
        {
            getNextWidowId();
            playerNetServerHandler.sendPacket(new Packet100(currentWindowId, 2, tileentityfurnace.getInvName(),
                                                            tileentityfurnace.getSizeInventory()));
            currentCraftingInventory = new CraftingInventoryFurnaceCb(inventory, tileentityfurnace);
            currentCraftingInventory.windowId = currentWindowId;
            currentCraftingInventory.onCraftGuiOpened(this);
        }

        public override void displayGUIDispenser(TileEntityDispenser tileentitydispenser)
        {
            getNextWidowId();
            playerNetServerHandler.sendPacket(new Packet100(currentWindowId, 3, tileentitydispenser.getInvName(),
                                                            tileentitydispenser.getSizeInventory()));
            currentCraftingInventory = new CraftingInventoryDispenserCB(inventory, tileentitydispenser);
            currentCraftingInventory.windowId = currentWindowId;
            currentCraftingInventory.onCraftGuiOpened(this);
        }

        public override void onItemStackChanged(ItemStack itemstack)
        {
        }

        public override void usePersonalCraftingInventory()
        {
            playerNetServerHandler.sendPacket(new Packet101(currentCraftingInventory.windowId));
            closeCraftingGui();
        }

        public void updateHeldItem()
        {
            if (isChangingQuantityOnly)
            {
                return;
            }
            else
            {
                playerNetServerHandler.sendPacket(new Packet103(-1, -1, inventory.getItemStack()));
                return;
            }
        }

        public void closeCraftingGui()
        {
            currentCraftingInventory.onCraftGuiClosed(this);
            currentCraftingInventory = personalCraftingInventory;
        }

        public void setMovementType(float f, float f1, bool flag, bool flag1, float f2, float f3)
        {
            moveStrafing = f;
            moveForward = f1;
            isJumping = flag;
            func_21043_b(flag1);
            rotationPitch = f2;
            rotationYaw = f3;
        }
    }
}