using java.lang;

namespace CraftyServer.Core
{
    public class NextTickListEntry
        : Object, Comparable
    {
        private static long nextTickEntryID;
        private readonly long tickEntryID;
        public int blockID;
        public long scheduledTime;
        public int xCoord;
        public int yCoord;
        public int zCoord;

        public NextTickListEntry(int i, int j, int k, int l)
        {
            tickEntryID = nextTickEntryID++;
            xCoord = i;
            yCoord = j;
            zCoord = k;
            blockID = l;
        }

        #region Comparable Members

        public int CompareTo(object obj)
        {
            return comparer((NextTickListEntry) obj);
        }

        #endregion

        public override bool equals(object obj)
        {
            if (obj is NextTickListEntry)
            {
                var nextticklistentry = (NextTickListEntry) obj;
                return xCoord == nextticklistentry.xCoord && yCoord == nextticklistentry.yCoord &&
                       zCoord == nextticklistentry.zCoord && blockID == nextticklistentry.blockID;
            }
            else
            {
                return false;
            }
        }

        public override int hashCode()
        {
            return (xCoord*128*1024 + zCoord*128 + yCoord)*256 + blockID;
        }

        public NextTickListEntry setScheduledTime(long l)
        {
            scheduledTime = l;
            return this;
        }

        public int comparer(NextTickListEntry nextticklistentry)
        {
            if (scheduledTime < nextticklistentry.scheduledTime)
            {
                return -1;
            }
            if (scheduledTime > nextticklistentry.scheduledTime)
            {
                return 1;
            }
            if (tickEntryID < nextticklistentry.tickEntryID)
            {
                return -1;
            }
            return tickEntryID <= nextticklistentry.tickEntryID ? 0 : 1;
        }

        public int compareTo(object obj)
        {
            return comparer((NextTickListEntry) obj);
        }
    }
}