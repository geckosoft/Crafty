using java.io;

namespace CraftyServer.Core
{
    public class Packet25 : Packet
    {
        public Packet25()
        {
        }

        public Packet25(EntityPainting entitypainting)
        {
            entityId = entitypainting.entityId;
            xPosition = entitypainting.xPosition;
            yPosition = entitypainting.yPosition;
            zPosition = entitypainting.zPosition;
            direction = entitypainting.direction;
            title = entitypainting.art.title;
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            entityId = datainputstream.readInt();
            title = datainputstream.readUTF();
            xPosition = datainputstream.readInt();
            yPosition = datainputstream.readInt();
            zPosition = datainputstream.readInt();
            direction = datainputstream.readInt();
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(entityId);
            dataoutputstream.writeUTF(title);
            dataoutputstream.writeInt(xPosition);
            dataoutputstream.writeInt(yPosition);
            dataoutputstream.writeInt(zPosition);
            dataoutputstream.writeInt(direction);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.func_21003_a(this);
        }

        public override int getPacketSize()
        {
            return 24;
        }

        public int entityId;
        public int xPosition;
        public int yPosition;
        public int zPosition;
        public int direction;
        public string title;
    }
}