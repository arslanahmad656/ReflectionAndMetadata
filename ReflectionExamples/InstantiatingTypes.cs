using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ReflectionExamples
{
    class InstantiatingTypes
    {
        delegate int Transformer(int x);

        int Square(int x) => x * x;
        static int Cube(int x) => x * x * x;

        public void Run()
        {
            //InstantiateWithActivator();
            //InstantiateWithCtorInfo();
            //CreateDelegates();
            //CreatingArrays();
            //CreatingGenerics();
            NullableTypeCheck();
        }

        void InstantiateWithActivator()
        {
            var @int = (int)Activator.CreateInstance(typeof(int));
            Console.WriteLine($"Dynamically instantiated int: {@int}");

            var date = Activator.CreateInstance(typeof(DateTime), 2009, 7, 9);
            Console.WriteLine($"Dynamically instantiated date: {date}");
        }

        void InstantiateWithCtorInfo()
        {
            var ctorInfo = typeof(DateTime).GetConstructor(new[] { typeof(int), typeof(int), typeof(int) });
            var date = ctorInfo.Invoke(new object[] { 2009, 7, 9 });
            Console.WriteLine($"Dynamically instantiated date: {date}");
        }

        void CreateDelegates()
        {
            var staticDelegate = Delegate.CreateDelegate(typeof(Transformer), this.GetType(), "Cube");
            var instanceDelegate = Delegate.CreateDelegate(typeof(Transformer), this, "Square");
            // Invoking dynamically:
            Console.WriteLine($"2*2*2 = {staticDelegate.DynamicInvoke(2)}");
            Console.WriteLine($"2*2 = {instanceDelegate.DynamicInvoke(2)}");
            // Invoking statically:
            Transformer c = (Transformer)staticDelegate;
            Transformer s = (Transformer)instanceDelegate;
            Console.WriteLine($"2*2*2 = {c(2)}");
            Console.WriteLine($"2*2 = {s(2)}");
        }

        void CreatingArrays()
        {
            /* We will create: 
             *  type1: int[], 
             *  type2: int[,], 
             *  type3: int[][], 
             *  type4: int[][,], 
             *  type5: int[,][]
            */
            var type1Arr = CreateArrayHelpers.CreateArrayOfInt();
            var type2Arr = CreateArrayHelpers.Create2dArrayOfInt();
            var type3Arr = CreateArrayHelpers.CreateArrayOfArrayOfInt();
            var type4Arr = CreateArrayHelpers.CreateArrayOf2dArrayOfInts();
            var type5Arr = CreateArrayHelpers.Create2dArrayOfArrayOfInt();
        }

        void CreatingGenerics()
        {
            var type1 = typeof(List<int>);  // can be instantiated directly
            var type2 = typeof(List<>); // can't be instantiated directly
            var type3 = typeof(Dictionary<int, string>); // can be instantiated directly
            var type4 = typeof(Dictionary<,>); // can't be instantiated directly

            Console.WriteLine($"{type1.FullName} is generic type: {type1.IsGenericType}");
            Console.WriteLine($"{type1.FullName} is unbound generic type: {type1.IsGenericTypeDefinition}");

            Console.WriteLine($"\n{type2.FullName} is generic type: {type2.IsGenericType}");
            Console.WriteLine($"{type2.FullName} is unbound generic type: {type2.IsGenericTypeDefinition}");

            Console.WriteLine($"Type parameters of {type3.FullName}:");
            foreach (var tp in type3.GetGenericArguments())
            {
                Console.WriteLine($"\t{tp.Name}");
            }

            Console.WriteLine($"Type parameters of {type4.FullName}:");
            foreach (var tp in type4.GetGenericArguments())
            {
                Console.WriteLine($"\t{tp.Name}");
            }

            var generic1 = (List<int>)Activator.CreateInstance(type1);
            generic1.Add(1);
            generic1.Add(2);
            generic1.Add(3);
            generic1.ForEach(i => Console.WriteLine($"Item: {i}"));

            try
            {
                var generic2 = Activator.CreateInstance(type2);
            }
            catch
            {
                Console.WriteLine("Could not create unbound generic type");
                var generic2 = (List<int>)Activator.CreateInstance(type2.MakeGenericType(typeof(int)));
                generic2.Add(10);
                generic2.Add(20);
                generic2.Add(30);
                generic2.ForEach(i => Console.WriteLine($"Item: {i}"));
            }

            var generic3 = (Dictionary<int, string>)Activator.CreateInstance(type3);
            generic3.Add(1, "Item 1");
            generic3.Add(2, "Item 2");
            generic3.Add(3, "Item 3");
            generic3.All(e =>
            {
                Console.WriteLine($"Key {e.Key}: {e.Value}");
                return true;
            });

            var generic4 = (Dictionary<string, int>)Activator.CreateInstance(type4.MakeGenericType(typeof(string), typeof(int)));
            generic4.Add("one", 1);
            generic4.Add("two", 2);
            generic4.Add("three", 3);
            generic4.All(e =>
            {
                Console.WriteLine($"Key {e.Key}: {e.Value}");
                return true;
            });
        }

        void NullableTypeCheck()
        {
            var type1 = typeof(int?);
            var type2 = typeof(int);
            Console.WriteLine($"Type {type1.FullName} is nullable: {IsNullable(type1)}");
            Console.WriteLine($"Type {type2.FullName} is nullable: {IsNullable(type2)}");

        }

        bool IsNullable(Type t)
        {
            // nullable type T? can be represented as Nullable<T>
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        private static class CreateArrayHelpers
        {
            public static int[] CreateArrayOfInt()
            {
                var type = typeof(int).MakeArrayType();
                var arr = (int[])Activator.CreateInstance(type, 3);
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = i + 1;
                }
                return (int[])arr;
            }

            public static int[,] Create2dArrayOfInt()
            {
                var type = typeof(int).MakeArrayType(2);
                var arr = (int[,])Activator.CreateInstance(type, 3, 4);
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        arr[i, j] = (i + 1) * (j + 1);
                    }
                }
                return arr;
            }

            public static int[][] CreateArrayOfArrayOfInt()
            {
                var type = typeof(int).MakeArrayType().MakeArrayType();
                var arr = (int[][])Activator.CreateInstance(type, 3);
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = (int[])Activator.CreateInstance(typeof(int).MakeArrayType(), i + 2);
                    for (int j = 0; j < arr[i].Length; j++)
                    {
                        arr[i][j] = (i + 1) * (j + 1);
                    }
                }
                return arr;
            }

            public static int[][,] CreateArrayOf2dArrayOfInts()
            {
                var type = typeof(int).MakeArrayType(2).MakeArrayType();
                var arr = (int[][,])Activator.CreateInstance(type, 3);
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = (int[,])Activator.CreateInstance(typeof(int).MakeArrayType(2), (i + 1) * 2, (i + 1) * 3);
                    for (int j = 0; j < arr[i].GetLength(0); j++)
                    {
                        for (int k = 0; k < arr[i].GetLength(1); k++)
                        {
                            arr[i][j, k] = (j + 1 + i) * (k + 1 + i);
                        }
                    }
                }
                return arr;
            }

            public static int[,][] Create2dArrayOfArrayOfInt()
            {
                var type = typeof(int).MakeArrayType().MakeArrayType(2);
                var arr = (int[,][])Activator.CreateInstance(type, 3, 4);
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        arr[i, j] = (int[])Activator.CreateInstance(typeof(int).MakeArrayType(), i + j + 1);
                        for (int k = 0; k < arr[i, j].Length; k++)
                        {
                            arr[i, j][k] = (i + 1) * (j + 1) * + (k + 1);
                        }
                    }
                }
                return arr;
            }
        }
    }
}
