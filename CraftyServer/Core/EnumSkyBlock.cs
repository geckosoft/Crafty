namespace CraftyServer.Core
{
    public class EnumSkyBlock
    {
        public static EnumSkyBlock Sky;
        public static EnumSkyBlock Block;
        private static readonly EnumSkyBlock[] field_983_d; /* synthetic field */
        public int field_984_c;

        static EnumSkyBlock()
        {
            Sky = new EnumSkyBlock("Sky", 0, 15);
            Block = new EnumSkyBlock("Block", 1, 0);
            field_983_d = (new[]
                           {
                               Sky, Block
                           });
        }

        private EnumSkyBlock(string s, int i, int j)
        {
            //base(s, i);
            field_984_c = j;
        }

        public static EnumSkyBlock[] values()
        {
            return (EnumSkyBlock[]) field_983_d.Clone();
        }

        public static EnumSkyBlock valueOf(string s)
        {
            return null; // return (EnumSkyBlock)Enum.valueOf(typeof(EnumSkyBlock), s);
        }
    }
}