using java.io;

namespace CraftyServer.Core
{
    public class Packet5PlayerInventory : Packet
    {
        public Packet5PlayerInventory()
        {
        }

        public Packet5PlayerInventory(int i, int j, ItemStack itemstack)
        {
            entityID = i;
            slot = j;
            if (itemstack == null)
            {
                itemID = -1;
                itemDamage = 0;
            }
            else
            {
                itemID = itemstack.itemID;
                itemDamage = itemstack.getItemDamage();
            }
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            entityID = datainputstream.readInt();
            slot = datainputstream.readShort();
            itemID = datainputstream.readShort();
            itemDamage = datainputstream.readShort();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(entityID);
            dataoutputstream.writeShort(slot);
            dataoutputstream.writeShort(itemID);
            dataoutputstream.writeShort(itemDamage);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handlePlayerInventory(this);
        }

        public override int getPacketSize()
        {
            return 8;
        }

        public int entityID;
        public int slot;
        public int itemID;
        public int itemDamage;
    }
}