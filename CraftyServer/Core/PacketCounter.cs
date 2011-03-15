namespace CraftyServer.Core
{
    internal class PacketCounter
    {
        private long field_22151_b;
        private int field_22152_a;

        private PacketCounter()
        {
        }

        internal PacketCounter(Empty1 empty1)
        {
        }

        public void func_22150_a(int i)
        {
            field_22152_a++;
            field_22151_b += i;
        }
    }
}