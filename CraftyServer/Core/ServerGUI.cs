using CraftyServer.Server;
using java.awt;
using java.lang;
using java.util.logging;
using javax.swing;
using javax.swing.border;

namespace CraftyServer.Core
{
    public class ServerGUI : JComponent
                             , ICommandListener
    {
        public static Logger logger = Logger.getLogger("Minecraft");
        private readonly MinecraftServer mcServer;

        public ServerGUI(MinecraftServer minecraftserver)
        {
            mcServer = minecraftserver;
            setPreferredSize(new Dimension(854, 480));
            setLayout(new BorderLayout());
            try
            {
                add(getLogComponent(), "Center");
                add(getStatsComponent(), "West");
            }
            catch (Exception exception)
            {
                exception.printStackTrace();
            }
        }

        #region ICommandListener Members

        public void log(string s)
        {
            logger.info(s);
        }

        public string getUsername()
        {
            return "CONSOLE";
        }

        #endregion

        public static void initGui(MinecraftServer minecraftserver)
        {
            try
            {
                UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
            }
            catch (Exception)
            {
            }
            var servergui = new ServerGUI(minecraftserver);
            var jframe = new JFrame("Minecraft server");
            jframe.add(servergui);
            jframe.pack();
            jframe.setLocationRelativeTo(null);
            jframe.setVisible(true);
            jframe.addWindowListener(new ServerWindowAdapter(minecraftserver));
        }

        private JComponent getStatsComponent()
        {
            var jpanel = new JPanel(new BorderLayout());
            jpanel.add(new GuiStatsComponent(), "North");
            jpanel.add(getPlayerListComponent(), "Center");
            jpanel.setBorder(new TitledBorder(new EtchedBorder(), "Stats"));
            return jpanel;
        }

        private JComponent getPlayerListComponent()
        {
            var playerlistbox = new PlayerListBox(mcServer);
            var jscrollpane = new JScrollPane(playerlistbox, 22, 30);
            jscrollpane.setBorder(new TitledBorder(new EtchedBorder(), "Players"));
            return jscrollpane;
        }

        private JComponent getLogComponent()
        {
            var jpanel = new JPanel(new BorderLayout());
            var jtextarea = new JTextArea();
            logger.addHandler(new GuiLogOutputHandler(jtextarea));
            var jscrollpane = new JScrollPane(jtextarea, 22, 30);
            jtextarea.setEditable(false);
            var jtextfield = new JTextField();
            jtextfield.addActionListener(new ServerGuiCommandListener(this, jtextfield));
            jtextarea.addFocusListener(new ServerGuiFocusAdapter(this));
            jpanel.add(jscrollpane, "Center");
            jpanel.add(jtextfield, "South");
            jpanel.setBorder(new TitledBorder(new EtchedBorder(), "Log and chat"));
            return jpanel;
        }

        public static MinecraftServer getMinecraftServer(ServerGUI servergui)
        {
            return servergui.mcServer;
        }
    }
}