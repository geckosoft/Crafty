using java.io;

namespace CraftyServer.Core
{
    public class Packet38 : Packet
    {
        public int entityId;
        public byte entityStatus;

        public Packet38()
        {
        }

        public Packet38(int i, byte byte0)
        {
            entityId = i;
            entityStatus = byte0;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            entityId = datainputstream.readInt();
            entityStatus = datainputstream.readByte();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(entityId);
            dataoutputstream.writeByte(entityStatus);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.func_9001_a(this);
        }

        public override int getPacketSize()
        {
            return 5;
        }
    }
}