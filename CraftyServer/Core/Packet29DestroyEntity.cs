using java.io;

namespace CraftyServer.Core
{
    public class Packet29DestroyEntity : Packet
    {
        public int entityId;

        public Packet29DestroyEntity()
        {
        }

        public Packet29DestroyEntity(int i)
        {
            entityId = i;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            entityId = datainputstream.readInt();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(entityId);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleDestroyEntity(this);
        }

        public override int getPacketSize()
        {
            return 4;
        }
    }
}