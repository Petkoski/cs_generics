using System;
//using DataStructures.Basic; //Only for CH1
//using DataStructures.Interface; //Only for CH3
using DataStructures.Methods; //Only for CH4

namespace DataStructures
{
    class Program
    {
        static void ConsoleWrite(double data)
        {
            Console.WriteLine(data);
        }

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

            //CH04-02:
            //var asInts = buffer.As<int>(); //Invoking generic method (defined in the class Buffer<T>)
            //TOut (for the generic method) = int

            //CH04-03:
            //Invoking extension methods (defined outside the class, in our case in: DataStructures.Methods.BufferExtensions)
            var asInts = buffer.AsEnumerableOf<double, int>();
            //buffer.Dump(); //Don't need to pass the generic type param (even that the method's signature is - void Dump<T>)

            //foreach (var item in asInts)
            //{
            //    Console.WriteLine(item);
            //}

            //CH04-04:
            //var consoleOut = new Printer<double>(ConsoleWrite);
            //buffer.DumpWithDelegate(consoleOut);

            //CH04-05:
            //Inside .NET there are 3 general purpose delegate types:

            //1) Action (always returns void)
            //Example: Action<double> - takes a double and returns void

            //First way (using named method):
            Action<double> print1 = ConsoleWrite;

            //Second way (anonymous delegate):
            Action<double> print2 = delegate (double data)
            {
                Console.WriteLine(data);
            };

            //Third way (easier anonymous delegate with lambda expression):
            Action<double> print3 = d => Console.WriteLine(d);

            buffer.DumpWithActionDelegate(print3);


            //2) Func (there is always at least 1 generic type argument to a Func because Func
            //always has to return a value. Last generic argument passed is the return type)
            //Example: Func<double, string> - takes a double and returns a string
            Func<double, double> square = d => d * d;
            Func<double, double, double> add = (x, y) => x + y;
            //Most of the LINQ operators (that operate on IEnumerable<T>) use Func of different types: WHERE, ORDERBY, SELECT, JOIN, GROUPBY.


            //3) Predicate (always returns a bool)
            //Example: Predicate<double> - takes a double and returns a bool
            Predicate<double> isLessThanTen = d => d < 10;

            //Console.WriteLine(isLessThanTen(square(add(3, 5))));

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
