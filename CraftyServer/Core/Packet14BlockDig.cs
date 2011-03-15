using java.io;

namespace CraftyServer.Core
{
    public class Packet14BlockDig : Packet
    {
        public int face;
        public int status;
        public int xPosition;
        public int yPosition;
        public int zPosition;

        public override void readPacketData(DataInputStream datainputstream)
        {
            status = datainputstream.read();
            xPosition = datainputstream.readInt();
            yPosition = datainputstream.read();
            zPosition = datainputstream.readInt();
            face = datainputstream.read();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.write(status);
            dataoutputstream.writeInt(xPosition);
            dataoutputstream.write(yPosition);
            dataoutputstream.writeInt(zPosition);
            dataoutputstream.write(face);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleBlockDig(this);
        }

        public override int getPacketSize()
        {
            return 11;
        }
    }
}