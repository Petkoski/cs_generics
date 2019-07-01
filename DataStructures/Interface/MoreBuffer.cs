using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Interface
{
    //Interfaces can also take generic type parameters
    //Anything that is an IBuffer<T>, has to be an IEnumerable<T> too (all our buffers will have GetEnumerator() method)
    //Implementing IEnumerable<T> will require two methods:
    //1) IEnumerator<T> GetEnumerator (returns a generic IEnumerator<T>)
    //2) IEnumerator GetEnumerator() [because anything that is IEnumerable<T> is also an IEnumerable (defined: public interface IEnumerable<out T> : IEnumerable in System.Collections.Generic)]
    public interface IBuffer<T> : IEnumerable<T>
    {
        void Write(T value);
        T Read(); //We want things to be strongly typed
        bool IsEmpty { get; }
    }

    //Let's implement a buffer that doesn't throw away info (like the CircularBuffer [defined in DataStructures.Basic.CircularBuffer<T>] does)
    //It will function as a BASE CLASS
    public class Buffer<T> : IBuffer<T> 
    {
        readonly protected Queue<T> _queue = new Queue<T>();
        //protected - can be accessed (by anyone who derives from this)

        public virtual void Write(T value)
        {
            _queue.Enqueue(value);    
        }
        //virtual - Anyone who derives from this, can TWEAK the behavior slightly

        public virtual T Read()
        {
            return _queue.Dequeue();
        }
        //virtual

        public virtual bool IsEmpty
        {
            get { return _queue.Count == 0; } //Returns true if the count is 0
        }
        //virtual 

        public IEnumerator<T> GetEnumerator()
        {
            //return _queue.GetEnumerator(); //Very simple implementation

            //Our own:
            foreach (var item in _queue)
            {
                // ...
                yield return item; //Magic C# syntax that will auto build the state machine to implement the IEnumerator<T>, it allows these items to be handed back one-at-a-time in a lazy manner
            }
        }

        //public IEnumerator GetEnumerator() //Not possible, C# compiler sees: Type 'Buffer<T>' already defines a member called 'GetEnumerator' with the same parameter types
        //The trick used to implement this method:
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator(); //Calls the previous version from above: public IEnumerator<T> GetEnumerator()
        }
    }

    //A new CircularBuffer implementation (for CH3)
    //CircularBuffer<T> implements Buffer<T> (which implements IBuffer<T> interface)
    //It is pretty much a special case for the Buffer (it has a fixed size capacity)
    public class CircularBuffer<T> : Buffer<T> //T must match in both CircularBuffer<T> & Buffer<T>
    {
        readonly int _capacity;

        public CircularBuffer()
            : this(capacity: 10)
        {
        }

        public CircularBuffer(int capacity = 10)
        {
            _capacity = capacity;
        }

        public override void Write(T value)
        {
            base.Write(value); //Base class (Buffer<T>) writes a value into its Queue<T>
            if (_queue.Count > _capacity) //Little post processing to see if capacity is exceeded
            {
                //_queue is defined in the base class
                _queue.Dequeue();
            }
        }
        //override - overriding the Write() method

        public int Capacity
        {
            get { return _capacity; }
        }

        //Should all buffers have an IsFull property? Is it a prop that we should make member of the
        //IBuffer<T> interface so to everyone (that derives) has to implement it?
        //No, because the other buffer (Buffer<T>) is never full.
        //So, we will define that method JUST here (CircularBuffer<T>), but not part of the interface definition.
        public bool IsFull
        {
            get { return _queue.Count >= _capacity; } //Returns true if _queue.Count >= _capacity
        }
    }
}
