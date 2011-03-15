using java.lang;
using java.util;

namespace CraftyServer.Core
{
    public class EntityTrackerEntry : Object
    {
        private readonly bool shouldSendMotionUpdates;
        public int encodedPosX;
        public int encodedPosY;
        public int encodedPosZ;
        public int encodedRotationPitch;
        public int encodedRotationYaw;
        public int field_9234_e;
        private bool firstUpdateDone;
        public double lastTrackedEntityMotionX;
        public double lastTrackedEntityMotionY;
        public double lastTrackedEntityMotionZ;
        private double lastTrackedEntityPosX;
        private double lastTrackedEntityPosY;
        private double lastTrackedEntityPosZ;
        public bool playerEntitiesUpdated;
        public Entity trackedEntity;
        public Set trackedPlayers;
        public int trackingDistanceThreshold;
        public int updateCounter;

        public EntityTrackerEntry(Entity entity, int i, int j, bool flag)
        {
            updateCounter = 0;
            firstUpdateDone = false;
            playerEntitiesUpdated = false;
            trackedPlayers = new HashSet();
            trackedEntity = entity;
            trackingDistanceThreshold = i;
            field_9234_e = j;
            shouldSendMotionUpdates = flag;
            encodedPosX = MathHelper.floor_double(entity.posX*32D);
            encodedPosY = MathHelper.floor_double(entity.posY*32D);
            encodedPosZ = MathHelper.floor_double(entity.posZ*32D);
            encodedRotationYaw = MathHelper.floor_float((entity.rotationYaw*256F)/360F);
            encodedRotationPitch = MathHelper.floor_float((entity.rotationPitch*256F)/360F);
        }

        public override bool equals(object obj)
        {
            if (obj is EntityTrackerEntry)
            {
                return ((EntityTrackerEntry) obj).trackedEntity.entityId == trackedEntity.entityId;
            }
            else
            {
                return false;
            }
        }

        public override int hashCode()
        {
            return trackedEntity.entityId;
        }

        public void updatePlayerList(List list)
        {
            playerEntitiesUpdated = false;
            if (!firstUpdateDone ||
                trackedEntity.getDistanceSq(lastTrackedEntityPosX, lastTrackedEntityPosY, lastTrackedEntityPosZ) > 16D)
            {
                lastTrackedEntityPosX = trackedEntity.posX;
                lastTrackedEntityPosY = trackedEntity.posY;
                lastTrackedEntityPosZ = trackedEntity.posZ;
                firstUpdateDone = true;
                playerEntitiesUpdated = true;
                updatePlayerEntities(list);
            }
            if (++updateCounter%field_9234_e == 0)
            {
                int i = MathHelper.floor_double(trackedEntity.posX*32D);
                int j = MathHelper.floor_double(trackedEntity.posY*32D);
                int k = MathHelper.floor_double(trackedEntity.posZ*32D);
                int l = MathHelper.floor_float((trackedEntity.rotationYaw*256F)/360F);
                int i1 = MathHelper.floor_float((trackedEntity.rotationPitch*256F)/360F);
                int j1 = i - encodedPosX;
                int k1 = j - encodedPosY;
                int l1 = k - encodedPosZ;
                object obj = null;
                bool flag = Math.abs(i) >= 8 || Math.abs(j) >= 8 || Math.abs(k) >= 8;
                bool flag1 = Math.abs(l - encodedRotationYaw) >= 8 || Math.abs(i1 - encodedRotationPitch) >= 8;
                if (j1 < -128 || j1 >= 128 || k1 < -128 || k1 >= 128 || l1 < -128 || l1 >= 128)
                {
                    obj = new Packet34EntityTeleport(trackedEntity.entityId, i, j, k, (byte) l, (byte) i1);
                }
                else if (flag && flag1)
                {
                    obj = new Packet33RelEntityMoveLook(trackedEntity.entityId, (byte) j1, (byte) k1, (byte) l1,
                                                        (byte) l, (byte) i1);
                }
                else if (flag)
                {
                    obj = new Packet31RelEntityMove(trackedEntity.entityId, (byte) j1, (byte) k1, (byte) l1);
                }
                else if (flag1)
                {
                    obj = new Packet32EntityLook(trackedEntity.entityId, (byte) l, (byte) i1);
                }
                if (shouldSendMotionUpdates)
                {
                    double d = trackedEntity.motionX - lastTrackedEntityMotionX;
                    double d1 = trackedEntity.motionY - lastTrackedEntityMotionY;
                    double d2 = trackedEntity.motionZ - lastTrackedEntityMotionZ;
                    double d3 = 0.02D;
                    double d4 = d*d + d1*d1 + d2*d2;
                    if (d4 > d3*d3 ||
                        d4 > 0.0D && trackedEntity.motionX == 0.0D && trackedEntity.motionY == 0.0D &&
                        trackedEntity.motionZ == 0.0D)
                    {
                        lastTrackedEntityMotionX = trackedEntity.motionX;
                        lastTrackedEntityMotionY = trackedEntity.motionY;
                        lastTrackedEntityMotionZ = trackedEntity.motionZ;
                        sendPacketToTrackedPlayers(new Packet28(trackedEntity.entityId, lastTrackedEntityMotionX,
                                                                lastTrackedEntityMotionY, lastTrackedEntityMotionZ));
                    }
                }
                if (obj != null)
                {
                    sendPacketToTrackedPlayers(((Packet) (obj)));
                }
                DataWatcher datawatcher = trackedEntity.getDataWatcher();
                if (datawatcher.hasObjectChanged())
                {
                    sendPacketToTrackedPlayersAndTrackedEntity(new Packet40(trackedEntity.entityId, datawatcher));
                }
                if (flag)
                {
                    encodedPosX = i;
                    encodedPosY = j;
                    encodedPosZ = k;
                }
                if (flag1)
                {
                    encodedRotationYaw = l;
                    encodedRotationPitch = i1;
                }
            }
            if (trackedEntity.beenAttacked)
            {
                sendPacketToTrackedPlayersAndTrackedEntity(new Packet28(trackedEntity));
                trackedEntity.beenAttacked = false;
            }
        }

