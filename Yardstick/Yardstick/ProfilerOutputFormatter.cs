using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yardstick.Extensions;

namespace Yardstick
{
    public abstract class ProfilerOutputFormatter
    {
        /// <summary>
        /// The results of the profiling operation.
        /// </summary>
        public ProfilerResult Result { get; private set; }

        protected int SafeConsoleWidth
        {
            get
            {
                return (int)(Console.WindowWidth * 0.90);
            }
            set { }
        }

        public ProfilerOutputFormatter(ProfilerResult result)
        {
            this.Result = result;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public virtual void DisplayResults()
        {
            Console.WriteLine(this.ToString());
        }
    }

    public class ProfilerDefaultOutputFormatter : ProfilerOutputFormatter
    {
        public ProfilerDefaultOutputFormatter(ProfilerResult result)
            : base(result)
        {
        }

        public override string ToString()
        {
            var display = new StringBuilder();

            display.Append(this.DrawBoxTop());
            display.Append(this.DrawBoxBodyText(" "));
            display.Append(this.DrawBoxBodyTextCentered(this.Result.ProfiledInstance.TaskName));
            display.Append(this.DrawBoxBodyText(" "));
            display.Append(this.DrawBoxBodySeparator());
            display.Append(this.DrawBoxBodyText(" "));
            display.Append(this.DrawBoxBodyText(String.Format("Number of Iterations: {0}", this.Result.ProfiledInstance.ProfileIterations)));
            display.Append(this.DrawBoxBodyText(String.Format("Total Elapsed Time: {0:n} ms", this.Result.TotalTime)));

            if (this.Result.ProfiledInstance.Info.Count > 0)
            {
                display.Append(this.DrawBoxBodyText(" "));
                foreach (var info in this.Result.ProfiledInstance.Info)
                {
                    display.Append(this.DrawBoxBodyText(String.Format("{0}: {1}", info.Key, info.Value)));
                }
            }

            display.Append(this.DrawBoxBodyText(" "));
            display.Append(this.DrawBoxBodyText("     Before Pruning:"));
            display.Append(this.DrawBoxBodyText(String.Format("          Average per Call: {0:n} ms", this.Result.AverageTime)));
            display.Append(this.DrawBoxBodyText(String.Format("          Standard Deviation: {0:n} ms", this.Result.StandardDeviation)));
            display.Append(this.DrawBoxBodyText(" "));
            display.Append(this.DrawBoxBodyText("     After Pruning:"));
            display.Append(this.DrawBoxBodyText(String.Format("          Low Outliers: {0}", this.Result.PrunedLowOutliers.ToLimitedString(4) )));
            display.Append(this.DrawBoxBodyText(String.Format("          High Outliers: {0}", this.Result.PrunedHighOutliers.ToLimitedString(4) )));
            display.Append(this.DrawBoxBodyText(String.Format("          Average per Call: {0:n} ms", this.Result.PrunedAverageTime)));
            display.Append(this.DrawBoxBodyText(String.Format("          Standard Deviation: {0:n} ms", this.Result.PrunedStandardDeviation)));
            display.Append(this.DrawBoxBodyText(" "));
            display.Append(this.DrawBoxBottom());
            return display.ToString();
        }

        private StringBuilder DrawBoxTop()
        {
            var display = new StringBuilder();
            display.Append("╔");
            display.Append(new String('═', this.SafeConsoleWidth));
            display.Append("╗");
            display.AppendLine();
            return display;
        }

        private StringBuilder DrawBoxBodyText(string text)
        {
            var display = new StringBuilder();
            display.Append("║ ");
            display.AppendFormat("{0,-" + (this.SafeConsoleWidth - 1) + "}", text);
            display.Append("║");
            display.AppendLine();
            return display;
        }

        private StringBuilder DrawBoxBodyTextCentered(string text)
        {
            var display = new StringBuilder();
            display.Append("║ ");
            display.AppendFormat("{0,-" + (this.SafeConsoleWidth - 1) + "}", text.Centered(this.SafeConsoleWidth - 1));
            display.Append("║");
            display.AppendLine();
            return display;
        }

        private StringBuilder DrawBoxBodySeparator()
        {
            var display = new StringBuilder();
            display.Append("║");
            display.Append(new String('═', this.SafeConsoleWidth));
            display.Append("║");
            display.AppendLine();
            return display;
        }

        private StringBuilder DrawBoxBottom()
        {
            var display = new StringBuilder();
            display.Append("╚");
            display.Append(new String('═', this.SafeConsoleWidth));
            display.Append("╝");
            display.AppendLine();
            return display;
        }
    }
}
