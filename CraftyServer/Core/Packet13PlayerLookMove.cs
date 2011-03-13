using java.io;

namespace CraftyServer.Core
{
    public class Packet13PlayerLookMove : Packet10Flying
    {
        public Packet13PlayerLookMove()
        {
            rotating = true;
            moving = true;
        }

        public Packet13PlayerLookMove(double d, double d1, double d2, double d3, float f, float f1, bool flag)
        {
            xPosition = d;
            yPosition = d1;
            stance = d2;
            zPosition = d3;
            yaw = f;
            pitch = f1;
            onGround = flag;
            rotating = true;
            moving = true;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            xPosition = datainputstream.readDouble();
            yPosition = datainputstream.readDouble();
            stance = datainputstream.readDouble();
            zPosition = datainputstream.readDouble();
            yaw = datainputstream.readFloat();
            pitch = datainputstream.readFloat();
            base.readPacketData(datainputstream);
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeDouble(xPosition);
            dataoutputstream.writeDouble(yPosition);
            dataoutputstream.writeDouble(stance);
            dataoutputstream.writeDouble(zPosition);
            dataoutputstream.writeFloat(yaw);
            dataoutputstream.writeFloat(pitch);
            base.writePacketData(dataoutputstream);
        }

        public override int getPacketSize()
        {
            return 41;
        }
    }
}