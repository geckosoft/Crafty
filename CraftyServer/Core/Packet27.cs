using java.io;

namespace CraftyServer.Core
{
    public class Packet27 : Packet
    {
        private float field_22034_b;
        private float field_22035_a;
        private float field_22036_f;
        private float field_22037_e;
        private bool field_22038_d;
        private bool field_22039_c;

        public override void readPacketData(DataInputStream datainputstream)
        {
            field_22035_a = datainputstream.readFloat();
            field_22034_b = datainputstream.readFloat();
            field_22037_e = datainputstream.readFloat();
            field_22036_f = datainputstream.readFloat();
            field_22039_c = datainputstream.readBoolean();
            field_22038_d = datainputstream.readBoolean();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeFloat(field_22035_a);
            dataoutputstream.writeFloat(field_22034_b);
            dataoutputstream.writeFloat(field_22037_e);
            dataoutputstream.writeFloat(field_22036_f);
            dataoutputstream.writeBoolean(field_22039_c);
            dataoutputstream.writeBoolean(field_22038_d);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.handleMovementTypePacket(this);
        }

        public override int getPacketSize()
        {
            return 18;
        }

        public float func_22031_c()
        {
            return field_22035_a;
        }

        public float func_22029_d()
        {
            return field_22037_e;
        }

        public float func_22028_e()
        {
            return field_22034_b;
        }

        public float func_22033_f()
        {
            return field_22036_f;
        }

        public bool func_22032_g()
        {
            return field_22039_c;
        }

        public bool func_22030_h()
        {
            return field_22038_d;
        }
    }
}