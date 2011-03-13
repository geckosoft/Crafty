using java.io;

namespace CraftyServer.Core
{
    public class Packet19 : Packet
    {
        public Packet19()
        {
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            entityId = datainputstream.readInt();
            state = datainputstream.readByte();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(entityId);
            dataoutputstream.writeByte(state);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.func_21001_a(this);
        }

        public override int getPacketSize()
        {
            return 5;
        }

        public int entityId;
        public int state;
    }
}