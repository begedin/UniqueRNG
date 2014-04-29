using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace URNG
{
    /// <summary>
    /// A linear congruence generator using a large prime number close to, but smaller than 2^32. 
    /// A single instance returns pseudo random unique numbers up to a maximum sequence length of 4294967291.
    /// Once the maximum length is reached, the exact same sequence repeats.
    /// </summary>
    public class RandomUsingPrimeCongruence : RandomBase
    {
        // used to generate initial value - seed
        private Random random;

        // fixed parameters for default constructor
        const uint M = 4294967291;  // large prime modulus - period of the generator
        const uint A = 1;           // multiplier
        const uint C = 1;           // increment

        // current value. next value depends on parameters and current value
        private uint x;
        // parameters
        private uint m;
        private uint a;
        private uint c;

        private uint range = 0;

        /// <summary>
        /// Creates a new linear congruential generator. 
        /// /// </summary>
        public RandomUsingPrimeCongruence()
        {
            init();
        }

        public RandomUsingPrimeCongruence(uint desiredRange)
        {
            init(desiredRange);
        }

        /// <summary>
        /// Initializes the generator with values set so it generates unsigned integers from the entire unsigned integer range.
        /// </summary>
        private void init()
        {
            this.random = new Random();
            this.m = M;
            this.x = (uint)random.Next();
            this.a = m + 1;
            this.c = (uint)random.Next();

            System.Diagnostics.Debug.WriteLine("x: " + x.ToString() + ", m: " + m.ToString() + ", a: " + a.ToString() + ", c: " + c.ToString());
        }

        /// <summary>
        /// Initializes the generator with values set so it generates unsigned integers in a specific, desired range.
        /// </summary>
        /// <param name="desiredRange"></param>
        private void init(uint desiredRange)
        {
            this.random = new Random();
            this.m = findPrime(desiredRange);
            this.x = (uint)random.Next(1, (int)desiredRange);
            this.a = m + 1;
            this.c = (uint)random.Next(1, (int)desiredRange);
            this.range = desiredRange;

            System.Diagnostics.Debug.WriteLine("x: " + x.ToString() + ", m: " + m.ToString() + ", a: " + a.ToString() + ", c: " + c.ToString());
        }

        // generates a random number on [0,0xffffffff]-interval
        protected override uint generateRandomInt32()
        {
            // compute and return next generator value
            while (true)
            {
                x = (uint)((a * (ulong)x + c) % m);
                if ((range == 0) || x < range )return x;
            }        
        }

        private uint findPrime(uint number)
        {
            long prime;

            // bool isPrime = true;
            for (long i = number; ; i++)
            {
                bool isPrime = true; // Move initialization to here
                for (long j = 2; j < i; j++) // you actually only need to check up to sqrt(i)
                {
                    if (i % j == 0) // you don't need the first condition
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    prime = i;
                    break;
                }            
            }

            return (uint)prime;
        }

        /// <summary>
        /// Returns a sequence of random unique unsigned integers
        /// </summary>
        /// <param name="size">Size of sequence</param>
        /// <param name="max">Maximum value of any number in the sequence.</param>
        /// <returns></returns>
        public List<uint> Sequence(int size, int max)
        {
            init((uint)max);

            var numbers = new List<uint>();

            for (int i = 0; i < size; i++)
            {
                numbers.Add(Next());
            }

            return numbers;
        }

        /// <summary>
        /// Returns a sequence of signed integers with the specified minimum and maximum.
        /// </summary>
        /// <param name="size">Size of sequence</param>
        /// <param name="min">Minimal value of any number in the sequence.</param>
        /// <param name="max">Maximal value of any number in the sequence.</param>
        /// <returns></returns>
        public List<int> Sequence(int size, int min, int max)
        {
            init((uint)((uint)max - min));

            var numbers = new List<int>();

            for (int i = 0; i < size; i++)
            {
                numbers.Add(unchecked((int)Next()) + min);
            }
            init();
            return numbers;
        }
    }
}
