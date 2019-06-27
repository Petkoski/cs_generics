namespace DataStructures.Basic
{
    /**
     * Generic types allow code reuse with type safety
     * 
     * Definition:
     * CircularBuffer<T> = generic class
     * T = type parameter (defines the type of data a class works with, think of it as part of the class implementation).
     * T can be a placeholder whenever we need to provide a type (like when defining a field, local variable, method param, return method type, etc).
     * Name 'T' is arbitrary. That's a convention many people follow.
     * 
     * Usage:
     * When using this class (when creating objects) we need to provide *type argument(s)*
     * var buffer = new CircularBuffer<double>();
     * <double> = type argument (tell the compiler what data type will parameterize a class)
     * 
     * Now the CircularBuffer works with ANY type
     */
    public class CircularBuffer<T>
    {
        private readonly T[] _buffer;
        private int _start;
        private int _end;

        public CircularBuffer()
            : this(capacity: 10)
        {
        }

        public CircularBuffer(int capacity)
        {
            _buffer = new T[capacity + 1];
            _start = 0;
            _end = 0;
        }

        public void Write(T value)
        {
            _buffer[_end] = value;
            _end = (_end + 1) % _buffer.Length;
            if (_end == _start)
            {
                _start = (_start + 1) % _buffer.Length;
            }
        }

        public T Read()
        {
            T result = _buffer[_start];
            _start = (_start + 1) % _buffer.Length;
            return result;
        }

        public int Capacity
        {
            get { return _buffer.Length; }
        }

        public bool IsEmpty
        {
            get { return _end == _start; }
        }

        public bool IsFull
        {
            get { return (_end + 1) % _buffer.Length == _start; }
        }
    }
}
