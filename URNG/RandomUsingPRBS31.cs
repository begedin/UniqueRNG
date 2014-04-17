using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace URNG
{
    /// <summary>
    /// A slightly simpler version of the LSFR algorithm.
    /// 
    /// This would generate a randomly-seeming sequence of integers that is (2^31-1)/31 long.
    /// 
    /// This method is relatively slow.
    /// </summary>
    public class RandomUsingPRBS31
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

        public uint Next()
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

        public List<uint> Sequence(int size)
        {
            var numbers = new List<uint>();

            for (int i = 0; i < size; i++)
            {
                numbers.Add(Next());
            }

            return numbers;
        }
    }
}
