namespace CraftyServer.Core
{
    public class ServerCommand
    {
        public ServerCommand(string s, ICommandListener icommandlistener)
        {
            command = s;
            commandListener = icommandlistener;
        }

        public string command;
        public ICommandListener commandListener;
    }
}