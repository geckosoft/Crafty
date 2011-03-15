namespace CraftyServer.Core
{
    public class NetHandler
    {
        public virtual void handleMapChunk(Packet51MapChunk packet51mapchunk)
        {
        }

        public virtual void registerPacket(Packet packet)
        {
        }

        public virtual void handleErrorMessage(string s, object[] aobj)
        {
        }

        public virtual void handleKickDisconnect(Packet255KickDisconnect packet255kickdisconnect)
        {
            registerPacket(packet255kickdisconnect);
        }

        public virtual void handleLogin(Packet1Login packet1login)
        {
            registerPacket(packet1login);
        }

        public virtual void handleFlying(Packet10Flying packet10flying)
        {
            registerPacket(packet10flying);
        }

        public virtual void handleMultiBlockChange(Packet52MultiBlockChange packet52multiblockchange)
        {
            registerPacket(packet52multiblockchange);
        }

        public virtual void handleBlockDig(Packet14BlockDig packet14blockdig)
        {
            registerPacket(packet14blockdig);
        }

        public virtual void handleBlockChange(Packet53BlockChange packet53blockchange)
        {
            registerPacket(packet53blockchange);
        }

        public virtual void handlePreChunk(Packet50PreChunk packet50prechunk)
        {
            registerPacket(packet50prechunk);
        }

        public virtual void handleNamedEntitySpawn(Packet20NamedEntitySpawn packet20namedentityspawn)
        {
            registerPacket(packet20namedentityspawn);
        }

        public virtual void handleEntity(Packet30Entity packet30entity)
        {
            registerPacket(packet30entity);
        }

        public virtual void handleEntityTeleport(Packet34EntityTeleport packet34entityteleport)
        {
            registerPacket(packet34entityteleport);
        }

        public virtual void handlePlace(Packet15Place packet15place)
        {
            registerPacket(packet15place);
        }

        public virtual void handleBlockItemSwitch(Packet16BlockItemSwitch packet16blockitemswitch)
        {
            registerPacket(packet16blockitemswitch);
        }

        public virtual void handleDestroyEntity(Packet29DestroyEntity packet29destroyentity)
        {
            registerPacket(packet29destroyentity);
        }

        public virtual void handlePickupSpawn(Packet21PickupSpawn packet21pickupspawn)
        {
            registerPacket(packet21pickupspawn);
        }

        public virtual void handleCollect(Packet22Collect packet22collect)
        {
            registerPacket(packet22collect);
        }

        public virtual void handleChat(Packet3Chat packet3chat)
        {
            registerPacket(packet3chat);
        }

        public virtual void handleVehicleSpawn(Packet23VehicleSpawn packet23vehiclespawn)
        {
            registerPacket(packet23vehiclespawn);
        }

        public virtual void handleArmAnimation(Packet18ArmAnimation packet18armanimation)
        {
            registerPacket(packet18armanimation);
        }

        public virtual void func_21001_a(Packet19 packet19)
        {
            registerPacket(packet19);
        }

        public virtual void handleHandshake(Packet2Handshake packet2handshake)
        {
            registerPacket(packet2handshake);
        }

        public virtual void handleMobSpawn(Packet24MobSpawn packet24mobspawn)
        {
            registerPacket(packet24mobspawn);
        }

        public virtual void handleUpdateTime(Packet4UpdateTime packet4updatetime)
        {
            registerPacket(packet4updatetime);
        }

        public virtual void handleSpawnPosition(Packet6SpawnPosition packet6spawnposition)
        {
            registerPacket(packet6spawnposition);
        }

        public virtual void func_6002_a(Packet28 packet28)
        {
            registerPacket(packet28);
        }

        public virtual void func_21002_a(Packet40 packet40)
        {
            registerPacket(packet40);
        }

        public virtual void func_6003_a(Packet39 packet39)
        {
            registerPacket(packet39);
        }

        public virtual void func_6006_a(Packet7 packet7)
        {
            registerPacket(packet7);
        }

        public virtual void func_9001_a(Packet38 packet38)
        {
            registerPacket(packet38);
        }

        public virtual void handleHealth(Packet8 packet8)
        {
            registerPacket(packet8);
        }

        public virtual void handleRespawnPacket(Packet9 packet9)
        {
            registerPacket(packet9);
        }

        public virtual void func_12001_a(Packet60 packet60)
        {
            registerPacket(packet60);
        }

        public virtual void func_20004_a(Packet100 packet100)
        {
            registerPacket(packet100);
        }

        public virtual void handleCraftingGuiClosedPacked(Packet101 packet101)
        {
            registerPacket(packet101);
        }

        public virtual void func_20007_a(Packet102 packet102)
        {
            registerPacket(packet102);
        }

        public virtual void func_20003_a(Packet103 packet103)
        {
            registerPacket(packet103);
        }

        public virtual void func_20001_a(Packet104 packet104)
        {
            registerPacket(packet104);
        }

        public virtual void func_20005_a(Packet130 packet130)
        {
            registerPacket(packet130);
        }

        public virtual void func_20002_a(Packet105 packet105)
        {
            registerPacket(packet105);
        }

        public virtual void handlePlayerInventory(Packet5PlayerInventory packet5playerinventory)
        {
            registerPacket(packet5playerinventory);
        }

        public virtual void func_20008_a(Packet106 packet106)
        {
            registerPacket(packet106);
        }

        public virtual void func_21003_a(Packet25 packet25)
        {
            registerPacket(packet25);
        }

        public virtual void func_21004_a(Packet54 packet54)
        {
            registerPacket(packet54);
        }

        public virtual void func_22002_a(Packet17Sleep packet17sleep)
        {
        }

        public virtual void handleMovementTypePacket(Packet27 packet27)
        {
        }
    }
}