namespace CraftyServer.Core
{
    public class MaterialLiquid : Material
    {
        public override bool getIsLiquid()
        {
            return true;
        }

        public override bool getIsSolid()
        {
            return false;
        }

        public override bool isSolid()
        {
            return false;
        }
    }
}