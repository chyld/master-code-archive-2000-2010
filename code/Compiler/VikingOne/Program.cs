using System;
using VikingOne.Common;

namespace VikingOne.Master
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var u = new Utility();

            var s = new Source("simple.txt");
            var k = new Scanner(s);
            var p = new Parser(k);

            p.Parse();
        }
    }
}
