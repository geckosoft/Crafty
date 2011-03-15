using java.util.logging;
using javax.swing;

namespace CraftyServer.Core
{
    public class GuiLogOutputHandler : Handler
    {
        private readonly JTextArea field_1000_d;
        private readonly int[] field_998_b;
        private readonly Formatter field_999_a;
        private int field_1001_c;

        public GuiLogOutputHandler(JTextArea jtextarea)
        {
            field_998_b = new int[1024];
            field_1001_c = 0;
            field_999_a = new GuiLogFormatter(this);
            setFormatter(field_999_a);
            field_1000_d = jtextarea;
        }

        public override void close()
        {
        }

        public override void flush()
        {
        }

        public override void publish(LogRecord logrecord)
        {
            int i = field_1000_d.getDocument().getLength();
            field_1000_d.append(field_999_a.format(logrecord));
            field_1000_d.setCaretPosition(field_1000_d.getDocument().getLength());
            int j = field_1000_d.getDocument().getLength() - i;
            if (field_998_b[field_1001_c] != 0)
            {
                field_1000_d.replaceRange("", 0, field_998_b[field_1001_c]);
            }
            field_998_b[field_1001_c] = j;
            field_1001_c = (field_1001_c + 1)%1024;
        }
    }
}