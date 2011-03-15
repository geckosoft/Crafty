using java.lang;

namespace CraftyServer.Core
{
    internal class NetworkMasterThread : Thread
    {
        private readonly NetworkManager netManager; /* synthetic field */

        public NetworkMasterThread(NetworkManager networkmanager)
        {
            netManager = networkmanager;
        }

        public override void run()
        {
            try
            {
                sleep(5000L);
                if (NetworkManager.getReadThread(netManager).isAlive())
                {
                    try
                    {
                        NetworkManager.getReadThread(netManager).stop();
                    }
                    catch (Throwable throwable)
                    {
                    }
                }
                if (NetworkManager.getWriteThread(netManager).isAlive())
                {
                    try
                    {
                        NetworkManager.getWriteThread(netManager).stop();
                    }
                    catch (Throwable throwable1)
                    {
                    }
                }
            }
            catch (InterruptedException interruptedexception)
            {
                interruptedexception.printStackTrace();
            }
        }
    }
}