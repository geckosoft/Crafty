using java.io;
using java.util.zip;

namespace CraftyServer.Core
{
    public class Packet51MapChunk : Packet
    {
        public byte[] chunk;
        private int chunkSize;
        public int xPosition;
        public int xSize;
        public int yPosition;
        public int ySize;
        public int zPosition;
        public int zSize;

        public Packet51MapChunk()
        {
            isChunkDataPacket = true;
        }

        public Packet51MapChunk(int i, int j, int k, int l, int i1, int j1, World world)
        {
            isChunkDataPacket = true;
            xPosition = i;
            yPosition = j;
            zPosition = k;
            xSize = l;
            ySize = i1;
            zSize = j1;
            byte[] abyte0 = world.getChunkData(i, j, k, l, i1, j1);
            var deflater = new Deflater(1);
            try
            {
                deflater.setInput(abyte0);
                deflater.finish();
                chunk = new byte[(l*i1*j1*5)/2];
                chunkSize = deflater.deflate(chunk);
            }
            finally
            {
                deflater.end();
            }
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            xPosition = datainputstream.readInt();
            yPosition = datainputstream.readShort();
            zPosition = datainputstream.readInt();
            xSize = datainputstream.read() + 1;
            ySize = datainputstream.read() + 1;
            zSize = datainputstream.read() + 1;
            chunkSize = datainputstream.readInt();
            var abyte0 = new byte[chunkSize];
            datainputstream.readFully(abyte0);
            chunk = new byte[(xSize*ySize*zSize*5)/2];
            var inflater = new Inflater();
            inflater.setInput(abyte0);
            try
            {
                inflater.inflate(chunk);
            }
            catch (DataFormatException dataformatexception)
            {
                throw new IOException("Bad compressed data format");
            }
            finally
            {
                inflater.end();
            }
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(xPosition);
            dataoutputstream.writeShort(yPosition);
            dataoutputstream.writeInt(zPosition);
            dataoutputstream.write(xSize - 1);
            dataoutputstream.write(ySize - 1);
            dataoutputstream.write(zSize - 1);
            dataoutputstream.writeInt(chunkSize);
            dataoutputstream.write(chunk, 0, chunkSize);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleMapChunk(this);
        }

        public override int getPacketSize()
        {
            return 17 + chunkSize;
        }
    }
}