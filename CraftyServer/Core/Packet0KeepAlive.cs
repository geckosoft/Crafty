using java.io;

namespace CraftyServer.Core
{
    public class Packet0KeepAlive : Packet
    {
        public Packet0KeepAlive()
        {
        }

        public override void processPacket(NetHandler nethandler)
        {
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
        }

        public override int getPacketSize()
        {
            return 0;
        }
    }
}