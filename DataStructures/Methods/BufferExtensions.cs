using System.Collections.Generic;
using System.ComponentModel;
using System;

/**
 * Generic method does not need to be part of a generic class or a generic interface.
 * It can be an instance method or static method on a NON-generic type. Furthermore,
 * we can use multiple generic type params <T, TOut>. Type parameters ARE PART of
 * the method signature (what the C# compiler uses to determine distinct method. It
 * uses the method name, types and number of parameters passed, as well as the 
 * number of generic type parameters to that method)
 * 
 * Examples below:
 */

namespace DataStructures.Methods
{
    public delegate void Printer<T>(T data);

    public static class BufferExtensions
    {
        public static IEnumerable<TOut> AsEnumerableOf<T, TOut>(this IBuffer<T> buffer) //Parameter that has 'this' keywoard in front of it (that's what EXTENSION methods are all about). We are making it available for all IBuffer<T> buffers.
        {
            var converter = TypeDescriptor.GetConverter(typeof(T)); //Return a converter for the provided type T
            foreach (var item in buffer)
            {
                var result = converter.ConvertTo(item, typeof(TOut)); //Convert each item from the queue (from type T to type TOut)
                yield return (TOut)result; //Builds an IEnumerable
            }
        }

        public static void Dump<T>(this IBuffer<T> buffer) //Extension method for IBuffer<T>
        {
            foreach (var item in buffer)
            {
                Console.WriteLine(item); //CH04-03
            }
        }

        //Delegates:
        public static void DumpWithDelegate<T>(this IBuffer<T> buffer, Printer<T> print)
        {
            foreach (var item in buffer)
            {
                print(item); //CH04-04
            }
        }

        public static void DumpWithActionDelegate<T>(this IBuffer<T> buffer, Action<T> print)
        {
            foreach (var item in buffer)
            {
                print(item); //CH04-05
            }
        }
    }
}
