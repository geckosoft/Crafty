using CraftyServer.Server;
using java.util;
using javax.swing;

namespace CraftyServer.Core
{
    public class PlayerListBox : JList
                                 , IUpdatePlayerListBox
    {
        public PlayerListBox(MinecraftServer minecraftserver)
        {
            updateCounter = 0;
            mcServer = minecraftserver;
            minecraftserver.func_6022_a(this);
        }

        public void update()
        {
            if (updateCounter++%20 == 0)
            {
                Vector vector = new Vector();
                for (int i = 0; i < mcServer.configManager.playerEntities.size(); i++)
                {
                    vector.add(((EntityPlayerMP) mcServer.configManager.playerEntities.get(i)).username);
                }

                setListData(vector);
            }
        }

        private MinecraftServer mcServer;
        private int updateCounter;
    }
}