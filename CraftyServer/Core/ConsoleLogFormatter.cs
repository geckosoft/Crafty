using System.Diagnostics;
using java.io;
using java.lang;
using java.text;
using java.util.logging;

namespace CraftyServer.Core
{
    public class ConsoleLogFormatter : Formatter
    {
        private readonly SimpleDateFormat dateFormat;

        public ConsoleLogFormatter()
        {
            dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        }

        public override string format(LogRecord logrecord)
        {
            var stringbuilder = new StringBuilder();
            stringbuilder.append(dateFormat.format(Long.valueOf(logrecord.getMillis())));
            Level level = logrecord.getLevel();
            if (level == Level.FINEST)
            {
                stringbuilder.append(" [FINEST] ");
            }
            else if (level == Level.FINER)
            {
                stringbuilder.append(" [FINER] ");
            }
            else if (level == Level.FINE)
            {
                stringbuilder.append(" [FINE] ");
            }
            else if (level == Level.INFO)
            {
                stringbuilder.append(" [INFO] ");
            }
            else if (level == Level.WARNING)
            {
                stringbuilder.append(" [WARNING] ");
            }
            else if (level == Level.SEVERE)
            {
                stringbuilder.append(" [SEVERE] ");
            }
            else if (level == Level.SEVERE)
            {
                stringbuilder.append(
                    (new StringBuilder()).append(" [").append(level.getLocalizedName()).append("] ").toString());
            }
            stringbuilder.append(logrecord.getMessage());
            stringbuilder.append('\n');
            var throwable = logrecord.getThrown() as Throwable;
            if (throwable != null)
            {
                var stringwriter = new StringWriter();
                throwable.printStackTrace(new PrintWriter(stringwriter));
                stringbuilder.append(stringwriter.toString());
            }

            Debug.WriteLine(stringbuilder.toString());
            return stringbuilder.toString();
        }
    }
}