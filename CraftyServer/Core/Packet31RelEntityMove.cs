using java.io;

namespace CraftyServer.Core
{
    public class Packet31RelEntityMove : Packet30Entity
    {
        public Packet31RelEntityMove()
        {
        }

        public Packet31RelEntityMove(int i, byte byte0, byte byte1, byte byte2) : base(i)
        {
            xPosition = byte0;
            yPosition = byte1;
            zPosition = byte2;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            base.readPacketData(datainputstream);
            xPosition = datainputstream.readByte();
            yPosition = datainputstream.readByte();
            zPosition = datainputstream.readByte();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            base.writePacketData(dataoutputstream);
            dataoutputstream.writeByte(xPosition);
            dataoutputstream.writeByte(yPosition);
            dataoutputstream.writeByte(zPosition);
        }

        public override int getPacketSize()
        {
            return 7;
        }
    }
}