using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yardstick;

namespace YardstickTests
{
    public class ArraySortTest : IProfilable
    {
        /// <summary>
        /// Gets or sets the descriptive name of the task to be displayed.
        /// </summary>
        public String TaskName { get; set; }

        /// <summary>
        /// Gets or sets the number of iterations to profile.
        /// </summary>
        public int ProfileIterations { get; set; }

        /// <summary>
        /// A dictionary of information to be displayed in the output formatter.
        /// </summary>
        public Dictionary<String, String> Info { get; set; }

        private int numElements = 1000000;
        private int[] numbers;
        private Random random = new Random();

        public ArraySortTest(int numIterations, int numElements)
        {
            this.numElements = numElements;
            this.ProfileIterations = numIterations;
            this.TaskName = String.Format("Sorting an Array of {0} Elements", this.numElements);
            this.Info = new Dictionary<string, string>();
        }

        /// <summary>
        /// Performs initialization and setup tasks that are not to be profiled.
        /// </summary>
        public void Setup()
        {
            numbers = new int[numElements];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(0, numElements);
            }
        }

        /// <summary>
        /// Performs the code to be profiled.
        /// </summary>
        public void Run()
        {
            Array.Sort(numbers);
        }

        /// <summary>
        /// Performs cleanup and reset tasks that are not to be profiled.
        /// </summary>
        public void Cleanup()
        {
            int thirdNumber = numbers[3];
        }
    }
}
