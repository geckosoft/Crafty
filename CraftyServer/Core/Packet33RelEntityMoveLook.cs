using java.io;

namespace CraftyServer.Core
{
    public class Packet33RelEntityMoveLook : Packet30Entity
    {
        public Packet33RelEntityMoveLook()
        {
            rotating = true;
        }

        public Packet33RelEntityMoveLook(int i, byte byte0, byte byte1, byte byte2, byte byte3, byte byte4)
            : base(i)
        {
            xPosition = byte0;
            yPosition = byte1;
            zPosition = byte2;
            yaw = byte3;
            pitch = byte4;
            rotating = true;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            base.readPacketData(datainputstream);
            xPosition = datainputstream.readByte();
            yPosition = datainputstream.readByte();
            zPosition = datainputstream.readByte();
            yaw = datainputstream.readByte();
            pitch = datainputstream.readByte();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            base.writePacketData(dataoutputstream);
            dataoutputstream.writeByte(xPosition);
            dataoutputstream.writeByte(yPosition);
            dataoutputstream.writeByte(zPosition);
            dataoutputstream.writeByte(yaw);
            dataoutputstream.writeByte(pitch);
        }

        public override int getPacketSize()
        {
            return 9;
        }
    }
}