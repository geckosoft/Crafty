using java.io;

namespace CraftyServer.Core
{
    public class Packet101 : Packet
    {
        public Packet101()
        {
        }

        public Packet101(int i)
        {
            windowId = i;
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleCraftingGuiClosedPacked(this);
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            windowId = datainputstream.readByte();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeByte(windowId);
        }

        public override int getPacketSize()
        {
            return 1;
        }

        public int windowId;
    }
}