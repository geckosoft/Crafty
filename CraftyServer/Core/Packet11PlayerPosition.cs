using java.io;

namespace CraftyServer.Core
{
    public class Packet11PlayerPosition : Packet10Flying
    {
        public Packet11PlayerPosition()
        {
            moving = true;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            xPosition = datainputstream.readDouble();
            yPosition = datainputstream.readDouble();
            stance = datainputstream.readDouble();
            zPosition = datainputstream.readDouble();
            base.readPacketData(datainputstream);
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeDouble(xPosition);
            dataoutputstream.writeDouble(yPosition);
            dataoutputstream.writeDouble(stance);
            dataoutputstream.writeDouble(zPosition);
            base.writePacketData(dataoutputstream);
        }

        public override int getPacketSize()
        {
            return 33;
        }
    }
}