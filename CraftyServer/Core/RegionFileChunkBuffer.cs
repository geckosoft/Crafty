using java.io;

namespace CraftyServer.Core
{
    internal class RegionFileChunkBuffer : ByteArrayOutputStream
    {
        private readonly int field_22156_b;
        private readonly RegionFile field_22157_a; /* synthetic field */
        private readonly int field_22158_c;

        public RegionFileChunkBuffer(RegionFile regionfile, int i, int j)
            : base(8096)
        {
            field_22157_a = regionfile;
            field_22156_b = i;
            field_22158_c = j;
        }

        public override void close()
        {
            field_22157_a.write(field_22156_b, field_22158_c, buf, count);
        }
    }
}