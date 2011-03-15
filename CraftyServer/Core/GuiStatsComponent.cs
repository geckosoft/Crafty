using java.awt;
using java.lang;
using javax.swing;

namespace CraftyServer.Core
{
    public class GuiStatsComponent : JComponent
    {
        private readonly string[] displayStrings;
        private readonly int[] memoryUse;
        private int updateCounter;

        public GuiStatsComponent()
        {
            memoryUse = new int[256];
            updateCounter = 0;
            displayStrings = new string[10];
            setPreferredSize(new Dimension(256, 196));
            setMinimumSize(new Dimension(256, 196));
            setMaximumSize(new Dimension(256, 196));
            (new Timer(500, new GuiStatsListener(this))).start();
            setBackground(Color.BLACK);
        }

        private void updateStats()
        {
            long l = Runtime.getRuntime().totalMemory() - Runtime.getRuntime().freeMemory();
            java.lang.System.gc();
            displayStrings[0] =
                (new StringBuilder()).append("Memory use: ").append(l/1024L/1024L).append(" mb (").append(
                    (Runtime.getRuntime().freeMemory()*100L)/Runtime.getRuntime().maxMemory()).append("% free)").
                    toString();
            displayStrings[1] =
                (new StringBuilder()).append("Threads: ").append(NetworkManager.numReadThreads).append(" + ").append(
                    NetworkManager.numWriteThreads).toString();
            memoryUse[updateCounter++ & 0xff] = (int) ((l*100L)/Runtime.getRuntime().maxMemory());
            repaint();
        }

        public override void paint(Graphics g)
        {
            g.setColor(new Color(0xffffff));
            g.fillRect(0, 0, 256, 192);
            for (int i = 0; i < 256; i++)
            {
                int k = memoryUse[i + updateCounter & 0xff];
                g.setColor(new Color(k + 28 << 16));
                g.fillRect(i, 100 - k, 1, k);
            }

            g.setColor(Color.BLACK);
            for (int j = 0; j < displayStrings.Length; j++)
            {
                string s = displayStrings[j];
                if (s != null)
                {
                    g.drawString(s, 32, 116 + j*16);
                }
            }
        }

        public static void update(GuiStatsComponent guistatscomponent)
        {
            guistatscomponent.updateStats();
        }
    }
}