using java.io;

namespace CraftyServer.Core
{
    public class Packet103 : Packet
    {
        public Packet103()
        {
        }

        public Packet103(int i, int j, ItemStack itemstack)
        {
            windowId = i;
            itemSlot = j;
            myItemStack = itemstack != null ? itemstack.copy() : itemstack;
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.func_20003_a(this);
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            windowId = datainputstream.readByte();
            itemSlot = datainputstream.readShort();
            short word0 = datainputstream.readShort();
            if (word0 >= 0)
            {
                byte byte0 = datainputstream.readByte();
                short word1 = datainputstream.readShort();
                myItemStack = new ItemStack(word0, byte0, word1);
            }
            else
            {
                myItemStack = null;
            }
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeByte(windowId);
            dataoutputstream.writeShort(itemSlot);
            if (myItemStack == null)
            {
                dataoutputstream.writeShort(-1);
            }
            else
            {
                dataoutputstream.writeShort(myItemStack.itemID);
                dataoutputstream.writeByte(myItemStack.stackSize);
                dataoutputstream.writeShort(myItemStack.getItemDamage());
            }
        }

        public override int getPacketSize()
        {
            return 8;
        }

        public int windowId;
        public int itemSlot;
        public ItemStack myItemStack;
    }
}