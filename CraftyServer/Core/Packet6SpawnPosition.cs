using java.io;

namespace CraftyServer.Core
{
    public class Packet6SpawnPosition : Packet
    {
        public Packet6SpawnPosition()
        {
        }

        public Packet6SpawnPosition(int i, int j, int k)
        {
            xPosition = i;
            yPosition = j;
            zPosition = k;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            xPosition = datainputstream.readInt();
            yPosition = datainputstream.readInt();
            zPosition = datainputstream.readInt();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(xPosition);
            dataoutputstream.writeInt(yPosition);
            dataoutputstream.writeInt(zPosition);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleSpawnPosition(this);
        }

        public override int getPacketSize()
        {
            return 12;
        }

        public int xPosition;
        public int yPosition;
        public int zPosition;
    }
}