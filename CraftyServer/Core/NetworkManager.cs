using java.io;
using java.lang;
using java.net;
using java.util;

namespace CraftyServer.Core
{
    public class NetworkManager
    {
        public static object threadSyncObject = new object();
        public static int numReadThreads;
        public static int numWriteThreads;
        private readonly List chunkDataPackets;
        private readonly List dataPackets;
        private readonly List readPackets;
        private readonly Thread readThread;
        private readonly SocketAddress remoteSocketAddress;
        private readonly object sendQueueLock;
        private readonly Thread writeThread;
        public int chunkDataSendCounter;
        private int field_20175_w;
        private object[] field_20176_t;
        private bool isTerminating;
        public bool m_isRunning;
        public bool m_isServerTerminating;
        private NetHandler netHandler;
        private Socket networkSocket;
        private int sendQueueByteLength;
        private DataInputStream socketInputStream;
        private DataOutputStream socketOutputStream;
        private string terminationReason;
        private int timeSinceLastRead;

        public NetworkManager(Socket socket, string s, NetHandler nethandler)
        {
            sendQueueLock = new object();
            m_isRunning = true;
            readPackets = Collections.synchronizedList(new ArrayList());
            dataPackets = Collections.synchronizedList(new ArrayList());
            chunkDataPackets = Collections.synchronizedList(new ArrayList());
            m_isServerTerminating = false;
            isTerminating = false;
            terminationReason = "";
            timeSinceLastRead = 0;
            sendQueueByteLength = 0;
            chunkDataSendCounter = 0;
            field_20175_w = 50;
            networkSocket = socket;
            remoteSocketAddress = socket.getRemoteSocketAddress();
            netHandler = nethandler;
            socket.setTrafficClass(24);
            socketInputStream = new DataInputStream(socket.getInputStream());
            socketOutputStream = new DataOutputStream(socket.getOutputStream());
            readThread = new NetworkReaderThread(this, (new StringBuilder()).append(s).append(" read thread").toString());
            writeThread = new NetworkWriterThread(this,
                                                  (new StringBuilder()).append(s).append(" write thread").toString());
            readThread.start();
            writeThread.start();
        }

        public void setNetHandler(NetHandler nethandler)
        {
            netHandler = nethandler;
        }

        public void addToSendQueue(Packet packet)
        {
            if (m_isServerTerminating)
            {
                return;
            }

            lock (sendQueueLock)
            {
                sendQueueByteLength += packet.getPacketSize() + 1;
                if (packet.isChunkDataPacket)
                {
                    chunkDataPackets.add(packet);
                }
                else
                {
                    dataPackets.add(packet);
                }
            }
        }

        private void sendPacket()
        {
            try
            {
                bool flag = true;
                if (!dataPackets.isEmpty() &&
                    (chunkDataSendCounter == 0 ||
                     java.lang.System.currentTimeMillis() - ((Packet) dataPackets.get(0)).creationTimeMillis >=
                     chunkDataSendCounter))
                {
                    flag = false;
                    Packet packet;
                    lock (sendQueueLock)
                    {
                        packet = (Packet) dataPackets.remove(0);
                        sendQueueByteLength -= packet.getPacketSize() + 1;
                    }
                    Packet.writePacket(packet, socketOutputStream);
                }
                if ((flag || field_20175_w-- <= 0) && !chunkDataPackets.isEmpty() &&
                    (chunkDataSendCounter == 0 ||
                     java.lang.System.currentTimeMillis() - ((Packet) chunkDataPackets.get(0)).creationTimeMillis >=
                     chunkDataSendCounter))
                {
                    flag = false;
                    Packet packet1;
                    lock (sendQueueLock)
                    {
                        packet1 = (Packet) chunkDataPackets.remove(0);
                        sendQueueByteLength -= packet1.getPacketSize() + 1;
                    }
                    Packet.writePacket(packet1, socketOutputStream);
                    field_20175_w = 50;
                }
                if (flag)
                {
                    Thread.sleep(10L);
                }
            }
            catch (InterruptedException interruptedexception)
            {
            }
            catch (Exception exception)
            {
                if (!isTerminating)
                {
                    onNetworkError(exception);
                }
            }
        }

        private void readPacket()
        {
            try
            {
                Packet packet = Packet.readPacket(socketInputStream);
                if (packet != null)
                {
                    readPackets.add(packet);
                }
                else
                {
                    networkShutdown("disconnect.endOfStream", new object[0]);
                }
            }
            catch (Exception exception)
            {
                if (!isTerminating)
                {
                    onNetworkError(exception);
                }
            }
        }

        private void onNetworkError(Exception exception)
        {
            exception.printStackTrace();
            networkShutdown("disconnect.genericReason", new object[]
                                                        {
                                                            (new StringBuilder()).append("Internal exception: ").append(
                                                                exception.toString()).toString()
                                                        });
        }

        public void networkShutdown(string s, object[] aobj)
        {
            if (!m_isRunning)
            {
                return;
            }
            isTerminating = true;
            terminationReason = s;
            field_20176_t = aobj;
            (new NetworkMasterThread(this)).start();
            m_isRunning = false;
            try
            {
                socketInputStream.close();
                socketInputStream = null;
            }
            catch (Throwable)
            {
            }
            try
            {
                socketOutputStream.close();
                socketOutputStream = null;
            }
            catch (Throwable)
            {
            }
            try
            {
                networkSocket.close();
                networkSocket = null;
            }
            catch (Throwable)
            {
            }
        }

        public void processReadPackets()
        {
            if (sendQueueByteLength > 0x100000)
            {
                networkShutdown("disconnect.overflow", new object[0]);
            }
            if (readPackets.isEmpty())
            {
                if (timeSinceLastRead++ == 1200)
                {
                    networkShutdown("disconnect.timeout", new object[0]);
                }
            }
            else
            {
                timeSinceLastRead = 0;
            }
            Packet packet;
            for (int i = 100; !readPackets.isEmpty() && i-- >= 0; packet.processPacket(netHandler))
            {
                packet = (Packet) readPackets.remove(0);
            }

            if (isTerminating && readPackets.isEmpty())
            {
                netHandler.handleErrorMessage(terminationReason, field_20176_t);
            }
        }

        public SocketAddress getRemoteAddress()
        {
            return remoteSocketAddress;
        }

        public void serverShutdown()
        {
            m_isServerTerminating = true;
            readThread.interrupt();
            (new ThreadMonitorConnection(this)).start();
        }

        public int getNumChunkDataPackets()
        {
            return chunkDataPackets.size();
        }

        public static bool isRunning(NetworkManager networkmanager)
        {
            return networkmanager.m_isRunning;
        }

        public static bool isServerTerminating(NetworkManager networkmanager)
        {
            return networkmanager.m_isServerTerminating;
        }

        public static void readNetworkPacket(NetworkManager networkmanager)
        {
            networkmanager.readPacket();
        }

        public static void sendNetworkPacket(NetworkManager networkmanager)
        {
            networkmanager.sendPacket();
        }

        public static Thread getReadThread(NetworkManager networkmanager)
        {
            return networkmanager.readThread;
        }

        public static Thread getWriteThread(NetworkManager networkmanager)
        {
            return networkmanager.writeThread;
        }
    }
}