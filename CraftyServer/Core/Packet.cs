using java.io;
using java.lang;
using java.util;

namespace CraftyServer.Core
{
    public abstract class Packet
    {
        private static readonly Map packetIdToClassMap = new HashMap();
        private static readonly Map packetClassToIdMap = new HashMap();
        private static readonly HashMap field_21904_c = new HashMap();
        private static int field_21903_d;
        public long creationTimeMillis = java.lang.System.currentTimeMillis();
        public bool isChunkDataPacket;

        static Packet()
        {
            addIdClassMapping(0, typeof (Packet0KeepAlive));
            addIdClassMapping(1, typeof (Packet1Login));
            addIdClassMapping(2, typeof (Packet2Handshake));
            addIdClassMapping(3, typeof (Packet3Chat));
            addIdClassMapping(4, typeof (Packet4UpdateTime));
            addIdClassMapping(5, typeof (Packet5PlayerInventory));
            addIdClassMapping(6, typeof (Packet6SpawnPosition));
            addIdClassMapping(7, typeof (Packet7));
            addIdClassMapping(8, typeof (Packet8));
            addIdClassMapping(9, typeof (Packet9));
            addIdClassMapping(10, typeof (Packet10Flying));
            addIdClassMapping(11, typeof (Packet11PlayerPosition));
            addIdClassMapping(12, typeof (Packet12PlayerLook));
            addIdClassMapping(13, typeof (Packet13PlayerLookMove));
            addIdClassMapping(14, typeof (Packet14BlockDig));
            addIdClassMapping(15, typeof (Packet15Place));
            addIdClassMapping(16, typeof (Packet16BlockItemSwitch));
            addIdClassMapping(17, typeof (Packet17Sleep));
            addIdClassMapping(18, typeof (Packet18ArmAnimation));
            addIdClassMapping(19, typeof (Packet19));
            addIdClassMapping(20, typeof (Packet20NamedEntitySpawn));
            addIdClassMapping(21, typeof (Packet21PickupSpawn));
            addIdClassMapping(22, typeof (Packet22Collect));
            addIdClassMapping(23, typeof (Packet23VehicleSpawn));
            addIdClassMapping(24, typeof (Packet24MobSpawn));
            addIdClassMapping(25, typeof (Packet25));
            addIdClassMapping(27, typeof (Packet27));
            addIdClassMapping(28, typeof (Packet28));
            addIdClassMapping(29, typeof (Packet29DestroyEntity));
            addIdClassMapping(30, typeof (Packet30Entity));
            addIdClassMapping(31, typeof (Packet31RelEntityMove));
            addIdClassMapping(32, typeof (Packet32EntityLook));
            addIdClassMapping(33, typeof (Packet33RelEntityMoveLook));
            addIdClassMapping(34, typeof (Packet34EntityTeleport));
            addIdClassMapping(38, typeof (Packet38));
            addIdClassMapping(39, typeof (Packet39));
            addIdClassMapping(40, typeof (Packet40));
            addIdClassMapping(50, typeof (Packet50PreChunk));
            addIdClassMapping(51, typeof (Packet51MapChunk));
            addIdClassMapping(52, typeof (Packet52MultiBlockChange));
            addIdClassMapping(53, typeof (Packet53BlockChange));
            addIdClassMapping(54, typeof (Packet54));
            addIdClassMapping(60, typeof (Packet60));
            addIdClassMapping(100, typeof (Packet100));
            addIdClassMapping(101, typeof (Packet101));
            addIdClassMapping(102, typeof (Packet102));
            addIdClassMapping(103, typeof (Packet103));
            addIdClassMapping(104, typeof (Packet104));
            addIdClassMapping(105, typeof (Packet105));
            addIdClassMapping(106, typeof (Packet106));
            addIdClassMapping(130, typeof (Packet130));
            addIdClassMapping(255, typeof (Packet255KickDisconnect));
        }

        public Packet()
        {
            isChunkDataPacket = false;
        }

        public static void addIdClassMapping(int i, Class class1)
        {
            if (packetIdToClassMap.containsKey(Integer.valueOf(i)))
            {
                throw new IllegalArgumentException(
                    (new StringBuilder()).append("Duplicate packet id:").append(i).toString());
            }
            if (packetClassToIdMap.containsKey(class1))
            {
                throw new IllegalArgumentException(
                    (new StringBuilder()).append("Duplicate packet class:").append(class1).toString());
            }
            else
            {
                packetIdToClassMap.put(Integer.valueOf(i), class1);
                packetClassToIdMap.put(class1, Integer.valueOf(i));
                return;
            }
        }

        public static Packet getNewPacket(int i)
        {
            try
            {
                var class1 = (Class) packetIdToClassMap.get(Integer.valueOf(i));
                if (class1 == null)
                {
                    return null;
                }
                else
                {
                    return (Packet) class1.newInstance();
                }
            }
            catch (Exception exception)
            {
                exception.printStackTrace();
            }
            java.lang.System.@out.println((new StringBuilder()).append("Skipping packet with id ").append(i).toString());
            return null;
        }

        public int getPacketId()
        {
            return ((Integer) packetClassToIdMap.get((Class) GetType())).intValue();
        }

        public static Packet readPacket(DataInputStream datainputstream)
        {
            int i = 0;
            Packet packet = null;
            datainputstream.mark(16384);
            try
            {
                i = datainputstream.read();
                if (i == -1)
                {
                    return null;
                }
                packet = getNewPacket(i);
                if (packet == null)
                {
                    throw new IOException((new StringBuilder()).append("Bad packet id ").append(i).toString());
                }
                packet.readPacketData(datainputstream);
            }
            catch (EOFException eofexception)
            {
                java.lang.System.@out.println("Reached end of stream");
                datainputstream.reset();
                return null;
            }
            var packetcounter = (PacketCounter) field_21904_c.get(Integer.valueOf(i));
            if (packetcounter == null)
            {
                packetcounter = new PacketCounter(null);
                field_21904_c.put(Integer.valueOf(i), packetcounter);
            }
            packetcounter.func_22150_a(packet.getPacketSize());
            field_21903_d++;
            if (field_21903_d%1000 != 0) ;
            return packet;
        }

        public static void writePacket(Packet packet, DataOutputStream dataoutputstream)
        {
            dataoutputstream.write(packet.getPacketId());
            packet.writePacketData(dataoutputstream);
        }

        public abstract void readPacketData(DataInputStream datainputstream);
        public abstract void writePacketData(DataOutputStream dataoutputstream);
        public abstract void processPacket(NetHandler nethandler);
        public abstract int getPacketSize();
    }
}