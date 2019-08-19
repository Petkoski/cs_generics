using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tips
{
    class Program
    {
        static void Main(string[] args)
        {
            //#1:
            //Using generics:
            var aGeneric = new ItemGeneric<int>();
            var bGeneric = new ItemGeneric<int>();
            var cGeneric = new ItemGeneric<string>();
            Console.WriteLine(ItemGeneric<int>.InstanceCount); //Output: 2. Only instances of ItemGeneric<int> share the static field.
            Console.WriteLine(ItemGeneric<string>.InstanceCount); //Output: 1

            //#2:
            //Using base class:
            var list = new List<Item>();

            //Adding different type items to a list
            var a = new Item<int>();
            var b = new Item<int>();
            var c = new Item<string>();

            list.Add(a);
            list.Add(b);
            list.Add(c);

            //Accessing static field of the base class
            Console.WriteLine(Item.InstanceCount); //Output: 3

            Console.Read();
        }
    }

    public class Item<T> : Item
    {
        
    }

    public class Item
    {
        public Item()
        {
            InstanceCount += 1;
        }

        public static int InstanceCount; //Static fields are shared accross all instances of Item
    }

    public class ItemGeneric<T>
    {
        public ItemGeneric()
        {
            InstanceCount += 1;
        }

        public static int InstanceCount;
    }
}
