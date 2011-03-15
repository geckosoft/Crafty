using java.awt.@event;

namespace CraftyServer.Core
{
    public class GuiStatsListener
        : ActionListener
    {
        private readonly GuiStatsComponent statsComponent; /* synthetic field */

        public GuiStatsListener(GuiStatsComponent guistatscomponent)
        {
            statsComponent = guistatscomponent;
        }

        #region ActionListener Members

        public void actionPerformed(ActionEvent actionevent)
        {
            GuiStatsComponent.update(statsComponent);
        }

        #endregion
    }
}