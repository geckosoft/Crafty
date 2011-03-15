using java.io;

namespace CraftyServer.Core
{
    public class Packet130 : Packet
    {
        public string[] signLines;
        public int xPosition;
        public int yPosition;
        public int zPosition;

        public Packet130()
        {
            isChunkDataPacket = true;
        }

        public Packet130(int i, int j, int k, string[] ask)
        {
            isChunkDataPacket = true;
            xPosition = i;
            yPosition = j;
            zPosition = k;
            signLines = ask;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            xPosition = datainputstream.readInt();
            yPosition = datainputstream.readShort();
            zPosition = datainputstream.readInt();
            signLines = new string[4];
            for (int i = 0; i < 4; i++)
            {
                signLines[i] = datainputstream.readUTF();
            }
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(xPosition);
            dataoutputstream.writeShort(yPosition);
            dataoutputstream.writeInt(zPosition);
            for (int i = 0; i < 4; i++)
            {
                dataoutputstream.writeUTF(signLines[i]);
            }
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.func_20005_a(this);
        }

        public override int getPacketSize()
        {
            int i = 0;
            for (int j = 0; j < 4; j++)
            {
                i += signLines[j].Length;
            }

            return i;
        }
    }
}