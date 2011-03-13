using CraftyServer.Server;
using java.awt;
using java.util.logging;
using javax.swing;
using javax.swing.border;
using java.lang;

namespace CraftyServer.Core
{
    public class ServerGUI : JComponent
                             , ICommandListener
    {
        public static void initGui(MinecraftServer minecraftserver)
        {
            try
            {
                UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
            }
            catch (Exception)
            {
            }
            ServerGUI servergui = new ServerGUI(minecraftserver);
            JFrame jframe = new JFrame("Minecraft server");
            jframe.add(servergui);
            jframe.pack();
            jframe.setLocationRelativeTo(null);
            jframe.setVisible(true);
            jframe.addWindowListener(new ServerWindowAdapter(minecraftserver));
        }

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

        private JComponent getStatsComponent()
        {
            JPanel jpanel = new JPanel(new BorderLayout());
            jpanel.add(new GuiStatsComponent(), "North");
            jpanel.add(getPlayerListComponent(), "Center");
            jpanel.setBorder(new TitledBorder(new EtchedBorder(), "Stats"));
            return jpanel;
        }

        private JComponent getPlayerListComponent()
        {
            PlayerListBox playerlistbox = new PlayerListBox(mcServer);
            JScrollPane jscrollpane = new JScrollPane(playerlistbox, 22, 30);
            jscrollpane.setBorder(new TitledBorder(new EtchedBorder(), "Players"));
            return jscrollpane;
        }

        private JComponent getLogComponent()
        {
            JPanel jpanel = new JPanel(new BorderLayout());
            JTextArea jtextarea = new JTextArea();
            logger.addHandler(new GuiLogOutputHandler(jtextarea));
            JScrollPane jscrollpane = new JScrollPane(jtextarea, 22, 30);
            jtextarea.setEditable(false);
            JTextField jtextfield = new JTextField();
            jtextfield.addActionListener(new ServerGuiCommandListener(this, jtextfield));
            jtextarea.addFocusListener(new ServerGuiFocusAdapter(this));
            jpanel.add(jscrollpane, "Center");
            jpanel.add(jtextfield, "South");
            jpanel.setBorder(new TitledBorder(new EtchedBorder(), "Log and chat"));
            return jpanel;
        }

        public void log(string s)
        {
            logger.info(s);
        }

        public string getUsername()
        {
            return "CONSOLE";
        }

        public static MinecraftServer getMinecraftServer(ServerGUI servergui)
        {
            return servergui.mcServer;
        }

        public static Logger logger = Logger.getLogger("Minecraft");
        private MinecraftServer mcServer;
    }
}