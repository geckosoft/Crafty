namespace CraftyServer.Core
{
    internal class PacketCounter
    {
        private PacketCounter()
        {
        }

        public void func_22150_a(int i)
        {
            field_22152_a++;
            field_22151_b += i;
        }

        internal PacketCounter(Empty1 empty1)
            : base()
        {
        }

        private int field_22152_a;
        private long field_22151_b;
    }
}