using java.lang;

namespace CraftyServer.Core
{
    internal class NetworkReaderThread : Thread
    {
        private readonly NetworkManager netManager; /* synthetic field */

        public NetworkReaderThread(NetworkManager networkmanager, string s) : base(s)
        {
            netManager = networkmanager;
        }

        public override void run()
        {
            lock (NetworkManager.threadSyncObject)
            {
                NetworkManager.numReadThreads++;
            }
            while (NetworkManager.isRunning(netManager) && !NetworkManager.isServerTerminating(netManager))
            {
                NetworkManager.readNetworkPacket(netManager);
                try
                {
                    sleep(0L);
                }
                catch (InterruptedException interruptedexception)
                {
                }
            }
            lock (NetworkManager.threadSyncObject)
            {
                NetworkManager.numReadThreads--;
            }
//        break MISSING_BLOCK_LABEL_123;
//        Exception exception2;
//        exception2;
            lock (NetworkManager.threadSyncObject)
            {
                NetworkManager.numReadThreads--;
            }
//        throw exception2;
        }
    }
}