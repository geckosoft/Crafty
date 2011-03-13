using java.io;

namespace CraftyServer.Core
{
    public class Packet17Sleep : Packet
    {
        public Packet17Sleep()
        {
        }

        public Packet17Sleep(Entity entity, int i, int j, int k, int l)
        {
            field_22042_e = i;
            field_22040_b = j;
            field_22044_c = k;
            field_22043_d = l;
            field_22041_a = entity.entityId;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            field_22041_a = datainputstream.readInt();
            field_22042_e = datainputstream.readByte();
            field_22040_b = datainputstream.readInt();
            field_22044_c = datainputstream.readByte();
            field_22043_d = datainputstream.readInt();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(field_22041_a);
            dataoutputstream.writeByte(field_22042_e);
            dataoutputstream.writeInt(field_22040_b);
            dataoutputstream.writeByte(field_22044_c);
            dataoutputstream.writeInt(field_22043_d);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.func_22002_a(this);
        }

        public override int getPacketSize()
        {
            return 14;
        }

        public int field_22041_a;
        public int field_22040_b;
        public int field_22044_c;
        public int field_22043_d;
        public int field_22042_e;
    }
}