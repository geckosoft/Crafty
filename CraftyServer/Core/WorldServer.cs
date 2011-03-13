using CraftyServer.Server;
using java.util;

namespace CraftyServer.Core
{
    public class WorldServer : World
    {
        public WorldServer(MinecraftServer minecraftserver, ISaveHandler isavehandler, string s, int i)
            : base(isavehandler, s, (new Random()).nextLong(), WorldProvider.func_4091_a(i))
        {
            field_819_z = false;
            field_20912_E = new MCHashTable();
            field_6160_D = minecraftserver;
        }

        public override void updateEntityWithOptionalForce(Entity entity, bool flag)
        {
            if (!field_6160_D.spawnPeacefulMobs && ((entity is EntityAnimals) || (entity is EntityWaterMob)))
            {
                entity.setEntityDead();
            }
            if (entity.riddenByEntity == null || !(entity.riddenByEntity is EntityPlayer))
            {
                base.updateEntityWithOptionalForce(entity, flag);
            }
        }

        public void func_12017_b(Entity entity, bool flag)
        {
            base.updateEntityWithOptionalForce(entity, flag);
        }

        protected override IChunkProvider func_22086_b()
        {
            IChunkLoader ichunkloader = worldFile.func_22092_a(worldProvider);
            field_20911_y = new ChunkProviderServer(this, ichunkloader, worldProvider.getChunkProvider());
            return field_20911_y;
        }

        public List getTileEntityList(int i, int j, int k, int l, int i1, int j1)
        {
            ArrayList arraylist = new ArrayList();
            for (int k1 = 0; k1 < loadedTileEntityList.size(); k1++)
            {
                TileEntity tileentity = (TileEntity) loadedTileEntityList.get(k1);
                if (tileentity.xCoord >= i && tileentity.yCoord >= j && tileentity.zCoord >= k && tileentity.xCoord < l &&
                    tileentity.yCoord < i1 && tileentity.zCoord < j1)
                {
                    arraylist.add(tileentity);
                }
            }

            return arraylist;
        }

        public override bool canMineBlock(EntityPlayer entityplayer, int i, int j, int k)
        {
            int l = (int) MathHelper.abs(i - worldInfo.getSpawnX());
            int i1 = (int) MathHelper.abs(k - worldInfo.getSpawnZ());
            if (l > i1)
            {
                i1 = l;
            }
            return i1 > 16 || field_6160_D.configManager.isOp(entityplayer.username);
        }

        public override void obtainEntitySkin(Entity entity)
        {
            base.obtainEntitySkin(entity);
            field_20912_E.addKey(entity.entityId, entity);
        }

        public override void releaseEntitySkin(Entity entity)
        {
            base.releaseEntitySkin(entity);
            field_20912_E.removeObject(entity.entityId);
        }

        public Entity func_6158_a(int i)
        {
            return (Entity) field_20912_E.lookup(i);
        }

        public override void func_9206_a(Entity entity, byte byte0)
        {
            Packet38 packet38 = new Packet38(entity.entityId, byte0);
            field_6160_D.entityTracker.sendPacketToTrackedPlayersAndTrackedEntity(entity, packet38);
        }

        public override Explosion newExplosion(Entity entity, double d, double d1, double d2,
                                               float f, bool flag)
        {
            Explosion explosion = base.newExplosion(entity, d, d1, d2, f, flag);
            field_6160_D.configManager.func_12022_a(d, d1, d2, 64D,
                                                    new Packet60(d, d1, d2, f, explosion.destroyedBlockPositions));
            return explosion;
        }

        public override void playNoteAt(int i, int j, int k, int l, int i1)
        {
            base.playNoteAt(i, j, k, l, i1);
            field_6160_D.configManager.func_12022_a(i, j, k, 64D, new Packet54(i, j, k, l, i1));
        }

        public void func_22088_r()
        {
            worldFile.func_22093_e();
        }

        public ChunkProviderServer field_20911_y;
        public bool field_819_z;
        public bool levelSaving;
        private MinecraftServer field_6160_D;
        private MCHashTable field_20912_E;
    }
}