        public void sendPacketToTrackedPlayers(Packet packet)
        {
            EntityPlayerMP entityplayermp;
            for (Iterator iterator = trackedPlayers.iterator();
                 iterator.hasNext();
                 entityplayermp.playerNetServerHandler.sendPacket(packet))
            {
                entityplayermp = (EntityPlayerMP) iterator.next();
            }
        }

        public void sendPacketToTrackedPlayersAndTrackedEntity(Packet packet)
        {
            sendPacketToTrackedPlayers(packet);
            if (trackedEntity is EntityPlayerMP)
            {
                ((EntityPlayerMP) trackedEntity).playerNetServerHandler.sendPacket(packet);
            }
        }

        public void sendDestroyEntityPacketToTrackedPlayers()
        {
            sendPacketToTrackedPlayers(new Packet29DestroyEntity(trackedEntity.entityId));
        }

        public void removeFromTrackedPlayers(EntityPlayerMP entityplayermp)
        {
            if (trackedPlayers.contains(entityplayermp))
            {
                trackedPlayers.remove(entityplayermp);
            }
        }

        public void updatePlayerEntity(EntityPlayerMP entityplayermp)
        {
            if (entityplayermp == trackedEntity)
            {
                return;
            }
            double d = entityplayermp.posX - (encodedPosX/32);
            double d1 = entityplayermp.posZ - (encodedPosZ/32);
            if (d >= (-trackingDistanceThreshold) && d <= trackingDistanceThreshold &&
                d1 >= (-trackingDistanceThreshold) && d1 <= trackingDistanceThreshold)
            {
                if (!trackedPlayers.contains(entityplayermp))
                {
                    trackedPlayers.add(entityplayermp);
                    entityplayermp.playerNetServerHandler.sendPacket(getSpawnPacket());
                    if (shouldSendMotionUpdates)
                    {
                        entityplayermp.playerNetServerHandler.sendPacket(new Packet28(trackedEntity.entityId,
                                                                                      trackedEntity.motionX,
                                                                                      trackedEntity.motionY,
                                                                                      trackedEntity.motionZ));
                    }
                    ItemStack[] aitemstack = trackedEntity.getInventory();
                    if (aitemstack != null)
                    {
                        for (int i = 0; i < aitemstack.Length; i++)
                        {
                            entityplayermp.playerNetServerHandler.sendPacket(
                                new Packet5PlayerInventory(trackedEntity.entityId, i, aitemstack[i]));
                        }
                    }
                }
            }
            else if (trackedPlayers.contains(entityplayermp))
            {
                trackedPlayers.remove(entityplayermp);
                entityplayermp.playerNetServerHandler.sendPacket(new Packet29DestroyEntity(trackedEntity.entityId));
            }
        }

