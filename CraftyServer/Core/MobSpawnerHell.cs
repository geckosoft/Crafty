using java.lang;

namespace CraftyServer.Core
{
    public class MobSpawnerHell : MobSpawnerBase
    {
        public MobSpawnerHell()
        {
            biomeMonsters = (new Class[]
                             {
                                 typeof (EntityGhast), typeof (EntityPigZombie)
                             });
            biomeCreatures = new Class[0];
        }
    }
}