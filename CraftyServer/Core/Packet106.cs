using java.io;

namespace CraftyServer.Core
{
    public class Packet106 : Packet
    {
        public bool field_20035_c;
        public short shortWindowId;
        public int windowId;

        public Packet106()
        {
        }

        public Packet106(int i, short word0, bool flag)
        {
            windowId = i;
            shortWindowId = word0;
            field_20035_c = flag;
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.func_20008_a(this);
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            windowId = datainputstream.readByte();
            shortWindowId = datainputstream.readShort();
            field_20035_c = datainputstream.readByte() != 0;
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeByte(windowId);
            dataoutputstream.writeShort(shortWindowId);
            dataoutputstream.writeByte(field_20035_c ? 1 : 0);
        }

        public override int getPacketSize()
        {
            return 4;
        }
    }
}