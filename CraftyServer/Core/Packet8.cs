using java.io;

namespace CraftyServer.Core
{
    public class Packet8 : Packet
    {
        public int healthMP;

        public Packet8()
        {
        }

        public Packet8(int i)
        {
            healthMP = i;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            healthMP = datainputstream.readShort();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeShort(healthMP);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleHealth(this);
        }

        public override int getPacketSize()
        {
            return 2;
        }
    }
}