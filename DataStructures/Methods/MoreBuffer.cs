using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DataStructures.Methods
{
    public interface IBuffer<T> : IEnumerable<T>
    {
        void Write(T value);
        T Read();
        bool IsEmpty { get; }
        //Generic method:
        IEnumerable<TOut> As<TOut>();
        /**
         * Returns: IEnumerable<TOut>
         * Method name: As<TOut>() //TOut - type of output we are expecting
         * TOut type parameter is only available to this method
         * Invoking this method in DataStructures.Program
         */
    }

    public class Buffer<T> : IBuffer<T>
    {
        readonly protected Queue<T> _queue = new Queue<T>();

        public virtual void Write(T value)
        {
            _queue.Enqueue(value);
        }

        public virtual T Read()
        {
            return _queue.Dequeue();
        }

        public virtual bool IsEmpty
        {
            get { return _queue.Count == 0; }
        }

        public IEnumerable<TOut> As<TOut>()
        {
            var converter = TypeDescriptor.GetConverter(typeof (T)); //Return a converter for the provided type T
            foreach (var item in _queue)
            {
                var result = converter.ConvertTo(item, typeof (TOut)); //Convert each item from the queue (from type T to type TOut)
                yield return (TOut)result; //Builds an IEnumerable
            }
            //We have access to both: T (from the class) & TOut (from the method)
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _queue)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static class BufferHelpers
    {
        public static IEnumerable<TOut> CastTo<T, TOut>(this IBuffer<T> buffer)
        {
            foreach (object item in buffer)
            {
                yield return (TOut)item;
            }
        }

        public static IEnumerable<TOut> Convert<T, TOut>(this IBuffer<T> buffer, Converter<T, TOut> converter)
        {
            foreach (var item in buffer)
            {
                yield return converter(item);
            }
        }
    }

    public class CircularBuffer<T> : Buffer<T>
    {
        readonly int _capacity;

        public CircularBuffer()
            : this(capacity: 10)
        {
        }

        public CircularBuffer(int capacity)
        {
            _capacity = capacity;
        }

        public override void Write(T value)
        {
            base.Write(value);
            if (_queue.Count > _capacity)
            {
                var discard = _queue.Dequeue();
                OnItemDiscarded(discard, value);
            }
        }

        private void OnItemDiscarded(T discard, T value)
        {
            if (ItemDiscarded != null)
            {
                var args = new ItemDiscardedEventArgs<T>(discard, value);
                ItemDiscarded(this, args); //Invoking the delegate
            }
        }

        public event EventHandler<ItemDiscardedEventArgs<T>> ItemDiscarded; //Essentially a delegate

        public int Capacity
        {
            get { return _capacity; }
        }

        public bool IsFull
        {
            get { return _queue.Count >= _capacity; }
        }
    }

    public class ItemDiscardedEventArgs<T> : EventArgs
    {
        public ItemDiscardedEventArgs(T discard, T newitem)
        {
            ItemDiscarded = discard;
            NewItem = newitem;
        }

        public T ItemDiscarded { get; set; }
        public T NewItem { get; set; }
    }
}