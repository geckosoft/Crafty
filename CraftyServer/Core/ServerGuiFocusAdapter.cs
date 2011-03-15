using java.awt.@event;

namespace CraftyServer.Core
{
    public class ServerGuiFocusAdapter : FocusAdapter
    {
        private ServerGUI mcServerGui; /* synthetic field */

        public ServerGuiFocusAdapter(ServerGUI servergui)
        {
            mcServerGui = servergui;
//        
        }

        public override void focusGained(FocusEvent focusevent)
        {
        }
    }
}