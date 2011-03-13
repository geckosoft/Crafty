namespace CraftyServer.Core
{
    public interface IPlayerFileData
    {
        void writePlayerData(EntityPlayer entityplayer);
        void readPlayerData(EntityPlayer entityplayer);
    }
}