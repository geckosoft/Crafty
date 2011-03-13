using java.io;
using java.util;

namespace CraftyServer.Core
{
    public class Packet40 : Packet
    {
        public Packet40()
        {
        }

        public Packet40(int i, DataWatcher datawatcher)
        {
            entityId = i;
            field_21018_b = datawatcher.getChangedObjects();
        }

        public override void readPacketData(DataInputStream datainputstream)
        {
            entityId = datainputstream.readInt();
            field_21018_b = DataWatcher.readWatchableObjects(datainputstream);
        }

        public override void writePacketData(DataOutputStream dataoutputstream)
        {
            dataoutputstream.writeInt(entityId);
            DataWatcher.writeObjectsInListToStream(field_21018_b, dataoutputstream);
        }

        public override void processPacket(NetHandler nethandler)
        {
            nethandler.func_21002_a(this);
        }

        public override int getPacketSize()
        {
            return 5;
        }

        public int entityId;
        private List field_21018_b;
    }
}