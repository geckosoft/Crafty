namespace CraftyServer.Core
{
    public class ServerCommand
    {
        public string command;
        public ICommandListener commandListener;

        public ServerCommand(string s, ICommandListener icommandlistener)
        {
            command = s;
            commandListener = icommandlistener;
        }
    }
}