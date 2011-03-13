using java.io;

namespace CraftyServer.Core
{
    public class Packet12PlayerLook : Packet10Flying
    {
        public Packet12PlayerLook()
        {
            rotating = true;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            yaw = datainputstream.readFloat();
            pitch = datainputstream.readFloat();
            base.readPacketData(datainputstream);
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeFloat(yaw);
            dataoutputstream.writeFloat(pitch);
            base.writePacketData(dataoutputstream);
        }

        public override int getPacketSize()
        {
            return 9;
        }
    }
}