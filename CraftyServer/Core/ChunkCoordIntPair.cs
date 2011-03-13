namespace CraftyServer.Core
{
    public class ChunkCoordIntPair : java.lang.Object
    {
        public ChunkCoordIntPair(int i, int j)
        {
            chunkXPos = i;
            chunkZPos = j;
        }

        public static int chunkXZ2Int(int i, int j)
        {
            return (int) (i >= 0 ? 0 : 0x80000000) | (i & 0x7fff) << 16 | (j >= 0 ? 0 : 0x8000) | j & 0x7fff;
        }

        public override int hashCode()
        {
            return chunkXZ2Int(chunkXPos, chunkZPos);
        }

        public override bool equals(object obj)
        {
            ChunkCoordIntPair chunkcoordintpair = (ChunkCoordIntPair) obj;
            return chunkcoordintpair.chunkXPos == chunkXPos && chunkcoordintpair.chunkZPos == chunkZPos;
        }

        public int chunkXPos;
        public int chunkZPos;
    }
}