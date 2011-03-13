using java.awt.@event;

namespace CraftyServer.Core
{
    public class GuiStatsListener
        : ActionListener
    {
        public GuiStatsListener(GuiStatsComponent guistatscomponent)
        {
            statsComponent = guistatscomponent;
        }

        public void actionPerformed(ActionEvent actionevent)
        {
            GuiStatsComponent.update(statsComponent);
        }

        private GuiStatsComponent statsComponent; /* synthetic field */
    }
}