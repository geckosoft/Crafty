using java.util;


namespace CraftyServer.Core
{
    public class MapGenBase
    {
        public MapGenBase()
        {
            field_947_a = 8;
            rand = new Random();
        }

        public virtual void func_667_a(IChunkProvider ichunkprovider, World world, int i, int j, byte[] abyte0)
        {
            int k = field_947_a;
            rand.setSeed(world.func_22079_j());
            long l = (rand.nextLong()/2L)*2L + 1L;
            long l1 = (rand.nextLong()/2L)*2L + 1L;
            for (int i1 = i - k; i1 <= i + k; i1++)
            {
                for (int j1 = j - k; j1 <= j + k; j1++)
                {
                    rand.setSeed((long) i1*l + (long) j1*l1 ^ world.func_22079_j());
                    func_666_a(world, i1, j1, i, j, abyte0);
                }
            }
        }

        public virtual void func_666_a(World world, int i, int j, int k, int l, byte[] abyte0)
        {
        }

        protected int field_947_a;
        protected Random rand;
    }
}