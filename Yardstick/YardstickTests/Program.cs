using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yardstick;

namespace YardstickTests
{
    class Program
    {
        static void Main(string[] args)
        {
            ProfilerResult result = Profiler.Profile(new ArraySortTest(numIterations: 34, numElements: 1000000));
            new ProfilerDefaultOutputFormatter(result).DisplayResults();
            Console.WriteLine("Finished.");
            Console.ReadLine();
        }
    }
}
