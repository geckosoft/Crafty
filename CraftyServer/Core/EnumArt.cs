namespace CraftyServer.Core
{
    public class EnumArt
    {
        public static EnumArt Kebab;
        public static EnumArt Aztec;
        public static EnumArt Alban;
        public static EnumArt Aztec2;
        public static EnumArt Bomb;
        public static EnumArt Plant;
        public static EnumArt Wasteland;
        public static EnumArt Pool;
        public static EnumArt Courbet;
        public static EnumArt Sea;
        public static EnumArt Sunset;
        public static EnumArt Creebet;
        public static EnumArt Wanderer;
        public static EnumArt Graham;
        public static EnumArt Match;
        public static EnumArt Bust;
        public static EnumArt Stage;
        public static EnumArt Void;
        public static EnumArt SkullAndRoses;
        public static EnumArt Fighters;
        public static EnumArt Pointer;
        public static EnumArt Pigscene;
        public static EnumArt BurningSkull;
        public static EnumArt Skeleton;
        public static EnumArt DonkeyKong;
        private static readonly EnumArt[] field_863_D;

        public int offsetX;
        public int offsetY;
        public int sizeX;
        public int sizeY;
        public string title;

        static EnumArt()
        {
            Kebab = new EnumArt("Kebab", 0, "Kebab", 16, 16, 0, 0);
            Aztec = new EnumArt("Aztec", 1, "Aztec", 16, 16, 16, 0);
            Alban = new EnumArt("Alban", 2, "Alban", 16, 16, 32, 0);
            Aztec2 = new EnumArt("Aztec2", 3, "Aztec2", 16, 16, 48, 0);
            Bomb = new EnumArt("Bomb", 4, "Bomb", 16, 16, 64, 0);
            Plant = new EnumArt("Plant", 5, "Plant", 16, 16, 80, 0);
            Wasteland = new EnumArt("Wasteland", 6, "Wasteland", 16, 16, 96, 0);
            Pool = new EnumArt("Pool", 7, "Pool", 32, 16, 0, 32);
            Courbet = new EnumArt("Courbet", 8, "Courbet", 32, 16, 32, 32);
            Sea = new EnumArt("Sea", 9, "Sea", 32, 16, 64, 32);
            Sunset = new EnumArt("Sunset", 10, "Sunset", 32, 16, 96, 32);
            Creebet = new EnumArt("Creebet", 11, "Creebet", 32, 16, 128, 32);
            Wanderer = new EnumArt("Wanderer", 12, "Wanderer", 16, 32, 0, 64);
            Graham = new EnumArt("Graham", 13, "Graham", 16, 32, 16, 64);
            Match = new EnumArt("Match", 14, "Match", 32, 32, 0, 128);
            Bust = new EnumArt("Bust", 15, "Bust", 32, 32, 32, 128);
            Stage = new EnumArt("Stage", 16, "Stage", 32, 32, 64, 128);
            Void = new EnumArt("Void", 17, "Void", 32, 32, 96, 128);
            SkullAndRoses = new EnumArt("SkullAndRoses", 18, "SkullAndRoses", 32, 32, 128, 128);
            Fighters = new EnumArt("Fighters", 19, "Fighters", 64, 32, 0, 96);
            Pointer = new EnumArt("Pointer", 20, "Pointer", 64, 64, 0, 192);
            Pigscene = new EnumArt("Pigscene", 21, "Pigscene", 64, 64, 64, 192);
            BurningSkull = new EnumArt("BurningSkull", 22, "BurningSkull", 64, 64, 128, 192);
            Skeleton = new EnumArt("Skeleton", 23, "Skeleton", 64, 48, 192, 64);
            DonkeyKong = new EnumArt("DonkeyKong", 24, "DonkeyKong", 64, 48, 192, 112);
            field_863_D = (new[]
                           {
                               Kebab, Aztec, Alban, Aztec2, Bomb, Plant, Wasteland, Pool, Courbet, Sea,
                               Sunset, Creebet, Wanderer, Graham, Match, Bust, Stage, Void, SkullAndRoses, Fighters,
                               Pointer, Pigscene, BurningSkull, Skeleton, DonkeyKong
                           });
        }

        private EnumArt(string s, int i, string s1, int j, int k, int l, int i1)
        {
            title = s1;
            sizeX = j;
            sizeY = k;
            offsetX = l;
            offsetY = i1;
        }

        public static EnumArt[] values()
        {
            return (EnumArt[]) field_863_D.Clone();
        }
    }
}