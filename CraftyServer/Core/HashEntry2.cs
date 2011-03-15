using java.lang;

namespace CraftyServer.Core
{
    public class HashEntry2
    {
        public object field_1024_b;
        public long field_1025_a;
        public int field_1026_d;
        public HashEntry2 field_1027_c;

        public HashEntry2(int i, long l, object obj, HashEntry2 hashentry2)
        {
            field_1024_b = obj;
            field_1027_c = hashentry2;
            field_1025_a = l;
            field_1026_d = i;
        }

        public long func_736_a()
        {
            return field_1025_a;
        }

        public object func_735_b()
        {
            return field_1024_b;
        }

        public bool equals(object obj)
        {
            if (!(obj is HashEntry2))
            {
                return false;
            }
            var hashentry2 = (HashEntry2) obj;
            Long long1 = Long.valueOf(func_736_a());
            Long long2 = Long.valueOf(hashentry2.func_736_a());
            if (long1 == long2 || long1 != null && long1.Equals(long2))
            {
                object obj1 = func_735_b();
                object obj2 = hashentry2.func_735_b();
                if (obj1 == obj2 || obj1 != null && obj1.Equals(obj2))
                {
                    return true;
                }
            }
            return false;
        }

        public int hashCode()
        {
            return MCHashTable2.func_674_d(field_1025_a);
        }

        public string toString()
        {
            return (new StringBuilder()).append(func_736_a()).append("=").append(func_735_b()).toString();
        }
    }
}