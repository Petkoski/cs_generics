using System;
//using DataStructures.Basic; //Only for CH1
using DataStructures.Interface;

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            //Chapter 01:
            //var buffer = new CircularBuffer<double>(3); //Only for CH1

            //Chapter 03:
            //var buffer = new Buffer<double>(); //CH3 (no capacity)
            var buffer = new CircularBuffer<double>(3); //CH3 (with capacity, default = 10)

            //var b1 = new CircularBuffer<string>();
            //var b2 = new CircularBuffer<string>();
            //var b3 = new CircularBuffer<int>();
            //var b4 = new CircularBuffer<object>();
            //Console.WriteLine(b1.GetType() == b2.GetType()); //True
            //Console.WriteLine(b1.GetType() == b3.GetType()); //False
            //Console.WriteLine(b1.GetType() == b4.GetType()); //False
            /*
             * ^ b4 (CircularBuffer of <object>) will allow you to add any type of object to the buffer (strings, doubles, ints),
             * but for value types like doubles - the values would be BOXED (because will be working with the values through an
             * 'object' reference). A CircularBuffer <double> will NOT BOX the values, it will work with them as doubles, and
             * that's better for perfomance.
             */

            ProcessInput(buffer);
            ProcessBuffer(buffer);

            Console.Read();
        }

        //private static void ProcessBuffer(CircularBuffer<double> buffer) //Using generic class (CH1)
        private static void ProcessBuffer(IBuffer<double> buffer) //Using generic interface (CH3)
        {
            //Display members:
            Console.WriteLine("Buffer: ");
            while (!buffer.IsEmpty)
            {
                Console.WriteLine("\t" + buffer.Read());
            }

            //Display a sum:
            //var sum = 0.0;
            //Console.WriteLine("Buffer: ");
            //while (!buffer.IsEmpty)
            //{
            //    sum += buffer.Read();
            //}
            //Console.WriteLine(sum);
        }

        //private static void ProcessInput(CircularBuffer<double> buffer) //Using generic class (CH1)
        private static void ProcessInput(IBuffer<double> buffer) //Using generic interface (CH3)
        {
            while (true)
            {
                var value = 0.0;
                var input = Console.ReadLine();

                if (double.TryParse(input, out value))
                {
                    buffer.Write(value);
                    continue;
                }
                break;
            }
        }
    }
}
