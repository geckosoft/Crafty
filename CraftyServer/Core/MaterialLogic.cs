namespace CraftyServer.Core
{
    public class MaterialLogic : Material
    {
        public MaterialLogic()
        {
        }

        public override bool isSolid()
        {
            return false;
        }

        public override bool getCanBlockGrass()
        {
            return false;
        }

        public override bool getIsSolid()
        {
            return false;
        }
    }
}