using java.io;

namespace CraftyServer.Core
{
    public class Packet10Flying : Packet
    {
        public Packet10Flying()
        {
        }

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

        public double xPosition;
        public double yPosition;
        public double zPosition;
        public double stance;
        public float yaw;
        public float pitch;
        public bool onGround;
        public bool moving;
        public bool rotating;
    }
}