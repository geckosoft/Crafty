using java.awt.@event;
using javax.swing;

namespace CraftyServer.Core
{
    public class ServerGuiCommandListener
        : ActionListener
    {
        public ServerGuiCommandListener(ServerGUI servergui, JTextField jtextfield)
        {
            mcServerGui = servergui;
            textField = jtextfield;
//        
        }

        public void actionPerformed(ActionEvent actionevent)
        {
            string s = textField.getText().Trim();
            if (s.Length > 0)
            {
                ServerGUI.getMinecraftServer(mcServerGui).addCommand(s, mcServerGui);
            }
            textField.setText("");
        }

        private JTextField textField; /* synthetic field */
        private ServerGUI mcServerGui; /* synthetic field */
    }
}