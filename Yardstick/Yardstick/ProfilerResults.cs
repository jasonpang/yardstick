using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yardstick.Extensions;

namespace Yardstick
{
    public class ProfilerResult
    {
        /// <summary>
        /// Gets the instance of the class that was profiled.
        /// </summary>
        public IProfilable ProfiledInstance { get; private set; }

        /// <summary>
        /// Gets the profile records, as a list of function timings in milliseconds.
        /// </summary>
        public List<double> Records { get; private set; }

        /// <summary>
        /// Gets the total time in milliseconds to complete profiling.
        /// </summary>
        public double TotalTime { get; private set; }

        /// <summary>
        /// Gets an unpruned average running time in milliseconds of Run().
        /// </summary>
        public double AverageTime { get; private set; }

        /// <summary>
        /// Gets the unpruned standard deviation of the records.
        /// </summary>
        public double StandardDeviation { get; private set; }

        /// <summary>
        /// Gets a pruned average running time in milliseconds of Run().
        /// Records below or above the mean by the standard deviation amount are discarded.
        /// </summary>
        public double PrunedAverageTime { get; private set; }

        /// <summary>
        /// Gets a pruned standard deviation of the records.
        /// Records below or above the mean by the standard deviation amount are discarded, and the standard deviation is recalculated.
        /// </summary>
        public double PrunedStandardDeviation { get; private set; }

        /// <summary>
        /// Gets the list of timings that were below the mean by the standard deviation amount.
        /// </summary>
        public List<double> PrunedLowOutliers { get; private set; }

        /// <summary>
        /// Gets the list of timings that were above the mean by the standard deviation amount.
        /// </summary>
        public List<double> PrunedHighOutliers { get; private set; }

        /// <summary>
        /// Creates a new ProfilerResult instance with an instance of the class to be profiled.
        /// </summary>
        /// <param name="instance">An instance of the class to be profiled.</param>
        public ProfilerResult(IProfilable instance)
        {
            this.ProfiledInstance = instance;
            this.Records = new List<double>(instance.ProfileIterations);
        }

        /// <summary>
        /// Calculates the average and standard deviation, both pruned and unpruned, from the list of records.
        /// </summary>
        public void ProcessResults()
        {
            this.TotalTime = Records.Sum();
            this.AverageTime = Records.Mean();
            this.StandardDeviation = Records.StandardDeviation();

            var tupleOutliers = Records.Prune();

            this.PrunedAverageTime = Records.Mean();
            this.PrunedStandardDeviation = Records.StandardDeviation();
            this.PrunedLowOutliers = tupleOutliers.Item1;
            this.PrunedHighOutliers = tupleOutliers.Item2;
        }
    }
}
