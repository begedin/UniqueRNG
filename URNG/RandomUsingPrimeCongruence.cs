using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace URNG
{
    /// <summary>
    /// This class is actually able to generate a sequence of 2^32 unique random integers and it would repeat the same sequence after being called more than 2^32 times. This is done
    /// by using a one-to-one function of integers - a permutation.
    /// </summary>
    public class RandomUsingPrimeCongruence
    {
        private Random random;

        uint m_index;
        uint m_intermediateOffset;

        /// <summary>
        /// Creates a new instance (with a new seed) of a uniquely sequenced random number generator, using a prime permutation method.
        /// </summary>
        public RandomUsingPrimeCongruence()
        {
            random = new Random();
            
            m_index = permuteQPR(permuteQPR((uint)random.Next(Int32.MaxValue)) + 0x682f0161);
            m_intermediateOffset = permuteQPR(permuteQPR((uint)random.Next(Int32.MaxValue)) + 0x46790905);
        }


        /// <summary>
        /// From mathematics, when p is a prime number, x^2 % p has some unique properties. 
        /// Numbers produced this way are quadratic residues and all of them are unique as long as 2x less than p.
        /// For example, 
        /// for p=9, the quadratic residues for x = 1, 2, 3, 4, are all unique,
        /// for p=11, the quadratic residues for x = 1, 2, 3, 4, 5 are all unique, etc.
        /// 
        /// For primes that satisfy the condition p (congruent) 3 mod 4, the remaining integers fit perfectly into the remaining slots
        /// by using the expression p - x * x % p
        /// 
        /// This gives us a 1 to 1 function for all integers less than p, where p is any prime number satisfying p (congruent) 3 mod 4
        /// 
        /// The closest such number to 2^32 is 4294967291. The remaining five numbers can simply be mapped 1 to 1 to themselves.
        /// 
        /// The permutation function is not optimal. It tends to cluster numbers together. There is a solution for that, however.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        uint permuteQPR(uint x)
        {
            // prime number for permutation
            const uint prime = 4294967291;
            if (x >= prime)
                return x;  // The 5 integers out of range are mapped to themselves.
            uint residue = (uint)((ulong)(x * x) % prime);
            return (x <= prime / 2) ? residue : prime - residue;
        }

        /// <summary>
        /// Generates a random number. For the same seed (same instance), it will generate 2^32 unique integers before repeating any of them.
        /// </summary>
        /// <returns>Next number in a random 2^32 sequence.</returns>
        public uint Next()
        {
            return permuteQPR((permuteQPR(m_index++) + m_intermediateOffset) ^ 0x5bf03635);
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
