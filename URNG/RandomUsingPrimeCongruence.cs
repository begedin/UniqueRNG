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
    public class RandomUsingPrimeCongruence : RandomAbstract
    {
        // used to generate initial value - seed
        private Random random;

        // parameters
        const uint M = 4294967291;  // large prime modulus - period of the generator
        const uint A = 1;           // multiplier
        const uint C = 1;           // increment

        // current value. next value depends on parameters and current value
        private uint x;

        public RandomUsingPrimeCongruence()
        {
            // generate initial value - seed
            random = new Random();
            x = (uint)random.Next();
        }

        // generates a random number on [0,0xffffffff]-interval
        protected override uint generateRandomInt32()
        {
            // compute and return next generator value
            x = (A * x + C) % M;
            return x;
        }
    }
}
