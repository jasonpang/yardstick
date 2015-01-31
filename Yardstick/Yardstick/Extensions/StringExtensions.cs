using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yardstick.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns the text centered and padded left and right with spaces within the specified width.
        /// </summary>
        public static String Centered(this String text, int width)
        {
            string padding = new String(' ', (width - text.Length) / 2);
            return padding + text + padding;
        }
    }
}
