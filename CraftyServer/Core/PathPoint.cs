using java.lang;

namespace CraftyServer.Core
{
    public class PathPoint : java.lang.Object
    {
        public PathPoint(int i, int j, int k)
        {
            index = -1;
            isFirst = false;
            xCoord = i;
            yCoord = j;
            zCoord = k;
            hash = func_22203_a(i, j, k);
        }

        public static int func_22203_a(int i, int j, int k)
        {
            return
                (int)
                (j & 0xff | (i & 0x7fff) << 8 | (k & 0x7fff) << 24 | (i >= 0 ? 0 : 0x80000000) | (k >= 0 ? 0 : 0x8000));
        }

        public float distanceTo(PathPoint pathpoint)
        {
            float f = pathpoint.xCoord - xCoord;
            float f1 = pathpoint.yCoord - yCoord;
            float f2 = pathpoint.zCoord - zCoord;
            return MathHelper.sqrt_float(f*f + f1*f1 + f2*f2);
        }

        public override bool equals(object obj)
        {
            if (obj is PathPoint)
            {
                PathPoint pathpoint = (PathPoint) obj;
                return hash == pathpoint.hash && xCoord == pathpoint.xCoord && yCoord == pathpoint.yCoord &&
                       zCoord == pathpoint.zCoord;
            }
            else
            {
                return false;
            }
        }

        public override int hashCode()
        {
            return hash;
        }

        public bool isAssigned()
        {
            return index >= 0;
        }

        public override string toString()
        {
            return
                (new StringBuilder()).append(xCoord).append(", ").append(yCoord).append(", ").append(zCoord).toString();
        }

        public int xCoord;
        public int yCoord;
        public int zCoord;
        private int hash;
        public int index;
        public float totalPathDistance;
        public float distanceToNext;
        public float distanceToTarget;
        public PathPoint previous;
        public bool isFirst;
    }
}