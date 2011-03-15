using java.io;

namespace CraftyServer.Core
{
    public class Packet2Handshake : Packet
    {
        public string username;

        public Packet2Handshake()
        {
        }

        public Packet2Handshake(string s)
        {
            username = s;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            username = datainputstream.readUTF();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeUTF(username);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleHandshake(this);
        }

        public override int getPacketSize()
        {
            return 4 + username.Length + 4;
        }
    }
}