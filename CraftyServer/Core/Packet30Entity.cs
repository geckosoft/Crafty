using java.io;

namespace CraftyServer.Core
{
    public class Packet30Entity : Packet
    {
        public Packet30Entity()
        {
            rotating = false;
        }

        public Packet30Entity(int i)
        {
            rotating = false;
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
            nethandler.handleEntity(this);
        }

        public override int getPacketSize()
        {
            return 4;
        }

        public int entityId;
        public byte xPosition;
        public byte yPosition;
        public byte zPosition;
        public byte yaw;
        public byte pitch;
        public bool rotating;
    }
}