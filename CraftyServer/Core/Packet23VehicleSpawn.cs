using java.io;

namespace CraftyServer.Core
{
    public class Packet23VehicleSpawn : Packet
    {
        public Packet23VehicleSpawn()
        {
        }

        public Packet23VehicleSpawn(Entity entity, int i)
        {
            entityId = entity.entityId;
            xPosition = MathHelper.floor_double(entity.posX*32D);
            yPosition = MathHelper.floor_double(entity.posY*32D);
            zPosition = MathHelper.floor_double(entity.posZ*32D);
            type = i;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            entityId = datainputstream.readInt();
            type = datainputstream.readByte();
            xPosition = datainputstream.readInt();
            yPosition = datainputstream.readInt();
            zPosition = datainputstream.readInt();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(entityId);
            dataoutputstream.writeByte(type);
            dataoutputstream.writeInt(xPosition);
            dataoutputstream.writeInt(yPosition);
            dataoutputstream.writeInt(zPosition);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleVehicleSpawn(this);
        }

        public override int getPacketSize()
        {
            return 17;
        }

        public int entityId;
        public int xPosition;
        public int yPosition;
        public int zPosition;
        public int type;
    }
}