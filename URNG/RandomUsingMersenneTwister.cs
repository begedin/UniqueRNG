﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace URNG
{
    /// <summary>
    /// 
    /// </summary>
    public class RandomUsingMersenneTwister : RandomAbstract
    {
        // Period parameters.
        private const int N = 624;
        private const int M = 397;
        private const uint MATRIX_A = 0x9908b0dfU;   // constant vector a
        private const uint UPPER_MASK = 0x80000000U; // most significant w-r bits
        private const uint LOWER_MASK = 0x7fffffffU; // least significant r bits
        private const int MAX_RAND_INT = 0x7fffffff;

        // mag01[x] = x * MATRIX_A  for x=0,1
        private uint[] mag01 = { 0x0U, MATRIX_A };

        // the array for the state vector
        private uint[] mt = new uint[N];

        // mti==N+1 means mt[N] is not initialized
        private int mti = N + 1;

        /// <summary>
        /// Creates a random number generator using the time of day in milliseconds as
        /// the seed.
        /// </summary>
        public RandomUsingMersenneTwister()
        {
            initGenerator((uint)DateTime.Now.Millisecond);
        }

        // initializes mt[N] with a seed
        private void initGenerator(uint seed)
        {
            mt[0] = seed & 0xffffffffU;
            for (mti = 1; mti < N; mti++)
            {
                mt[mti] =
                  (uint)(1812433253U * (mt[mti - 1] ^ (mt[mti - 1] >> 30)) + mti);
                // See Knuth TAOCP Vol2. 3rd Ed. P.106 for multiplier. 
                // In the previous versions, MSBs of the seed affect   
                // only MSBs of the array mt[].                        
                // 2002/01/09 modified by Makoto Matsumoto             
                mt[mti] &= 0xffffffffU;
                // for >32 bit machines
            }
        }

        // generates a random number on [0,0xffffffff]-interval
        protected override uint generateRandomInt32()
        {
            uint y;
            if (mti >= N)
            { /* generate N words at one time */
                int kk;

                if (mti == N + 1)   /* if init_genrand() has not been called, */
                    initGenerator(5489U); /* a default initial seed is used */

                for (kk = 0; kk < N - M; kk++)
                {
                    y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                    mt[kk] = mt[kk + M] ^ (y >> 1) ^ mag01[y & 0x1U];
                }
                for (; kk < N - 1; kk++)
                {
                    y = (mt[kk] & UPPER_MASK) | (mt[kk + 1] & LOWER_MASK);
                    mt[kk] = mt[kk + (M - N)] ^ (y >> 1) ^ mag01[y & 0x1U];
                }
                y = (mt[N - 1] & UPPER_MASK) | (mt[0] & LOWER_MASK);
                mt[N - 1] = mt[M - 1] ^ (y >> 1) ^ mag01[y & 0x1U];

                mti = 0;
            }

            y = mt[mti++];

            // Tempering
            y ^= (y >> 11);
            y ^= (y << 7) & 0x9d2c5680U;
            y ^= (y << 15) & 0xefc60000U;
            y ^= (y >> 18);

            return y;
        }
    }
}

