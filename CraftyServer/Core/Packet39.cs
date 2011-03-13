using java.io;

namespace CraftyServer.Core
{
    public class Packet39 : Packet
    {
        public Packet39()
        {
        }

        public Packet39(Entity entity, Entity entity1)
        {
            entityId = entity.entityId;
            vehicleEntityId = entity1 == null ? -1 : entity1.entityId;
        }

        public override int getPacketSize()
        {
            return 8;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            entityId = datainputstream.readInt();
            vehicleEntityId = datainputstream.readInt();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(entityId);
            dataoutputstream.writeInt(vehicleEntityId);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.func_6003_a(this);
        }

        public int entityId;
        public int vehicleEntityId;
    }
}