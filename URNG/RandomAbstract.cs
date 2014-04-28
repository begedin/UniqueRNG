using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace URNG
{
    public abstract class RandomAbstract
    {

        protected abstract uint generateRandomInt32();

        /// <summary>
        /// Generates a random positive integer
        /// </summary>
        /// <returns>Random and unique integer value</returns>
        protected int generateRandomInt31()
        {
            return (int)(generateRandomInt32() >> 1);
        }

        /// <summary>
        /// Generates a random number on [0,1]-real-interval
        /// </summary>
        /// <returns>Random number on [0,1]-real-interval (includes 0, includes 1)</returns>
        double generateRandomRealIncluding1()
        {
            return generateRandomInt32() * (1.0 / 4294967295.0);
            // divided by 2^32-1
        }

        /// <summary>
        /// Generates a random number on [0,1)-real-interval
        /// </summary>
        /// <returns>Random number on [0,1)-real-interval (includes 0, excludes 1)</returns>
        double generateRandomRealExcluding1()
        {
            return generateRandomInt32() * (1.0 / 4294967296.0);
            // divided by 2^32
        }

        /// <summary>
        /// Generates a random number on (0,1)-real-interval
        /// </summary>
        /// <returns>Random number on (0,1)-real-interval (excludes 0, excludes 1)</returns>
        double generateRandomRealExcluding01()
        {
            return (((double)generateRandomInt32()) + 0.5) * (1.0 / 4294967296.0);
            // divided by 2^32
        }

        /// <summary>
        /// Generates a random number on [0,1) with 53-bit resolution
        /// </summary>
        /// <returns></returns>
        double generateRandomReal53Bit()
        {
            uint a = generateRandomInt32() >> 5, b = generateRandomInt32() >> 6;
            return (a * 67108864.0 + b) * (1.0 / 9007199254740992.0);
        }

        /// <summary>
        /// Returns a random integer greater than or equal to zero and
        /// less than or equal to <c>MaxRandomInt</c>. 
        /// </summary>
        /// <returns>The next random integer.</returns>
        public int Next()
        {
            return generateRandomInt31();
        }

        /// <summary>
        /// Returns a positive random integer less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">The maximum value. Must be greater than zero.</param>
        /// <returns>A positive random integer less than or equal to <c>maxValue</c>.</returns>
        public int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

        /// <summary>
        /// Returns a random integer within the specified range.
        /// </summary>
        /// <param name="minValue">The lower bound.</param>
        /// <param name="maxValue">The upper bound.</param>
        /// <returns>A random integer greater than or equal to <c>minValue</c>, and less than
        /// or equal to <c>maxValue</c>.</returns>
        public int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                int tmp = maxValue;
                maxValue = minValue;
                minValue = tmp;
            }

            return (int)(Math.Floor((maxValue - minValue + 1) * generateRandomRealIncluding1() + minValue));
        }

        /// <summary>
        /// Returns a random number between 0.0 and 1.0.
        /// </summary>
        /// <returns>A single-precision floating point number greater than or equal to 0.0, 
        /// and less than 1.0.</returns>
        public float NextFloat()
        {
            return (float)generateRandomRealExcluding1();
        }

        /// <summary>
        /// Returns a random number greater than or equal to zero, and either strictly
        /// less than one, or less than or equal to one, depending on the value of the
        /// given boolean parameter.
        /// </summary>
        /// <param name="includeOne">
        /// If <c>true</c>, the random number returned will be 
        /// less than or equal to one; otherwise, the random number returned will
        /// be strictly less than one.
        /// </param>
        /// <returns>
        /// If <c>includeOne</c> is <c>true</c>, this method returns a
        /// single-precision random number greater than or equal to zero, and less
        /// than or equal to one. If <c>includeOne</c> is <c>false</c>, this method
        /// returns a single-precision random number greater than or equal to zero and
        /// strictly less than one.
        /// </returns>
        public float NextFloat(bool includeOne)
        {
            if (includeOne)
            {
                return (float)generateRandomRealIncluding1();
            }
            return (float)generateRandomRealExcluding1();
        }

        /// <summary>
        /// Returns a random number greater than 0.0 and less than 1.0.
        /// </summary>
        /// <returns>A random number greater than 0.0 and less than 1.0.</returns>
        public float NextFloatPositive()
        {
            return (float)generateRandomRealExcluding01();
        }

        /// <summary>
        /// Returns a random number between 0.0 and 1.0.
        /// </summary>
        /// <returns>A double-precision floating point number greater than or equal to 0.0, 
        /// and less than 1.0.</returns>
        public double NextDouble()
        {
            return generateRandomRealExcluding1();
        }

        /// <summary>
        /// Returns a random number greater than or equal to zero, and either strictly
        /// less than one, or less than or equal to one, depending on the value of the
        /// given boolean parameter.
        /// </summary>
        /// <param name="includeOne">
        /// If <c>true</c>, the random number returned will be 
        /// less than or equal to one; otherwise, the random number returned will
        /// be strictly less than one.
        /// </param>
        /// <returns>
        /// If <c>includeOne</c> is <c>true</c>, this method returns a
        /// single-precision random number greater than or equal to zero, and less
        /// than or equal to one. If <c>includeOne</c> is <c>false</c>, this method
        /// returns a single-precision random number greater than or equal to zero and
        /// strictly less than one.
        /// </returns>
        public double NextDouble(bool includeOne)
        {
            if (includeOne)
            {
                return generateRandomRealIncluding1();
            }
            return generateRandomRealExcluding1();
        }

        /// <summary>
        /// Returns a random number greater than 0.0 and less than 1.0.
        /// </summary>
        /// <returns>A random number greater than 0.0 and less than 1.0.</returns>
        public double NextDoublePositive()
        {
            return generateRandomRealExcluding01();
        }

        /// <summary>
        /// Generates a random number on <c>[0,1)</c> with 53-bit resolution.
        /// </summary>
        /// <returns>A random number on <c>[0,1)</c> with 53-bit resolution</returns>
        public double Next53BitRes()
        {
            return generateRandomReal53Bit();
        }

        /// <summary>
        /// Retrieve a list of unique and random uint values
        /// </summary>
        /// <param name="size">Amount of values to retrieve</param>
        /// <returns>List of unique and random uint values</returns>
        public List<uint> Sequence(int size)
        {
            var numbers = new List<uint>();

            for (int i = 0; i < size; i++)
            {
                numbers.Add(generateRandomInt32());
            }

            return numbers;
        }
    }
}
