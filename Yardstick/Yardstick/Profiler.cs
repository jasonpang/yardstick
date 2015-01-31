using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yardstick
{
    public static class Profiler
    {
        public static ProfilerResult Profile(IProfilable instance)
        {
            var profileResults = new ProfilerResult(instance);

            // Perform a test run
            instance.Setup();
            instance.Run();
            instance.Cleanup();

            Stopwatch timer = new Stopwatch();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            for (int i = 0; i < instance.ProfileIterations; i++)
            {
                instance.Setup();

                timer.Start();
                instance.Run();
                timer.Stop();

                profileResults.Records.Add(timer.Elapsed.TotalMilliseconds);
                timer.Reset();
            }

            profileResults.ProcessResults();
            return profileResults;
        }
    }
}
