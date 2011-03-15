using java.io;

namespace CraftyServer.Core
{
    public class Packet54 : Packet
    {
        public int instrumentType;
        public int pitch;
        public int xLocation;
        public int yLocation;
        public int zLocation;

        public Packet54()
        {
        }

        public Packet54(int i, int j, int k, int l, int i1)
        {
            xLocation = i;
            yLocation = j;
            zLocation = k;
            instrumentType = l;
            pitch = i1;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            xLocation = datainputstream.readInt();
            yLocation = datainputstream.readShort();
            zLocation = datainputstream.readInt();
            instrumentType = datainputstream.read();
            pitch = datainputstream.read();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(xLocation);
            dataoutputstream.writeShort(yLocation);
            dataoutputstream.writeInt(zLocation);
            dataoutputstream.write(instrumentType);
            dataoutputstream.write(pitch);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.func_21004_a(this);
        }

        public override int getPacketSize()
        {
            return 12;
        }
    }
}