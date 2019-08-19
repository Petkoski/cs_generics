using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tips
{
    class MathProblems
    {
        static void DoIt(string[] args)
        {
            var numbers = new double[] { 1, 2, 3, 4, 5, 6 };
            var result = SampledAverage(numbers);
            Console.WriteLine(result);
        }

        /**
         * When writing math code (that uses math operations, like SampledAverage())
         * you have to overload methods with different types of parameters (double in
         * our case, write separate method for ints / floats / etc., if required). 
         * Don't use generic type parameter, because C# compiler doesn't know if
         * the generic type T has those operators (+=, divison, etc.)
         * When it comes to operators like division, multip., subtr. accross the built
         * in types (like int / double / float), the best solution is NOT generic type
         * paremeters, but overloaded methods.
         */
        private static double SampledAverage(double[] numbers)
        {
            var count = 0;
            var sum = 0.0;
            for (int i = 0; i < numbers.Length; i += 2)
            {
                sum += numbers[i];
                count += 1;
            }
            return sum / count;
        }
    }
}
