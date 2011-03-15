using java.io;

namespace CraftyServer.Core
{
    public class Packet102 : Packet
    {
        public short action;
        public int inventorySlot;
        public ItemStack itemStack;
        public int mouseClick;
        public int window_Id;

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.func_20007_a(this);
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            window_Id = datainputstream.readByte();
            inventorySlot = datainputstream.readShort();
            mouseClick = datainputstream.readByte();
            action = datainputstream.readShort();
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
            dataoutputstream.writeByte(window_Id);
            dataoutputstream.writeShort(inventorySlot);
            dataoutputstream.writeByte(mouseClick);
            dataoutputstream.writeShort(action);
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

        public override int getPacketSize()
        {
            return 11;
        }
    }
}