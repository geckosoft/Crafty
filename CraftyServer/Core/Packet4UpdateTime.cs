using java.io;

namespace CraftyServer.Core
{
    public class Packet4UpdateTime : Packet
    {
        public long time;

        public Packet4UpdateTime()
        {
        }

        public Packet4UpdateTime(long l)
        {
            time = l;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            time = datainputstream.readLong();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeLong(time);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleUpdateTime(this);
        }

        public override int getPacketSize()
        {
            return 8;
        }
    }
}