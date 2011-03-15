using CraftyServer.Server;
using java.util;

namespace CraftyServer.Core
{
    public class PlayerManager
    {
        private readonly int[][] field_22089_e = new[]
                                                 {
                                                     new[]
                                                     {
                                                         1, 0
                                                     }, new[]
                                                        {
                                                            0, 1
                                                        }, new[]
                                                           {
                                                               -1, 0
                                                           }, new[]
                                                              {
                                                                  0, -1
                                                              }
                                                 };

        private readonly List field_833_c;
        private readonly MCHashTable2 field_9215_b;
        private readonly List field_9216_a;
        private readonly MinecraftServer mcServer;

        public PlayerManager(MinecraftServer minecraftserver)
        {
            field_9216_a = new ArrayList();
            field_9215_b = new MCHashTable2();
            field_833_c = new ArrayList();
            mcServer = minecraftserver;
        }

        public void func_538_a()
        {
            for (int i = 0; i < field_833_c.size(); i++)
            {
                ((PlayerInstance) field_833_c.get(i)).func_777_a();
            }

            field_833_c.clear();
        }

        private PlayerInstance func_537_a(int i, int j, bool flag)
        {
            long l = i + 0x7fffffffL | j + 0x7fffffffL << 32;
            var playerinstance = (PlayerInstance) field_9215_b.func_677_a(l);
            if (playerinstance == null && flag)
            {
                playerinstance = new PlayerInstance(this, i, j);
                field_9215_b.func_675_a(l, playerinstance);
            }
            return playerinstance;
        }

        public void func_535_a(int i, int j, int k)
        {
            int l = i >> 4;
            int i1 = k >> 4;
            PlayerInstance playerinstance = func_537_a(l, i1, false);
            if (playerinstance != null)
            {
                playerinstance.func_775_a(i & 0xf, j, k & 0xf);
            }
        }

        public void addPlayer(EntityPlayerMP entityplayermp)
        {
            int i = (int) entityplayermp.posX >> 4;
            int j = (int) entityplayermp.posZ >> 4;
            entityplayermp.field_9155_d = entityplayermp.posX;
            entityplayermp.field_9154_e = entityplayermp.posZ;
            int k = 0;
            byte byte0 = 10;
            int l = 0;
            int i1 = 0;
            func_537_a(i, j, true).func_779_a(entityplayermp);
            for (int j1 = 1; j1 <= byte0*2; j1++)
            {
                for (int l1 = 0; l1 < 2; l1++)
                {
                    int[] ai = field_22089_e[k++%4];
                    for (int i2 = 0; i2 < j1; i2++)
                    {
                        l += ai[0];
                        i1 += ai[1];
                        func_537_a(i + l, j + i1, true).func_779_a(entityplayermp);
                    }
                }
            }

            k %= 4;
            for (int k1 = 0; k1 < byte0*2; k1++)
            {
                l += field_22089_e[k][0];
                i1 += field_22089_e[k][1];
                func_537_a(i + l, j + i1, true).func_779_a(entityplayermp);
            }

            field_9216_a.add(entityplayermp);
        }

        public void removePlayer(EntityPlayerMP entityplayermp)
        {
            int i = (int) entityplayermp.field_9155_d >> 4;
            int j = (int) entityplayermp.field_9154_e >> 4;
            for (int k = i - 10; k <= i + 10; k++)
            {
                for (int l = j - 10; l <= j + 10; l++)
                {
                    PlayerInstance playerinstance = func_537_a(k, l, false);
                    if (playerinstance != null)
                    {
                        playerinstance.func_778_b(entityplayermp);
                    }
                }
            }

            field_9216_a.remove(entityplayermp);
        }

        private bool func_544_a(int i, int j, int k, int l)
        {
            int i1 = i - k;
            int j1 = j - l;
            if (i1 < -10 || i1 > 10)
            {
                return false;
            }
            return j1 >= -10 && j1 <= 10;
        }

        public void func_543_c(EntityPlayerMP entityplayermp)
        {
            int i = (int) entityplayermp.posX >> 4;
            int j = (int) entityplayermp.posZ >> 4;
            double d = entityplayermp.field_9155_d - entityplayermp.posX;
            double d1 = entityplayermp.field_9154_e - entityplayermp.posZ;
            double d2 = d*d + d1*d1;
            if (d2 < 64D)
            {
                return;
            }
            int k = (int) entityplayermp.field_9155_d >> 4;
            int l = (int) entityplayermp.field_9154_e >> 4;
            int i1 = i - k;
            int j1 = j - l;
            if (i1 == 0 && j1 == 0)
            {
                return;
            }
            for (int k1 = i - 10; k1 <= i + 10; k1++)
            {
                for (int l1 = j - 10; l1 <= j + 10; l1++)
                {
                    if (!func_544_a(k1, l1, k, l))
                    {
                        func_537_a(k1, l1, true).func_779_a(entityplayermp);
                    }
                    if (func_544_a(k1 - i1, l1 - j1, i, j))
                    {
                        continue;
                    }
                    PlayerInstance playerinstance = func_537_a(k1 - i1, l1 - j1, false);
                    if (playerinstance != null)
                    {
                        playerinstance.func_778_b(entityplayermp);
                    }
                }
            }

            entityplayermp.field_9155_d = entityplayermp.posX;
            entityplayermp.field_9154_e = entityplayermp.posZ;
        }

        public int func_542_b()
        {
            return 144;
        }

        public static MinecraftServer getMinecraftServer(PlayerManager playermanager)
        {
            return playermanager.mcServer;
        }

        public static MCHashTable2 func_539_b(PlayerManager playermanager)
        {
            return playermanager.field_9215_b;
        }

        public static List func_533_c(PlayerManager playermanager)
        {
            return playermanager.field_833_c;
        }
    }
}