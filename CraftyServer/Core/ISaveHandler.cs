using java.util;

namespace CraftyServer.Core
{
    public interface ISaveHandler
    {
        WorldInfo func_22096_c();
        void func_22091_b();
        IChunkLoader func_22092_a(WorldProvider worldprovider);
        void func_22095_a(WorldInfo worldinfo, List list);
        void func_22094_a(WorldInfo worldinfo);
        IPlayerFileData func_22090_d();
        void func_22093_e();
    }
}