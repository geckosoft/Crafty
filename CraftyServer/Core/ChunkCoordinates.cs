using java.lang;

namespace CraftyServer.Core
{
    public class ChunkCoordinates
        : java.lang.Object, Comparable
    {
        public ChunkCoordinates()
        {
        }

        public ChunkCoordinates(int i, int j, int k)
        {
            posX = i;
            posY = j;
            posZ = k;
        }

        public override bool equals(object obj)
        {
            if (!(obj is ChunkCoordinates))
            {
                return false;
            }
            else
            {
                ChunkCoordinates chunkcoordinates = (ChunkCoordinates) obj;
                return posX == chunkcoordinates.posX && posY == chunkcoordinates.posY && posZ == chunkcoordinates.posZ;
            }
        }

        public override int hashCode()
        {
            return posX + posZ << 8 + posY << 16;
        }

        public int func_22215_a(ChunkCoordinates chunkcoordinates)
        {
            if (posY == chunkcoordinates.posY)
            {
                if (posZ == chunkcoordinates.posZ)
                {
                    return posX - chunkcoordinates.posX;
                }
                else
                {
                    return posZ - chunkcoordinates.posZ;
                }
            }
            else
            {
                return posY - chunkcoordinates.posY;
            }
        }

        public int compareTo(object obj)
        {
            return func_22215_a((ChunkCoordinates) obj);
        }

        public int posX;
        public int posY;
        public int posZ;

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return func_22215_a((ChunkCoordinates) obj);
        }

        #endregion
    }
}