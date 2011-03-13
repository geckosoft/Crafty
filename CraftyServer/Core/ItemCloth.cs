namespace CraftyServer.Core
{
    public class ItemCloth : ItemBlock
    {
        public ItemCloth(int i) : base(i)
        {
            setMaxDamage(0);
            setHasSubtypes(true);
        }

        public override int getMetadata(int i)
        {
            return i;
        }
    }
}