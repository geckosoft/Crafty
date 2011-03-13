using java.awt.@event;

namespace CraftyServer.Core
{
    public class ServerGuiFocusAdapter : FocusAdapter
    {
        public ServerGuiFocusAdapter(ServerGUI servergui)
        {
            mcServerGui = servergui;
//        
        }

        public override void focusGained(FocusEvent focusevent)
        {
        }

        private ServerGUI mcServerGui; /* synthetic field */
    }
}