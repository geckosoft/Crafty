using java.io;

namespace CraftyServer.Core
{
    public class Packet255KickDisconnect : Packet
    {
        public string reason;

        public Packet255KickDisconnect()
        {
        }

        public Packet255KickDisconnect(string s)
        {
            reason = s;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            reason = datainputstream.readUTF();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeUTF(reason);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleKickDisconnect(this);
        }

        public override int getPacketSize()
        {
            return reason.Length;
        }
    }
}