namespace CraftyServer.Core
{
    public class EnumMovingObjectType
    {
        public static EnumMovingObjectType TILE;
        public static EnumMovingObjectType ENTITY;
        private static readonly EnumMovingObjectType[] field_21124_c; /* synthetic field */

        static EnumMovingObjectType()
        {
            TILE = new EnumMovingObjectType("TILE", 0);
            ENTITY = new EnumMovingObjectType("ENTITY", 1);
            field_21124_c = (new[]
                             {
                                 TILE, ENTITY
                             });
        }

        private EnumMovingObjectType(string s, int i)
        {
            //base(s, i);
        }

        public static EnumMovingObjectType[] values()
        {
            return (EnumMovingObjectType[]) field_21124_c.Clone();
        }

        public static EnumMovingObjectType valueOf(string s)
        {
            return null; // (EnumMovingObjectType)Enum.valueOf(typeof(EnumMovingObjectType), s);
        }
    }
}