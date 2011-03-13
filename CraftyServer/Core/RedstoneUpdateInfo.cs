namespace CraftyServer.Core
{
    internal class RedstoneUpdateInfo
    {
        public RedstoneUpdateInfo(int i, int j, int k, long l)
        {
            x = i;
            y = j;
            z = k;
            updateTime = l;
        }

        public int x;
        public int y;
        public int z;
        public long updateTime;
    }
}