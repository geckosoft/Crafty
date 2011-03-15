using java.awt.@event;
using javax.swing;

namespace CraftyServer.Core
{
    public class ServerGuiCommandListener
        : ActionListener
    {
        private readonly ServerGUI mcServerGui; /* synthetic field */
        private readonly JTextField textField; /* synthetic field */

        public ServerGuiCommandListener(ServerGUI servergui, JTextField jtextfield)
        {
            mcServerGui = servergui;
            textField = jtextfield;
//        
        }

        #region ActionListener Members

        public void actionPerformed(ActionEvent actionevent)
        {
            string s = textField.getText().Trim();
            if (s.Length > 0)
            {
                ServerGUI.getMinecraftServer(mcServerGui).addCommand(s, mcServerGui);
            }
            textField.setText("");
        }

        #endregion
    }
}