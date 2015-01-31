using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yardstick.Extensions
{
    /* Source: http://www.martijnkooij.nl/2013/04/csharp-math-mean-variance-and-standard-deviation */
    public static class ListExtensions
    {
        public static double Mean(this List<double> values)
        {
            return values.Count == 0 ? 0 : values.Mean(0, values.Count);
        }

        public static double Mean(this List<double> values, int start, int end)
        {
            double s = 0;

            for (int i = start; i < end; i++)
            {
                s += values[i];
            }

            return s / (end - start);
        }

        public static double Variance(this List<double> values)
        {
            return values.Variance(values.Mean(), 0, values.Count);
        }

        public static double Variance(this List<double> values, double mean)
        {
            return values.Variance(mean, 0, values.Count);
        }

        public static double Variance(this List<double> values, double mean, int start, int end)
        {
            double variance = 0;

            for (int i = start; i < end; i++)
            {
                variance += Math.Pow((values[i] - mean), 2);
            }

            int n = end - start;
            if (start > 0) n -= 1;

            return variance / (n);
        }

        public static double StandardDeviation(this List<double> values)
        {
            return values.Count == 0 ? 0 : values.StandardDeviation(0, values.Count);
        }

        public static double StandardDeviation(this List<double> values, int start, int end)
        {
            double mean = values.Mean(start, end);
            double variance = values.Variance(mean, start, end);

            return Math.Sqrt(variance);
        }

        /// <summary>
        /// Removes values lower or higher than the standard deviation from the list, and respectively returns the low and high outliers.
        /// </summary>
        public static Tuple<List<double>, List<double>> Prune(this List<double> values)
        {
            double standardDeviation = values.StandardDeviation();
            double mean = values.Mean();

            List<double> lowOutliers = new List<double>();
            List<double> highOutliers = new List<double>();

            for (int i = values.Count - 1; i >= 0; i--)
            {
                double value = values[i];

                if (((mean - value) > 0) && (Math.Abs(mean - value) > standardDeviation))
                {
                    lowOutliers.Add(value);
                    values.Remove(value);
                }
                else if (((mean - value) < 0) && (Math.Abs(mean - value) > standardDeviation))
                {
                    highOutliers.Add(value);
                    values.Remove(value);
                }
            }

            return new Tuple<List<double>, List<double>>(lowOutliers, highOutliers);
        }

        public static String ToLimitedString(this List<double> values, int limit)
        {
            limit = Math.Min(values.Count, limit);
            StringBuilder display = new StringBuilder();
            display.Append("[");
            if (limit > 0)
            {
                for (int i = 0; i < limit; i++)
                    display.Append(values[i] + ", ");
                display = display.Remove(display.Length - 2, 2);
            }
            display.Append("]");
            return display.ToString();
        }
    }
}
