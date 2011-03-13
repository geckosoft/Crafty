using java.io;

namespace CraftyServer.Core
{
    public class Packet32EntityLook : Packet30Entity
    {
        public Packet32EntityLook()
        {
            rotating = true;
        }

        public Packet32EntityLook(int i, byte byte0, byte byte1)
            : base(i)
        {
            yaw = byte0;
            pitch = byte1;
            rotating = true;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            base.readPacketData(datainputstream);
            yaw = datainputstream.readByte();
            pitch = datainputstream.readByte();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            base.writePacketData(dataoutputstream);
            dataoutputstream.writeByte(yaw);
            dataoutputstream.writeByte(pitch);
        }

        public override int getPacketSize()
        {
            return 6;
        }
    }
}