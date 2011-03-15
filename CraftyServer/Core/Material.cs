namespace CraftyServer.Core
{
    public class Material
    {
        public static Material air = new MaterialTransparent();
        public static Material ground = new Material();
        public static Material wood = (new Material()).setBurning();
        public static Material rock = new Material();
        public static Material iron = new Material();
        public static Material water = new MaterialLiquid();
        public static Material lava = new MaterialLiquid();
        public static Material leaves = (new Material()).setBurning();
        public static Material plants = new MaterialLogic();
        public static Material sponge = new Material();
        public static Material cloth = (new Material()).setBurning();
        public static Material fire = new MaterialTransparent();
        public static Material sand = new Material();
        public static Material circuits = new MaterialLogic();
        public static Material glass = new Material();
        public static Material tnt = (new Material()).setBurning();
        public static Material field_4215_q = new Material();
        public static Material ice = new Material();
        public static Material snow = new MaterialLogic();
        public static Material builtSnow = new Material();
        public static Material cactus = new Material();
        public static Material clay = new Material();
        public static Material pumpkin = new Material();
        public static Material portal = new Material();
        public static Material field_21100_y = new Material();
        private bool canBurn;

        public virtual bool getIsLiquid()
        {
            return false;
        }

        public virtual bool isSolid()
        {
            return true;
        }

        public virtual bool getCanBlockGrass()
        {
            return true;
        }

        public virtual bool getIsSolid()
        {
            return true;
        }

        private Material setBurning()
        {
            canBurn = true;
            return this;
        }

        public virtual bool getBurning()
        {
            return canBurn;
        }
    }
}