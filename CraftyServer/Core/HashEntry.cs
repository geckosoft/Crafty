using java.lang;

namespace CraftyServer.Core
{
    public class HashEntry
    {
        public int hashEntry;
        public HashEntry nextEntry;
        public int slotHash;
        public object valueEntry;

        public HashEntry(int i, int j, object obj, HashEntry hashentry)
        {
            valueEntry = obj;
            nextEntry = hashentry;
            hashEntry = j;
            slotHash = i;
        }

        public int getHash()
        {
            return hashEntry;
        }

        public object getValue()
        {
            return valueEntry;
        }

        public bool equals(object obj)
        {
            if (!(obj is HashEntry))
            {
                return false;
            }
            var hashentry = (HashEntry) obj;
            Integer integer = Integer.valueOf(getHash());
            Integer integer1 = Integer.valueOf(hashentry.getHash());
            if (integer == integer1 || integer != null && integer.Equals(integer1))
            {
                object obj1 = getValue();
                object obj2 = hashentry.getValue();
                if (obj1 == obj2 || obj1 != null && obj1.Equals(obj2))
                {
                    return true;
                }
            }
            return false;
        }

        public int hashCode()
        {
            return MCHashTable.getHash(hashEntry);
        }

        public string toString()
        {
            return (new StringBuilder()).append(getHash()).append("=").append(getValue()).toString();
        }
    }
}