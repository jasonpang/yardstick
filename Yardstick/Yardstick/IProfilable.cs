using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yardstick
{
    /// <summary>
    /// Defines the interface for a profilable class.
    /// </summary>
    public interface IProfilable
    {
        /// <summary>
        /// Gets or sets the descriptive name of the task to be displayed.
        /// </summary>
        String TaskName { get; set; }

        /// <summary>
        /// Gets or sets the number of iterations to profile.
        /// </summary>
        int ProfileIterations { get; set; }

        /// <summary>
        /// Performs initialization and setup tasks that are not to be profiled.
        /// </summary>
        void Setup();

        /// <summary>
        /// Performs the code to be profiled.
        /// </summary>
        void Run();

        /// <summary>
        /// Performs cleanup and reset tasks that are not to be profiled.
        /// </summary>
        void Cleanup();
    }
}
