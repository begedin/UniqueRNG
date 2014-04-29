using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace URNG
{
    public class UniqueSequenceGenerator
    {
        private static LinearCongruentialGenerator random;

        /// <summary>
        /// Retrieve a list of unique and random uint values
        /// 
        /// Numbers generated are from the same state of the generator as those retrieved with Next(), so they DO affect the outcome of Next()
        /// </summary>
        /// <param name="size">Amount of values to retrieve</param>
        /// <returns>List of unique and random uint values</returns>
        public static List<uint> Sequence(uint size)
        {
            random = new LinearCongruentialGenerator();

            var numbers = new List<uint>();

            for (int i = 0; i < size; i++)
            {
                numbers.Add(random.Next());
            }

            return numbers;
        }

        /// <summary>
        /// Returns a sequence of random unique unsigned integers
        /// 
        /// Numbers generated are from a different state of the generator, so they DO NOT affect the outcome of Next()
        /// </summary>
        /// <param name="size">Size of sequence</param>
        /// <param name="max">Maximum value of any number in the sequence.</param>
        /// <returns></returns>
        public static List<uint> Sequence(uint size, uint max)
        {
            random = new LinearCongruentialGenerator(max);

            var numbers = new List<uint>();

            for (int i = 0; i < size; i++)
            {
                numbers.Add(random.Next());
            }

            return numbers;

        }

        /// <summary>
        /// Returns a sequence of signed integers with the specified minimum and maximum.
        /// 
        /// Numbers generated are from a different state of the generator, so they DO NOT affect the outcome of Next()
        /// </summary>
        /// <param name="size">Size of sequence</param>
        /// <param name="min">Minimal value of any number in the sequence.</param>
        /// <param name="max">Maximal value of any number in the sequence.</param>
        /// <returns></returns>
        public static List<int> Sequence(uint size, int min, int max)
        {
            random = new LinearCongruentialGenerator(min, max);

            var numbers = new List<int>();

            for (int i = 0; i < size; i++)
            {
                if (min > 0) numbers.Add(random.NextSigned());
            }

            return numbers;
        }
    }
}
