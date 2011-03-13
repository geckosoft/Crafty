using java.io;

namespace CraftyServer.Core
{
    public class Packet3Chat : Packet
    {
        public Packet3Chat()
        {
        }

        public Packet3Chat(string s)
        {
            message = s;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            message = datainputstream.readUTF();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeUTF(message);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleChat(this);
        }

        public override int getPacketSize()
        {
            return message.Length;
        }

        public string message;
    }
}