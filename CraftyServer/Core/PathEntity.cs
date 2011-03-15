namespace CraftyServer.Core
{
    public class PathEntity
    {
        private readonly PathPoint[] points;
        private int pathIndex;
        public int pathLength;

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
            double d = points[pathIndex].xCoord + (int) (entity.width + 1.0F)*0.5D;
            double d1 = points[pathIndex].yCoord;
            double d2 = points[pathIndex].zCoord + (int) (entity.width + 1.0F)*0.5D;
            return Vec3D.createVector(d, d1, d2);
        }
    }
}