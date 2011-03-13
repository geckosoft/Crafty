using java.io;
using java.util;

namespace CraftyServer.Core
{
    public class Packet24MobSpawn : Packet
    {
        public Packet24MobSpawn()
        {
        }

        public Packet24MobSpawn(EntityLiving entityliving)
        {
            entityId = entityliving.entityId;
            type = (byte) EntityList.getEntityID(entityliving);
            xPosition = MathHelper.floor_double(entityliving.posX*32D);
            yPosition = MathHelper.floor_double(entityliving.posY*32D);
            zPosition = MathHelper.floor_double(entityliving.posZ*32D);
            yaw = (byte) (int) ((entityliving.rotationYaw*256F)/360F);
            pitch = (byte) (int) ((entityliving.rotationPitch*256F)/360F);
            metaData = entityliving.getDataWatcher();
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            entityId = datainputstream.readInt();
            type = datainputstream.readByte();
            xPosition = datainputstream.readInt();
            yPosition = datainputstream.readInt();
            zPosition = datainputstream.readInt();
            yaw = datainputstream.readByte();
            pitch = datainputstream.readByte();
            receivedMetadata = DataWatcher.readWatchableObjects(datainputstream);
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(entityId);
            dataoutputstream.writeByte(type);
            dataoutputstream.writeInt(xPosition);
            dataoutputstream.writeInt(yPosition);
            dataoutputstream.writeInt(zPosition);
            dataoutputstream.writeByte(yaw);
            dataoutputstream.writeByte(pitch);
            metaData.writeWatchableObjects(dataoutputstream);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleMobSpawn(this);
        }

        public override int getPacketSize()
        {
            return 20;
        }

        public int entityId;
        public byte type;
        public int xPosition;
        public int yPosition;
        public int zPosition;
        public byte yaw;
        public byte pitch;
        private DataWatcher metaData;
        private List receivedMetadata;
    }
}