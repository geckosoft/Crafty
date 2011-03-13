namespace CraftyServer.Core
{
    public class EnumMobType
    {
        public static EnumMobType[] values()
        {
            return (EnumMobType[]) field_990_d.Clone();
        }

        public static EnumMobType valueOf(string s)
        {
            return null; // return (EnumMobType)Enum.valueOf(typeof(EnumMobType), s);
        }

        private EnumMobType(string s, int i)
        {
        }

        public static EnumMobType everything;
        public static EnumMobType mobs;
        public static EnumMobType players;
        private static EnumMobType[] field_990_d; /* synthetic field */

        static EnumMobType()
        {
            everything = new EnumMobType("everything", 0);
            mobs = new EnumMobType("mobs", 1);
            players = new EnumMobType("players", 2);
            field_990_d = (new EnumMobType[]
                           {
                               everything, mobs, players
                           });
        }
    }
}