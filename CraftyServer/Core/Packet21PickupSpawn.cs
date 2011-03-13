using java.io;

namespace CraftyServer.Core
{
    public class Packet21PickupSpawn : Packet
    {
        public Packet21PickupSpawn()
        {
        }

        public Packet21PickupSpawn(EntityItem entityitem)
        {
            entityId = entityitem.entityId;
            itemID = entityitem.item.itemID;
            count = entityitem.item.stackSize;
            itemDamage = entityitem.item.getItemDamage();
            xPosition = MathHelper.floor_double(entityitem.posX*32D);
            yPosition = MathHelper.floor_double(entityitem.posY*32D);
            zPosition = MathHelper.floor_double(entityitem.posZ*32D);
            rotation = (byte) (int) (entityitem.motionX*128D);
            pitch = (byte) (int) (entityitem.motionY*128D);
            roll = (byte) (int) (entityitem.motionZ*128D);
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            entityId = datainputstream.readInt();
            itemID = datainputstream.readShort();
            count = datainputstream.readByte();
            itemDamage = datainputstream.readShort();
            xPosition = datainputstream.readInt();
            yPosition = datainputstream.readInt();
            zPosition = datainputstream.readInt();
            rotation = datainputstream.readByte();
            pitch = datainputstream.readByte();
            roll = datainputstream.readByte();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(entityId);
            dataoutputstream.writeShort(itemID);
            dataoutputstream.writeByte(count);
            dataoutputstream.writeShort(itemDamage);
            dataoutputstream.writeInt(xPosition);
            dataoutputstream.writeInt(yPosition);
            dataoutputstream.writeInt(zPosition);
            dataoutputstream.writeByte(rotation);
            dataoutputstream.writeByte(pitch);
            dataoutputstream.writeByte(roll);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handlePickupSpawn(this);
        }

        public override int getPacketSize()
        {
            return 24;
        }

        public int entityId;
        public int xPosition;
        public int yPosition;
        public int zPosition;
        public byte rotation;
        public byte pitch;
        public byte roll;
        public int itemID;
        public int count;
        public int itemDamage;
    }
}