using java.io;

namespace CraftyServer.Core
{
    public class Packet28 : Packet
    {
        public Packet28()
        {
        }

        public Packet28(Entity entity)
            : this(entity.entityId, entity.motionX, entity.motionY, entity.motionZ)
        {
        }

        public Packet28(int i, double d, double d1, double d2)
        {
            entityId = i;
            double d3 = 3.8999999999999999D;
            if (d < -d3)
            {
                d = -d3;
            }
            if (d1 < -d3)
            {
                d1 = -d3;
            }
            if (d2 < -d3)
            {
                d2 = -d3;
            }
            if (d > d3)
            {
                d = d3;
            }
            if (d1 > d3)
            {
                d1 = d3;
            }
            if (d2 > d3)
            {
                d2 = d3;
            }
            motionX = (int) (d*8000D);
            motionY = (int) (d1*8000D);
            motionZ = (int) (d2*8000D);
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            entityId = datainputstream.readInt();
            motionX = datainputstream.readShort();
            motionY = datainputstream.readShort();
            motionZ = datainputstream.readShort();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(entityId);
            dataoutputstream.writeShort(motionX);
            dataoutputstream.writeShort(motionY);
            dataoutputstream.writeShort(motionZ);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.func_6002_a(this);
        }

        public override int getPacketSize()
        {
            return 10;
        }

        public int entityId;
        public int motionX;
        public int motionY;
        public int motionZ;
    }
}