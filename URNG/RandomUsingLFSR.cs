using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace URNG
{
    /// <summary>
    /// Algorithm for generating unique random numbers using a Linear Feedback Shift Register
    /// http://en.wikipedia.org/wiki/Linear_feedback_shift_register
    /// </summary>
    public class RandomUsingLFSR : RandomAbstract
    {
        const uint POLYMASK_32 = 0xB4BCD35C;
        const uint POLYMASK_31 = 0x7A5BC2E3;

        uint lfsr32, lfsr31;

        Random random;

        public RandomUsingLFSR()
        {
            random = new Random();
            lfsr32 = (uint)random.Next(Int32.MaxValue);
            lfsr31 = (uint)random.Next(Int32.MaxValue);
        }

        uint shiftLFSR(uint lfsr, uint polyMask)
        {
            uint feedback;

            feedback = lfsr & 1;

            lfsr >>= 1;

            if (feedback == 1) lfsr ^= polyMask;

            return lfsr;
        }

        // generates a random number on [0,0xffffffff]-interval
        protected override uint generateRandomInt32()
        {
            // compute and return next generator value
            lfsr32 = shiftLFSR(lfsr32, POLYMASK_32);
            return (shiftLFSR(lfsr32, POLYMASK_32) ^ shiftLFSR(lfsr31, POLYMASK_31) & 0xffffff);
        }
    }
}
