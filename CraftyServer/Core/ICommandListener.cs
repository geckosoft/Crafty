namespace CraftyServer.Core
{
    public interface ICommandListener
    {
        void log(string s);
        string getUsername();
    }
}