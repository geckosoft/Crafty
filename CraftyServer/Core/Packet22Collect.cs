using java.io;

namespace CraftyServer.Core
{
    public class Packet22Collect : Packet
    {
        public int collectedEntityId;
        public int collectorEntityId;

        public Packet22Collect()
        {
        }

        public Packet22Collect(int i, int j)
        {
            collectedEntityId = i;
            collectorEntityId = j;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            collectedEntityId = datainputstream.readInt();
            collectorEntityId = datainputstream.readInt();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(collectedEntityId);
            dataoutputstream.writeInt(collectorEntityId);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleCollect(this);
        }

        public override int getPacketSize()
        {
            return 8;
        }
    }
}