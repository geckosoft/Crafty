namespace CraftyServer.Core
{
    internal class RedstoneUpdateInfo
    {
        public long updateTime;
        public int x;
        public int y;
        public int z;

        public RedstoneUpdateInfo(int i, int j, int k, long l)
        {
            x = i;
            y = j;
            z = k;
            updateTime = l;
        }
    }
}