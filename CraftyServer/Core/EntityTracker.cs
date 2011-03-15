using CraftyServer.Server;
using java.lang;
using java.util;

namespace CraftyServer.Core
{
    public class EntityTracker
    {
        private readonly int maxTrackingDistanceThreshold;
        private readonly MinecraftServer mcServer;
        private readonly MCHashTable trackedEntityHashTable;
        private readonly Set trackedEntitySet;

        public EntityTracker(MinecraftServer minecraftserver)
        {
            trackedEntitySet = new HashSet();
            trackedEntityHashTable = new MCHashTable();
            mcServer = minecraftserver;
            maxTrackingDistanceThreshold = minecraftserver.configManager.func_640_a();
        }

        public void trackEntity(Entity entity)
        {
            if (entity is EntityPlayerMP)
            {
                trackEntity(entity, 512, 2);
                var entityplayermp = (EntityPlayerMP) entity;
                Iterator iterator = trackedEntitySet.iterator();
                do
                {
                    if (!iterator.hasNext())
                    {
                        break;
                    }
                    var entitytrackerentry = (EntityTrackerEntry) iterator.next();
                    if (entitytrackerentry.trackedEntity != entityplayermp)
                    {
                        entitytrackerentry.updatePlayerEntity(entityplayermp);
                    }
                } while (true);
            }
            else if (entity is EntityFish)
            {
                trackEntity(entity, 64, 5, true);
            }
            else if (entity is EntityArrow)
            {
                trackEntity(entity, 64, 5, true);
            }
            else if (entity is EntitySnowball)
            {
                trackEntity(entity, 64, 5, true);
            }
            else if (entity is EntityEgg)
            {
                trackEntity(entity, 64, 5, true);
            }
            else if (entity is EntityItem)
            {
                trackEntity(entity, 64, 20, true);
            }
            else if (entity is EntityMinecart)
            {
                trackEntity(entity, 160, 5, true);
            }
            else if (entity is EntityBoat)
            {
                trackEntity(entity, 160, 5, true);
            }
            else if (entity is EntitySquid)
            {
                trackEntity(entity, 160, 3, true);
            }
            else if (entity is IAnimals)
            {
                trackEntity(entity, 160, 3);
            }
            else if (entity is EntityTNTPrimed)
            {
                trackEntity(entity, 160, 10, true);
            }
            else if (entity is EntityFallingSand)
            {
                trackEntity(entity, 160, 20, true);
            }
            else if (entity is EntityPainting)
            {
                trackEntity(entity, 160, 0x7fffffff, false);
            }
        }

        public void trackEntity(Entity entity, int i, int j)
        {
            trackEntity(entity, i, j, false);
        }

        public void trackEntity(Entity entity, int i, int j, bool flag)
        {
            if (i > maxTrackingDistanceThreshold)
            {
                i = maxTrackingDistanceThreshold;
            }
            if (trackedEntityHashTable.containsItem(entity.entityId))
            {
                throw new IllegalStateException("Entity is already tracked!");
            }
            else
            {
                var entitytrackerentry = new EntityTrackerEntry(entity, i, j, flag);
                trackedEntitySet.add(entitytrackerentry);
                trackedEntityHashTable.addKey(entity.entityId, entitytrackerentry);
                entitytrackerentry.updatePlayerEntities(mcServer.worldMngr.playerEntities);
                return;
            }
        }

        public void untrackEntity(Entity entity)
        {
            if (entity is EntityPlayerMP)
            {
                var entityplayermp = (EntityPlayerMP) entity;
                EntityTrackerEntry entitytrackerentry1;
                for (Iterator iterator = trackedEntitySet.iterator();
                     iterator.hasNext();
                     entitytrackerentry1.removeFromTrackedPlayers(entityplayermp))
                {
                    entitytrackerentry1 = (EntityTrackerEntry) iterator.next();
                }
            }
            var entitytrackerentry =
                (EntityTrackerEntry) trackedEntityHashTable.removeObject(entity.entityId);
            if (entitytrackerentry != null)
            {
                trackedEntitySet.remove(entitytrackerentry);
                entitytrackerentry.sendDestroyEntityPacketToTrackedPlayers();
            }
        }

        public void updateTrackedEntities()
        {
            var arraylist = new ArrayList();
            Iterator iterator = trackedEntitySet.iterator();
            do
            {
                if (!iterator.hasNext())
                {
                    break;
                }
                var entitytrackerentry = (EntityTrackerEntry) iterator.next();
                entitytrackerentry.updatePlayerList(mcServer.worldMngr.playerEntities);
                if (entitytrackerentry.playerEntitiesUpdated && (entitytrackerentry.trackedEntity is EntityPlayerMP))
                {
                    arraylist.add(entitytrackerentry.trackedEntity);
                }
            } while (true);

            for (int i = 0; i < arraylist.size(); i++)
            {
                var entityplayermp = (EntityPlayerMP) arraylist.get(i);
                Iterator iterator1 = trackedEntitySet.iterator();
                do
                {
                    if (!iterator1.hasNext())
                    {
                        break;
                    }
                    var entitytrackerentry1 = (EntityTrackerEntry) iterator1.next();
                    if (entitytrackerentry1.trackedEntity != entityplayermp)
                    {
                        entitytrackerentry1.updatePlayerEntity(entityplayermp);
                    }
                } while (true);
            }
        }

        public void sendPacketToTrackedPlayers(Entity entity, Packet packet)
        {
            var entitytrackerentry = (EntityTrackerEntry) trackedEntityHashTable.lookup(entity.entityId);
            if (entitytrackerentry != null)
            {
                entitytrackerentry.sendPacketToTrackedPlayers(packet);
            }
        }

        public void sendPacketToTrackedPlayersAndTrackedEntity(Entity entity, Packet packet)
        {
            var entitytrackerentry = (EntityTrackerEntry) trackedEntityHashTable.lookup(entity.entityId);
            if (entitytrackerentry != null)
            {
                entitytrackerentry.sendPacketToTrackedPlayersAndTrackedEntity(packet);
            }
        }

        public void removeTrackedPlayerSymmetric(EntityPlayerMP entityplayermp)
        {
            EntityTrackerEntry entitytrackerentry;
            for (Iterator iterator = trackedEntitySet.iterator();
                 iterator.hasNext();
                 entitytrackerentry.removeTrackedPlayerSymmetric(entityplayermp))
            {
                entitytrackerentry = (EntityTrackerEntry) iterator.next();
            }
        }
    }
}