using java.lang;

namespace CraftyServer.Core
{
    public class ChunkPosition : Object
    {
        public int x;
        public int y;
        public int z;

        public ChunkPosition(int i, int j, int k)
        {
            x = i;
            y = j;
            z = k;
        }

        public override bool equals(object obj)
        {
            if (obj is ChunkPosition)
            {
                var chunkposition = (ChunkPosition) obj;
                return chunkposition.x == x && chunkposition.y == y && chunkposition.z == z;
            }
            else
            {
                return false;
            }
        }

        public override int hashCode()
        {
            return x*0x88f9fa + y*0xef88b + z;
        }
    }
}