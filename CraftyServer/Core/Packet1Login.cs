using java.io;

namespace CraftyServer.Core
{
    public class Packet1Login : Packet
    {
        public Packet1Login()
        {
        }

        public Packet1Login(string s, string s1, int i, long l, byte byte0)
        {
            username = s;
            password = s1;
            protocolVersion = i;
            mapSeed = l;
            dimension = byte0;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            protocolVersion = datainputstream.readInt();
            username = datainputstream.readUTF();
            password = datainputstream.readUTF();
            mapSeed = datainputstream.readLong();
            dimension = datainputstream.readByte();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(protocolVersion);
            dataoutputstream.writeUTF(username);
            dataoutputstream.writeUTF(password);
            dataoutputstream.writeLong(mapSeed);
            dataoutputstream.writeByte(dimension);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleLogin(this);
        }

        public override int getPacketSize()
        {
            return 4 + username.Length + password.Length + 4 + 5;
        }

        public int protocolVersion;
        public string username;
        public string password;
        public long mapSeed;
        public byte dimension;
    }
}