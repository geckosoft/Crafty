namespace CraftyServer.Core
{
    public interface IWorldAccess
    {
        void func_683_a(int i, int j, int k);
        void markBlockRangeNeedsUpdate(int i, int j, int k, int l, int i1, int j1);

        void playSound(string s, double d, double d1, double d2,
                       float f, float f1);

        void spawnParticle(string s, double d, double d1, double d2,
                           double d3, double d4, double d5);

        void obtainEntitySkin(Entity entity);
        void releaseEntitySkin(Entity entity);
        void updateAllRenderers();
        void playRecord(string s, int i, int j, int k);
        void doNothingWithTileEntity(int i, int j, int k, TileEntity tileentity);
    }
}