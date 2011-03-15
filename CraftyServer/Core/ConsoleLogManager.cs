using java.lang;
using java.util.logging;

namespace CraftyServer.Core
{
    public class ConsoleLogManager
    {
        public static Logger logger = Logger.getLogger("Minecraft");

        public static void init()
        {
            var consolelogformatter = new ConsoleLogFormatter();
            logger.setUseParentHandlers(false);
            var consolehandler = new ConsoleHandler();
            consolehandler.setFormatter(consolelogformatter);
            logger.addHandler(consolehandler);


            try
            {
                var filehandler = new FileHandler("server.log", true);
                filehandler.setFormatter(consolelogformatter);
                logger.addHandler(filehandler);
            }
            catch (Exception exception)
            {
                logger.log(Level.WARNING, "Failed to log to server.log", exception);
            }
        }
    }
}