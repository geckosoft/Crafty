namespace CraftyServer.Core
{
    public class PathEntity
    {
        public PathEntity(PathPoint[] apathpoint)
        {
            points = apathpoint;
            pathLength = apathpoint.Length;
        }

        public void incrementPathIndex()
        {
            pathIndex++;
        }

        public bool isFinished()
        {
            return pathIndex >= points.Length;
        }

        public PathPoint func_22211_c()
        {
            if (pathLength > 0)
            {
                return points[pathLength - 1];
            }
            else
            {
                return null;
            }
        }

        public Vec3D getPosition(Entity entity)
        {
            double d = (double) points[pathIndex].xCoord + (double) (int) (entity.width + 1.0F)*0.5D;
            double d1 = points[pathIndex].yCoord;
            double d2 = (double) points[pathIndex].zCoord + (double) (int) (entity.width + 1.0F)*0.5D;
            return Vec3D.createVector(d, d1, d2);
        }

        private PathPoint[] points;
        public int pathLength;
        private int pathIndex;
    }
}