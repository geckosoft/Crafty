using java.lang;

namespace CraftyServer.Core
{
    internal class NetworkWriterThread : Thread
    {
        private readonly NetworkManager netManager; /* synthetic field */

        public NetworkWriterThread(NetworkManager networkmanager, string s)
            : base(s)
        {
            netManager = networkmanager;
        }

        public override void run()
        {
            lock (NetworkManager.threadSyncObject)
            {
                NetworkManager.numWriteThreads++;
            }
            for (; NetworkManager.isRunning(netManager); NetworkManager.sendNetworkPacket(netManager))
            {
            }
            lock (NetworkManager.threadSyncObject)
            {
                NetworkManager.numWriteThreads--;
            }
//        break MISSING_BLOCK_LABEL_105;
//        Exception exception2;
//        exception2;
            lock (NetworkManager.threadSyncObject)
            {
                NetworkManager.numWriteThreads--;
            }
//        throw exception2;
        }
    }
}