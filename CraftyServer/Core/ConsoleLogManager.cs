using java.util.logging;
using java.lang;

namespace CraftyServer.Core
{
    public class ConsoleLogManager
    {
        public ConsoleLogManager()
        {
        }

        public static void init()
        {
            ConsoleLogFormatter consolelogformatter = new ConsoleLogFormatter();
            logger.setUseParentHandlers(false);
            ConsoleHandler consolehandler = new ConsoleHandler();
            consolehandler.setFormatter(consolelogformatter);
            logger.addHandler(consolehandler);


            try
            {
                FileHandler filehandler = new FileHandler("server.log", true);
                filehandler.setFormatter(consolelogformatter);
                logger.addHandler(filehandler);
            }
            catch (Exception exception)
            {
                logger.log(Level.WARNING, "Failed to log to server.log", exception);
            }
        }

        public static Logger logger = Logger.getLogger("Minecraft");
    }
}