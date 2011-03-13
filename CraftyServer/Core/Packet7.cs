using java.io;

namespace CraftyServer.Core
{
    public class Packet7 : Packet
    {
        public Packet7()
        {
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            playerEntityId = datainputstream.readInt();
            targetEntity = datainputstream.readInt();
            isLeftClick = datainputstream.readByte();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(playerEntityId);
            dataoutputstream.writeInt(targetEntity);
            dataoutputstream.writeByte(isLeftClick);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.func_6006_a(this);
        }

        public override int getPacketSize()
        {
            return 9;
        }

        public int playerEntityId;
        public int targetEntity;
        public int isLeftClick;
    }
}