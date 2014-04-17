using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using URNG;

namespace URNGConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Input sequence length: ");
                var amount = Int32.Parse(Console.ReadLine());

                RandomUsingPermutation(amount);
            }
        }

        static void RandomUsingPermutation(int amount)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            RandomUsingPrimeCongruence randomPermutation = new RandomUsingPrimeCongruence();

            var numbers = new List<uint>();

            for (int i = 0; i < amount; i++)
            {
                numbers.Add(randomPermutation.Next());
            }

            sw.Stop();

            foreach (var number in numbers)
            {
                Console.Write(number.ToString() + ", ");
            }
            
            Console.WriteLine();

            Console.WriteLine("Execution time for " + amount.ToString() + " items: " + sw.ElapsedMilliseconds.ToString() + " milliseconds");

        }
    }
}
