using java.io;

namespace CraftyServer.Core
{
    public class Packet10Flying : Packet
    {
        public bool moving;
        public bool onGround;
        public float pitch;
        public bool rotating;
        public double stance;
        public double xPosition;
        public double yPosition;
        public float yaw;
        public double zPosition;

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleFlying(this);
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            onGround = datainputstream.read() != 0;
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.write(onGround ? 1 : 0);
        }

        public override int getPacketSize()
        {
            return 1;
        }
    }
}