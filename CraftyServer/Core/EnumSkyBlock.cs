namespace CraftyServer.Core
{
    public class EnumSkyBlock
    {
        public static EnumSkyBlock[] values()
        {
            return (EnumSkyBlock[]) field_983_d.Clone();
        }

        public static EnumSkyBlock valueOf(string s)
        {
            return null; // return (EnumSkyBlock)Enum.valueOf(typeof(EnumSkyBlock), s);
        }

        private EnumSkyBlock(string s, int i, int j)
        {
            //base(s, i);
            field_984_c = j;
        }

        public static EnumSkyBlock Sky;
        public static EnumSkyBlock Block;
        public int field_984_c;
        private static EnumSkyBlock[] field_983_d; /* synthetic field */

        static EnumSkyBlock()
        {
            Sky = new EnumSkyBlock("Sky", 0, 15);
            Block = new EnumSkyBlock("Block", 1, 0);
            field_983_d = (new EnumSkyBlock[]
                           {
                               Sky, Block
                           });
        }
    }
}