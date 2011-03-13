using java.io;

namespace CraftyServer.Core
{
    public class Packet50PreChunk : Packet
    {
        public Packet50PreChunk()
        {
            isChunkDataPacket = false;
        }

        public Packet50PreChunk(int i, int j, bool flag)
        {
            isChunkDataPacket = false;
            xPosition = i;
            yPosition = j;
            mode = flag;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            xPosition = datainputstream.readInt();
            yPosition = datainputstream.readInt();
            mode = datainputstream.read() != 0;
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(xPosition);
            dataoutputstream.writeInt(yPosition);
            dataoutputstream.write(mode ? 1 : 0);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handlePreChunk(this);
        }

        public override int getPacketSize()
        {
            return 9;
        }

        public int xPosition;
        public int yPosition;
        public bool mode;
    }
}