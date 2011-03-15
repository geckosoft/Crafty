using java.io;

namespace CraftyServer.Core
{
    public class Packet16BlockItemSwitch : Packet
    {
        public int id;

        public override void readPacketData(DataInputStream datainputstream)
        {
            id = datainputstream.readShort();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeShort(id);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleBlockItemSwitch(this);
        }

        public override int getPacketSize()
        {
            return 2;
        }
    }
}