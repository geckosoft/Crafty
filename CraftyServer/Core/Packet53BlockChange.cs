using java.io;

namespace CraftyServer.Core
{
    public class Packet53BlockChange : Packet
    {
        public int metadata;
        public int type;
        public int xPosition;
        public int yPosition;
        public int zPosition;

        public Packet53BlockChange()
        {
            isChunkDataPacket = true;
        }

        public Packet53BlockChange(int i, int j, int k, World world)
        {
            isChunkDataPacket = true;
            xPosition = i;
            yPosition = j;
            zPosition = k;
            type = world.getBlockId(i, j, k);
            metadata = world.getBlockMetadata(i, j, k);
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            xPosition = datainputstream.readInt();
            yPosition = datainputstream.read();
            zPosition = datainputstream.readInt();
            type = datainputstream.read();
            metadata = datainputstream.read();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(xPosition);
            dataoutputstream.write(yPosition);
            dataoutputstream.writeInt(zPosition);
            dataoutputstream.write(type);
            dataoutputstream.write(metadata);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleBlockChange(this);
        }

        public override int getPacketSize()
        {
            return 11;
        }
    }
}