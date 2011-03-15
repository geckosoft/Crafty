using CraftyServer.Server;
using java.util;
using javax.swing;

namespace CraftyServer.Core
{
    public class PlayerListBox : JList
                                 , IUpdatePlayerListBox
    {
        private readonly MinecraftServer mcServer;
        private int updateCounter;

        public PlayerListBox(MinecraftServer minecraftserver)
        {
            updateCounter = 0;
            mcServer = minecraftserver;
            minecraftserver.func_6022_a(this);
        }

        #region IUpdatePlayerListBox Members

        public void update()
        {
            if (updateCounter++%20 == 0)
            {
                var vector = new Vector();
                for (int i = 0; i < mcServer.configManager.playerEntities.size(); i++)
                {
                    vector.add(((EntityPlayerMP) mcServer.configManager.playerEntities.get(i)).username);
                }

                setListData(vector);
            }
        }

        #endregion
    }
}