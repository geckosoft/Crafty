namespace CraftyServer.Core
{
    public class MCHashTable
    {
        public MCHashTable()
        {
            threshold = 12;
            slots = new HashEntry[16];
        }

        private static int computeHash(int i)
        {
            i ^= SupportClass.URShift(i, 20) ^ SupportClass.URShift(i, 12);
            return i ^ SupportClass.URShift(i, 7) ^ SupportClass.URShift(i, 4);
        }

        private static int getSlotIndex(int i, int j)
        {
            return i & j - 1;
        }

        public object lookup(int i)
        {
            int j = computeHash(i);
            for (HashEntry hashentry = slots[getSlotIndex(j, slots.Length)];
                 hashentry != null;
                 hashentry = hashentry.nextEntry)
            {
                if (hashentry.hashEntry == i)
                {
                    return hashentry.valueEntry;
                }
            }

            return null;
        }

        public bool containsItem(int i)
        {
            return lookupEntry(i) != null;
        }

        private HashEntry lookupEntry(int i)
        {
            int j = computeHash(i);
            for (HashEntry hashentry = slots[getSlotIndex(j, slots.Length)];
                 hashentry != null;
                 hashentry = hashentry.nextEntry)
            {
                if (hashentry.hashEntry == i)
                {
                    return hashentry;
                }
            }

            return null;
        }

        public void addKey(int i, object obj)
        {
            int j = computeHash(i);
            int k = getSlotIndex(j, slots.Length);
            for (HashEntry hashentry = slots[k]; hashentry != null; hashentry = hashentry.nextEntry)
            {
                if (hashentry.hashEntry == i)
                {
                    hashentry.valueEntry = obj;
                }
            }

            versionStamp++;
            insert(j, i, obj, k);
        }

        private void grow(int i)
        {
            HashEntry[] ahashentry = slots;
            int j = ahashentry.Length;
            if (j == 0x40000000)
            {
                threshold = 0x7fffffff;
                return;
            }
            else
            {
                HashEntry[] ahashentry1 = new HashEntry[i];
                copyTo(ahashentry1);
                slots = ahashentry1;
                threshold = (int) ((float) i*growFactor);
                return;
            }
        }

        private void copyTo(HashEntry[] ahashentry)
        {
            HashEntry[] ahashentry1 = slots;
            int i = ahashentry.Length;
            for (int j = 0; j < ahashentry1.Length; j++)
            {
                HashEntry hashentry = ahashentry1[j];
                if (hashentry == null)
                {
                    continue;
                }
                ahashentry1[j] = null;
                do
                {
                    HashEntry hashentry1 = hashentry.nextEntry;
                    int k = getSlotIndex(hashentry.slotHash, i);
                    hashentry.nextEntry = ahashentry[k];
                    ahashentry[k] = hashentry;
                    hashentry = hashentry1;
                } while (hashentry != null);
            }
        }

        public object removeObject(int i)
        {
            HashEntry hashentry = removeEntry(i);
            return hashentry != null ? hashentry.valueEntry : null;
        }

        private HashEntry removeEntry(int i)
        {
            int j = computeHash(i);
            int k = getSlotIndex(j, slots.Length);
            HashEntry hashentry = slots[k];
            HashEntry hashentry1;
            HashEntry hashentry2;
            for (hashentry1 = hashentry; hashentry1 != null; hashentry1 = hashentry2)
            {
                hashentry2 = hashentry1.nextEntry;
                if (hashentry1.hashEntry == i)
                {
                    versionStamp++;
                    count--;
                    if (hashentry == hashentry1)
                    {
                        slots[k] = hashentry2;
                    }
                    else
                    {
                        hashentry.nextEntry = hashentry2;
                    }
                    return hashentry1;
                }
                hashentry = hashentry1;
            }

            return hashentry1;
        }

        public void clearMap()
        {
            versionStamp++;
            HashEntry[] ahashentry = slots;
            for (int i = 0; i < ahashentry.Length; i++)
            {
                ahashentry[i] = null;
            }

            count = 0;
        }

        private void insert(int i, int j, object obj, int k)
        {
            HashEntry hashentry = slots[k];
            slots[k] = new HashEntry(i, j, obj, hashentry);
            if (count++ >= threshold)
            {
                grow(2*slots.Length);
            }
        }

        public static int getHash(int i)
        {
            return computeHash(i);
        }

        [System.NonSerialized] private HashEntry[] slots;
        [System.NonSerialized] private int count;
        private int threshold;
        private float growFactor = 0.75F;
        [System.NonSerialized] private volatile int versionStamp;
    }
}