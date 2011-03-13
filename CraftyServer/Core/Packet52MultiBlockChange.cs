using java.io;

namespace CraftyServer.Core
{
    public class Packet52MultiBlockChange : Packet
    {
        public Packet52MultiBlockChange()
        {
            isChunkDataPacket = true;
        }

        public Packet52MultiBlockChange(int i, int j, short[] aword0, int k, World world)
        {
            isChunkDataPacket = true;
            xPosition = i;
            zPosition = j;
            size = k;
            coordinateArray = new short[k];
            typeArray = new byte[k];
            metadataArray = new byte[k];
            Chunk chunk = world.getChunkFromChunkCoords(i, j);
            for (int l = 0; l < k; l++)
            {
                int i1 = aword0[l] >> 12 & 0xf;
                int j1 = aword0[l] >> 8 & 0xf;
                int k1 = aword0[l] & 0xff;
                coordinateArray[l] = aword0[l];
                typeArray[l] = (byte) chunk.getBlockID(i1, k1, j1);
                metadataArray[l] = (byte) chunk.getBlockMetadata(i1, k1, j1);
            }
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            xPosition = datainputstream.readInt();
            zPosition = datainputstream.readInt();
            size = datainputstream.readShort() & 0xffff;
            coordinateArray = new short[size];
            typeArray = new byte[size];
            metadataArray = new byte[size];
            for (int i = 0; i < size; i++)
            {
                coordinateArray[i] = datainputstream.readShort();
            }

            datainputstream.readFully(typeArray);
            datainputstream.readFully(metadataArray);
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(xPosition);
            dataoutputstream.writeInt(zPosition);
            dataoutputstream.writeShort((short) size);
            for (int i = 0; i < size; i++)
            {
                dataoutputstream.writeShort(coordinateArray[i]);
            }

            dataoutputstream.write(typeArray);
            dataoutputstream.write(metadataArray);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleMultiBlockChange(this);
        }

        public override int getPacketSize()
        {
            return 10 + size*4;
        }

        public int xPosition;
        public int zPosition;
        public short[] coordinateArray;
        public byte[] typeArray;
        public byte[] metadataArray;
        public int size;
    }
}