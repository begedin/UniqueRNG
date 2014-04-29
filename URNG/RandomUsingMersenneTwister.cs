using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace URNG
{
    /// <summary>
    /// Generates random numbers using the Mersenne Twister algorithm.
    /// 
    /// NOTE: This one currently doesn't work properly and returns duplicates for longer sequences. 
    /// Since a proper MT algorithm should have an extremely long period, this means there's 
    /// probably an error in this implementation I haven't managed to track down yet.
    /// </summary>
    public class RandomUsingMersenneTwister : RandomBase
    {
        // Period parameters.
        private const int N = 624; // 107454
        private const int M = 397;
        private const uint MATRIX_A = 0x9908b0dfU;   // constant vector a - 2567483615
        private const uint UPPER_MASK = 0x80000000U; // most significant w-r bits
        private const uint LOWER_MASK = 0x7fffffffU; // least significant r bits
        private const int MAX_RAND_INT = 0x7fffffff;

        private const uint TEMPERING_MASK_B = 0x9d2c5680U;
        private const uint TEMPERING_MASK_C = 0xefc60000U;

        // the array for the state vector
        private uint[] x = new uint[N];

        // mti==N+1 means mt[N] is not initialized
        private int index = 0;

        private Random random;

        /// <summary>
        /// Creates a random number generator using the time of day in milliseconds as
        /// the seed.
        /// </summary>
        public RandomUsingMersenneTwister()
        {
            random = new Random();
            initGenerator((uint)random.NextDouble());
        }

        // initializes mt[N] with a seed
        private void initGenerator(uint seed)
        {
            x[0] = seed & 0xffffffffU;
            
            for (int i = 1; i < N; i++)
            {
                x[i] = (uint)(1812433253U * (x[i - 1] ^ (x[i - 1] >> 30)) + i);
                x[i] &= 0xffffffffU; // for >32 bit machines, takes last 32 bits
            }
        }

        // generates a random number on [0,0xffffffff]-interval
        protected override uint generateRandomInt32()
        {
            if (index == N) generateNumbers();

            uint y = x[index++];

            // Tempering
            y ^= (y >> 11);
            y ^= (y << 7) & TEMPERING_MASK_B;
            y ^= (y << 15) & TEMPERING_MASK_C;
            y ^= (y >> 18);

            return y;
        }

        private void generateNumbers()
        {
            index = 0;

            uint y, a;

            for (int i = 0; i < N - 1; i++)
            {
                y = (x[i] & UPPER_MASK) | x[i + 1] & LOWER_MASK;
                a = ((y & 0x1U) > 0) ? MATRIX_A : 0x0U;
                x[i] = (uint)(x[(i + M) % N] ^ (y >> 1) ^ a);
            }

            y = (x[N-1] & UPPER_MASK) | x[0] & LOWER_MASK;
            a = (y & 0x1U) > 0 ? MATRIX_A : 0x0U;
            x[N - 1] = (uint)(x[M - 1] ^ (y >> 1) ^ a);
        }
    }
}

