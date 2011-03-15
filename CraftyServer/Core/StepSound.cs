using java.lang;

namespace CraftyServer.Core
{
    public class StepSound
    {
        public float field_1028_b;
        public string field_1029_a;
        public float field_1030_c;

        public StepSound(string s, float f, float f1)
        {
            field_1029_a = s;
            field_1028_b = f;
            field_1030_c = f1;
        }

        public float func_738_a()
        {
            return field_1028_b;
        }

        public float func_739_b()
        {
            return field_1030_c;
        }

        public string func_737_c()
        {
            return (new StringBuilder()).append("step.").append(field_1029_a).toString();
        }
    }
}