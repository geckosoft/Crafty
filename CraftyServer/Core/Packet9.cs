using java.io;

namespace CraftyServer.Core
{
    public class Packet9 : Packet
    {
        public Packet9()
        {
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleRespawnPacket(this);
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