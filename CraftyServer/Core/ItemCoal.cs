namespace CraftyServer.Core
{
    public class ItemCoal : Item
    {
        public ItemCoal(int i) : base(i)
        {
            setHasSubtypes(true);
            setMaxDamage(0);
        }
    }
}