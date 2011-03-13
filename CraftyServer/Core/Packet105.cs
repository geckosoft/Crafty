using java.io;

namespace CraftyServer.Core
{
    public class Packet105 : Packet
    {
        public Packet105()
        {
        }

        public Packet105(int i, int j, int k)
        {
            windowId = i;
            progressBar = j;
            progressBarValue = k;
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.func_20002_a(this);
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            windowId = datainputstream.readByte();
            progressBar = datainputstream.readShort();
            progressBarValue = datainputstream.readShort();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeByte(windowId);
            dataoutputstream.writeShort(progressBar);
            dataoutputstream.writeShort(progressBarValue);
        }

        public override int getPacketSize()
        {
            return 5;
        }

        public int windowId;
        public int progressBar;
        public int progressBarValue;
    }
}