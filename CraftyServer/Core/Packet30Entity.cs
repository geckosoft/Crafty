using java.io;

namespace CraftyServer.Core
{
    public class Packet30Entity : Packet
    {
        public int entityId;
        public byte pitch;
        public bool rotating;
        public byte xPosition;
        public byte yPosition;
        public byte yaw;
        public byte zPosition;

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
    }
}