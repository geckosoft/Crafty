using java.io;

namespace CraftyServer.Core
{
    public class Packet15Place : Packet
    {
        public int direction;
        public ItemStack itemStack;
        public int xPosition;
        public int yPosition;
        public int zPosition;

        public override void readPacketData(DataInputStream datainputstream)
        {
            xPosition = datainputstream.readInt();
            yPosition = datainputstream.read();
            zPosition = datainputstream.readInt();
            direction = datainputstream.read();
            short word0 = datainputstream.readShort();
            if (word0 >= 0)
            {
                byte byte0 = datainputstream.readByte();
                short word1 = datainputstream.readShort();
                itemStack = new ItemStack(word0, byte0, word1);
            }
            else
            {
                itemStack = null;
            }
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(xPosition);
            dataoutputstream.write(yPosition);
            dataoutputstream.writeInt(zPosition);
            dataoutputstream.write(direction);
            if (itemStack == null)
            {
                dataoutputstream.writeShort(-1);
            }
            else
            {
                dataoutputstream.writeShort(itemStack.itemID);
                dataoutputstream.writeByte(itemStack.stackSize);
                dataoutputstream.writeShort(itemStack.getItemDamage());
            }
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handlePlace(this);
        }

        public override int getPacketSize()
        {
            return 15;
        }
    }
}