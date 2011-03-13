using java.io;
using java.util;

namespace CraftyServer.Core
{
    public class SaveOldDir : PlayerNBTManager
    {
        public SaveOldDir(File file, string s, bool flag)
            : base(file, s, flag)
        {
        }

        public override IChunkLoader func_22092_a(WorldProvider worldprovider)
        {
            File file = func_22097_a();
            if (worldprovider is WorldProviderHell)
            {
                File file1 = new File(file, "DIM-1");
                file1.mkdirs();
                return new McRegionChunkLoader(file1);
            }
            else
            {
                return new McRegionChunkLoader(file);
            }
        }

        public override void func_22095_a(WorldInfo worldinfo, List list)
        {
            worldinfo.func_22191_a(19132);
            base.func_22095_a(worldinfo, list);
        }

        public override void func_22093_e()
        {
            RegionFileCache.func_22122_a();
        }
    }
}