        public void updatePlayerEntities(List list)
        {
            for (int i = 0; i < list.size(); i++)
            {
                updatePlayerEntity((EntityPlayerMP) list.get(i));
            }
        }

        private Packet getSpawnPacket()
        {
            if (trackedEntity is EntityItem)
            {
                var entityitem = (EntityItem) trackedEntity;
                var packet21pickupspawn = new Packet21PickupSpawn(entityitem);
                entityitem.posX = packet21pickupspawn.xPosition/32D;
                entityitem.posY = packet21pickupspawn.yPosition/32D;
                entityitem.posZ = packet21pickupspawn.zPosition/32D;
                return packet21pickupspawn;
            }
            if (trackedEntity is EntityPlayerMP)
            {
                return new Packet20NamedEntitySpawn((EntityPlayer) trackedEntity);
            }
            if (trackedEntity is EntityMinecart)
            {
                var entityminecart = (EntityMinecart) trackedEntity;
                if (entityminecart.minecartType == 0)
                {
                    return new Packet23VehicleSpawn(trackedEntity, 10);
                }
                if (entityminecart.minecartType == 1)
                {
                    return new Packet23VehicleSpawn(trackedEntity, 11);
                }
                if (entityminecart.minecartType == 2)
                {
                    return new Packet23VehicleSpawn(trackedEntity, 12);
                }
            }
            if (trackedEntity is EntityBoat)
            {
                return new Packet23VehicleSpawn(trackedEntity, 1);
            }
            if (trackedEntity is IAnimals)
            {
                return new Packet24MobSpawn((EntityLiving) trackedEntity);
            }
            if (trackedEntity is EntityFish)
            {
                return new Packet23VehicleSpawn(trackedEntity, 90);
            }
            if (trackedEntity is EntityArrow)
            {
                return new Packet23VehicleSpawn(trackedEntity, 60);
            }
            if (trackedEntity is EntitySnowball)
            {
                return new Packet23VehicleSpawn(trackedEntity, 61);
            }
            if (trackedEntity is EntityEgg)
            {
                return new Packet23VehicleSpawn(trackedEntity, 62);
            }
            if (trackedEntity is EntityTNTPrimed)
            {
                return new Packet23VehicleSpawn(trackedEntity, 50);
            }
            if (trackedEntity is EntityFallingSand)
            {
                var entityfallingsand = (EntityFallingSand) trackedEntity;
                if (entityfallingsand.blockID == Block.sand.blockID)
                {
                    return new Packet23VehicleSpawn(trackedEntity, 70);
                }
                if (entityfallingsand.blockID == Block.gravel.blockID)
                {
                    return new Packet23VehicleSpawn(trackedEntity, 71);
                }
            }
            if (trackedEntity is EntityPainting)
            {
                return new Packet25((EntityPainting) trackedEntity);
            }
            else
            {
                throw new IllegalArgumentException(
                    (new StringBuilder()).append("Don't know how to add ").append(trackedEntity.GetType()).append("!").
                        toString());
            }
        }

        public void removeTrackedPlayerSymmetric(EntityPlayerMP entityplayermp)
        {
            if (trackedPlayers.contains(entityplayermp))
            {
                trackedPlayers.remove(entityplayermp);
                entityplayermp.playerNetServerHandler.sendPacket(new Packet29DestroyEntity(trackedEntity.entityId));
            }
        }
    }
}