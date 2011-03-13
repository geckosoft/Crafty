namespace CraftyServer.Core
{
    public class ItemSlab : ItemBlock
    {
        public ItemSlab(int i) : base(i)
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