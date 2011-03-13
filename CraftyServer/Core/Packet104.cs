using java.io;
using java.util;

namespace CraftyServer.Core
{
    public class Packet104 : Packet
    {
        public Packet104()
        {
        }

        public Packet104(int i, List list)
        {
            windowId = i;
            itemStack = new ItemStack[list.size()];
            for (int j = 0; j < itemStack.Length; j++)
            {
                ItemStack itemstack = (ItemStack) list.get(j);
                itemStack[j] = itemstack != null ? itemstack.copy() : null;
            }
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            windowId = datainputstream.readByte();
            short word0 = datainputstream.readShort();
            itemStack = new ItemStack[word0];
            for (int i = 0; i < word0; i++)
            {
                short word1 = datainputstream.readShort();
                if (word1 >= 0)
                {
                    byte byte0 = datainputstream.readByte();
                    short word2 = datainputstream.readShort();
                    itemStack[i] = new ItemStack(word1, byte0, word2);
                }
            }
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeByte(windowId);
            dataoutputstream.writeShort(itemStack.Length);
            for (int i = 0; i < itemStack.Length; i++)
            {
                if (itemStack[i] == null)
                {
                    dataoutputstream.writeShort(-1);
                }
                else
                {
                    dataoutputstream.writeShort((short) itemStack[i].itemID);
                    dataoutputstream.writeByte((byte) itemStack[i].stackSize);
                    dataoutputstream.writeShort((short) itemStack[i].getItemDamage());
                }
            }
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.func_20001_a(this);
        }

        public override int getPacketSize()
        {
            return 3 + itemStack.Length*5;
        }

        public int windowId;
        public ItemStack[] itemStack;
    }
}