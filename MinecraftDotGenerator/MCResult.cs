using System;
using System.Collections.Generic;
using System.Linq;

namespace MinecraftDotGenerator
{
    /// <summary> MCResult class. </summary>
    internal sealed class MCResult
    {
        /// <summary> Initializes a new instance of the <see cref="MCResult" /> class. </summary>
        /// <param name="items"> The items. </param>
        public MCResult(IEnumerable<double> items)
        {
            double[] numbers = items.OrderBy(x => x).ToArray();
            Max = numbers.Max();
            Min = numbers.Min();
            Mean = numbers.Average();

            int middleIndex = numbers.Length / 2;
            Median = numbers.Length % 2 == 0 ? (numbers[middleIndex - 1] + numbers[middleIndex]) / 2 : numbers[middleIndex];

            double sumOfSquaredDifferences = numbers.Sum(e => Math.Pow(e - Mean, 2));
            StdDev = Math.Sqrt(sumOfSquaredDifferences / numbers.Length);
        }

        /// <summary> Determines the maximum of the parameters. </summary>
        /// <value> The maximum. </value>
        public double Max { get; }

        /// <summary> Gets the mean. </summary>
        /// <value> The mean. </value>
        public double Mean { get; }

        /// <summary> Gets the median. </summary>
        /// <value> The median. </value>
        public double Median { get; }

        /// <summary> Determines the minimum of the parameters. </summary>
        /// <value> The minimum. </value>
        public double Min { get; }

        /// <summary> Gets the standard dev. </summary>
        /// <value> The standard dev. </value>
        public double StdDev { get; }
    }
}
