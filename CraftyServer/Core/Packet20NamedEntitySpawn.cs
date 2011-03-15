using java.io;

namespace CraftyServer.Core
{
    public class Packet20NamedEntitySpawn : Packet
    {
        public int currentItem;
        public int entityId;
        public string name;
        public byte pitch;
        public byte rotation;
        public int xPosition;
        public int yPosition;
        public int zPosition;

        public Packet20NamedEntitySpawn()
        {
        }

        public Packet20NamedEntitySpawn(EntityPlayer entityplayer)
        {
            entityId = entityplayer.entityId;
            name = entityplayer.username;
            xPosition = MathHelper.floor_double(entityplayer.posX*32D);
            yPosition = MathHelper.floor_double(entityplayer.posY*32D);
            zPosition = MathHelper.floor_double(entityplayer.posZ*32D);
            rotation = (byte) (int) ((entityplayer.rotationYaw*256F)/360F);
            pitch = (byte) (int) ((entityplayer.rotationPitch*256F)/360F);
            ItemStack itemstack = entityplayer.inventory.getCurrentItem();
            currentItem = itemstack != null ? itemstack.itemID : 0;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            entityId = datainputstream.readInt();
            name = datainputstream.readUTF();
            xPosition = datainputstream.readInt();
            yPosition = datainputstream.readInt();
            zPosition = datainputstream.readInt();
            rotation = datainputstream.readByte();
            pitch = datainputstream.readByte();
            currentItem = datainputstream.readShort();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(entityId);
            dataoutputstream.writeUTF(name);
            dataoutputstream.writeInt(xPosition);
            dataoutputstream.writeInt(yPosition);
            dataoutputstream.writeInt(zPosition);
            dataoutputstream.writeByte(rotation);
            dataoutputstream.writeByte(pitch);
            dataoutputstream.writeShort(currentItem);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleNamedEntitySpawn(this);
        }

        public override int getPacketSize()
        {
            return 28;
        }
    }
}