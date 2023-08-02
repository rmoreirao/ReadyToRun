using LibBLibADependsOnThis;

namespace LibAWpfDependsOnThis
{
    public class Class1LibA
    {
        public void Test() {
            var test = new ClassLibB();
            test.Test();
        }
    }
}