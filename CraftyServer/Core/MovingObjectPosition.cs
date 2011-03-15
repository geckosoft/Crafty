namespace CraftyServer.Core
{
    public class MovingObjectPosition
    {
        public int blockX;
        public int blockY;
        public int blockZ;
        public Entity entityHit;
        public Vec3D hitVec;
        public int sideHit;
        public EnumMovingObjectType typeOfHit;

        public MovingObjectPosition(int i, int j, int k, int l, Vec3D vec3d)
        {
            typeOfHit = EnumMovingObjectType.TILE;
            blockX = i;
            blockY = j;
            blockZ = k;
            sideHit = l;
            hitVec = Vec3D.createVector(vec3d.xCoord, vec3d.yCoord, vec3d.zCoord);
        }

        public MovingObjectPosition(Entity entity)
        {
            typeOfHit = EnumMovingObjectType.ENTITY;
            entityHit = entity;
            hitVec = Vec3D.createVector(entity.posX, entity.posY, entity.posZ);
        }
    }
}