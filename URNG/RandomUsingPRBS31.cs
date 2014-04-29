﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace URNG
{
    /// <summary>
    /// A slightly simpler version of the LSFR algorithm called the PseudoRadnom Bit Sequence.
    /// 
    /// This method is much slower than the regular LFSR implementation and is not recommended.
    /// </summary>
    public class RandomUsingPRBS31 : RandomBase
    {
        private uint seed;
        private uint state;
        private Random random;

        public RandomUsingPRBS31()
        {
            random = new Random();
            seed = (uint)random.Next(Int32.MaxValue);
            state = seed;
        }

        private uint prbs31(uint state)
        {
            uint feedback = ((state >> 30) ^ (state >> 27)) & 1;
            state = ((state << 1) | feedback) & 0xffffffff;
            return state;
        }
        
        // generates a random number on [0,0xffffffff]-interval
        protected override uint generateRandomInt32()
        {
            for (int i = 0; i < 31; ++i)
            {
                state = prbs31(state);
                if (state == seed)
                {
                    throw new Exception("Sequence ran out of elements");
                }
            }
            return state;
        }
    }
}
