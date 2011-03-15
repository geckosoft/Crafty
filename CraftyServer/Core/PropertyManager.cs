using java.io;
using java.lang;
using java.util;
using java.util.logging;

namespace CraftyServer.Core
{
    public class PropertyManager
    {
        public static Logger logger = Logger.getLogger("Minecraft");
        private readonly Properties serverProperties;
        private readonly File serverPropertiesFile;

        public PropertyManager(File file)
        {
            serverProperties = new Properties();
            serverPropertiesFile = file;
            if (file.exists())
            {
                try
                {
                    serverProperties.load(new FileInputStream(file));
                }
                catch (Exception exception)
                {
                    logger.log(Level.WARNING, (new StringBuilder()).append("Failed to load ").append(file).toString(),
                               exception);
                    generateNewProperties();
                }
            }
            else
            {
                logger.log(Level.WARNING, (new StringBuilder()).append(file).append(" does not exist").toString());
                generateNewProperties();
            }
        }

        public void generateNewProperties()
        {
            logger.log(Level.INFO, "Generating new properties file");
            saveProperties();
        }

        public void saveProperties()
        {
            try
            {
                serverProperties.store(new FileOutputStream(serverPropertiesFile), "Minecraft server properties");
            }
            catch (Exception exception)
            {
                logger.log(Level.WARNING,
                           (new StringBuilder()).append("Failed to save ").append(serverPropertiesFile).toString(),
                           exception);
                generateNewProperties();
            }
        }

        public string getStringProperty(string s, string s1)
        {
            if (!serverProperties.containsKey(s))
            {
                serverProperties.setProperty(s, s1);
                saveProperties();
            }
            return serverProperties.getProperty(s, s1);
        }

        public int getIntProperty(string s, int i)
        {
            try
            {
                return
                    Integer.parseInt(getStringProperty(s,
                                                       (new StringBuilder()).append("").append(i).toString()));
            }
            catch (Exception)
            {
                serverProperties.setProperty(s, (new StringBuilder()).append("").append(i).toString());
            }
            return i;
        }

        public bool getBooleanProperty(string s, bool flag)
        {
            try
            {
                return
                    Boolean.parseBoolean(getStringProperty(s, (new StringBuilder()).append("").append(flag).toString()));
            }
            catch (Exception)
            {
                serverProperties.setProperty(s, (new StringBuilder()).append("").append(flag).toString());
            }
            return flag;
        }

        public void func_22118_b(string s, bool flag)
        {
            serverProperties.setProperty(s, (new StringBuilder()).append("").append(flag).toString());
            saveProperties();
        }
    }
}