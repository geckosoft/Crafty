namespace CraftyServer.Core
{
    public class WatchableObject
    {
        public WatchableObject(int i, int j, object obj)
        {
            dataValueId = j;
            watchedObject = obj;
            objectType = i;
            isWatching = true;
        }

        public int getDataValueId()
        {
            return dataValueId;
        }

        public void setObject(object obj)
        {
            watchedObject = obj;
        }

        public object getObject()
        {
            return watchedObject;
        }

        public int getObjectType()
        {
            return objectType;
        }

        public bool getWatching()
        {
            return isWatching;
        }

        public void setWatching(bool flag)
        {
            isWatching = flag;
        }

        private int objectType;
        private int dataValueId;
        private object watchedObject;
        private bool isWatching;
    }
}