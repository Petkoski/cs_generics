using System;
//using DataStructures.Basic; //Only for CH1
//using DataStructures.Interface; //Only for CH3
using DataStructures.Methods; //Only for CH4

namespace DataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            /**
             * Chapter 01 - C# Generics - Why Generics
             */
            //var buffer = new CircularBuffer<double>(3); //Only for CH1

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

            //ProcessInput(buffer);
            //ProcessBuffer(buffer);


            /**
             * Chapter 03 - Generic Classes and Interfaces
             */

            //var buffer = new Buffer<double>(); //CH3 (no capacity)
            //var buffer = new CircularBuffer<double>(3); //CH3 (with capacity, default = 10)

            //ProcessInput(buffer);

            //foreach (var item in buffer) { Console.WriteLine(item); } //Not possible because buffer is not Enumerable (doesn't have public GetEnumerator() method)

            //To fix that, we should implement IEnumerable<T> in IBuffer<T>
            //IEnumerable<T> - nearly every generic collection implements it, all basic LINQ query operators are built on top of it, even strings support it
            //After implementation of IEnumerable<T>, now for-each works (in both Buffer<T> & CircularBuffer<T>):

            //ProcessInput(buffer);
            //foreach (var item in buffer)
            //{
            //    Console.WriteLine(item);
            //}
            //ProcessBuffer(buffer);


            /**
             * Chapter 04 - Generic Methods and Delegates
             */

            var buffer = new Buffer<double>(); //CH4
            //T (for the generic class) = double

            ProcessInput(buffer);

            var asInts = buffer.As<int>(); //Invoking generic method
            //TOut (for the generic method) = int

            foreach (var item in asInts)
            {
                Console.WriteLine(item);
            }

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
