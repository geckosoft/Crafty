using java.io;

namespace CraftyServer.Core
{
    public class Packet18ArmAnimation : Packet
    {
        public Packet18ArmAnimation()
        {
        }

        public Packet18ArmAnimation(Entity entity, int i)
        {
            entityId = entity.entityId;
            animate = i;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            entityId = datainputstream.readInt();
            animate = datainputstream.readByte();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(entityId);
            dataoutputstream.writeByte(animate);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleArmAnimation(this);
        }

        public override int getPacketSize()
        {
            return 5;
        }

        public int entityId;
        public int animate;
    }
}