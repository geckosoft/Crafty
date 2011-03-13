using java.io;

namespace CraftyServer.Core
{
    public class Packet100 : Packet
    {
        public Packet100()
        {
        }

        public Packet100(int i, int j, string s, int k)
        {
            windowId = i;
            inventoryType = j;
            windowTitle = s;
            slotsCount = k;
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.func_20004_a(this);
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            windowId = datainputstream.readByte();
            inventoryType = datainputstream.readByte();
            windowTitle = datainputstream.readUTF();
            slotsCount = datainputstream.readByte();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeByte(windowId);
            dataoutputstream.writeByte(inventoryType);
            dataoutputstream.writeUTF(windowTitle);
            dataoutputstream.writeByte(slotsCount);
        }

        public override int getPacketSize()
        {
            return 3 + windowTitle.Length;
        }

        public int windowId;
        public int inventoryType;
        public string windowTitle;
        public int slotsCount;
    }
}