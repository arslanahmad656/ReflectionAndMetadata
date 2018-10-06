using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReflectionExamples
{
    static class Basics
    {
        public static void Run()
        {
            //ArrayAndSimples();
            //NamesAndFullNames();
            //BaseTypesAndInterfaces();
            VerifyTypeRelations();
        }

        static void ArrayAndSimples()
        {
            var t = typeof(int).MakeArrayType();
            Console.WriteLine($"typeof(int).MakeArrayType(): {t.Name}");
            t = typeof(int).MakeArrayType().MakeArrayType();
            Console.WriteLine($"typeof(int).MakeArrayType().MakeArrayType(): {t.Name}, Rank: {t.GetArrayRank()}");
        }

        static void NamesAndFullNames()
        {
            Console.WriteLine("General Cases:\n");
            var type = typeof(Console);
            Console.WriteLine($"typeof(Console); Name: {type.Name}, FullName: {type.FullName}");

            type = typeof(int);
            Console.WriteLine($"typeof(int); Name: {type.Name}, FullName: {type.FullName}");

            Console.WriteLine("\nNested Types:");
            type = typeof(Environment.SpecialFolder);
            Console.WriteLine($"typeof(Environment.SpecialFolder); Name: {type.Name}, FullName: {type.FullName}");

            Console.WriteLine("\nUnbound Generic Types:");
            type = typeof(Dictionary<,>);
            Console.WriteLine($"typeof(Dictionary<,>); Name: {type.Name}, FullName: {type.FullName}");

            Console.WriteLine("\nClosed Generic Types:");
            type = typeof(Dictionary<int, double>);
            Console.WriteLine($"typeof(Dictionary<int, double>); Name: {type.Name}, FullName: {type.FullName}");

            type = typeof(Dictionary<Dictionary<string, System.Reflection.Assembly>, Dictionary<int, double>>);
            Console.WriteLine($"typeof(Dictionary<Dictionary<string, System.Reflection.Assembly>, Dictionary<int, double>>); Name: {type.Name}, FullName: {type.FullName}");
        }

        static void BaseTypesAndInterfaces()
        {
            var type = typeof(string);
            Console.WriteLine("Type: string");
            var baseType = type.BaseType;
            Console.WriteLine($"Base type: {baseType}");

            Console.WriteLine("Interfaces:");
            var interfaces = type.GetInterfaces();
            foreach (var i in interfaces)
            {
                Console.WriteLine($"  {i.Name}");
            }
        }

        public static void VerifyTypeRelations()
        {
            var obj = "This is a sample string.";
            var objType = obj.GetType();
            var testType1 = typeof(string);
            var testType2 = typeof(object);
            var testType3 = typeof(StringBuilder);
            var testType4 = typeof(IComparable);
            var testType5 = typeof(IDisposable);
            Console.WriteLine($"Type of test object {nameof(obj)}: {objType.Name}");

            Console.WriteLine("\nDemonstrating IsInstanceOfType");
            Console.WriteLine($"Testing with {testType1.Name}: {testType1.IsInstanceOfType(obj)}");
            Console.WriteLine($"Testing with {testType2.Name}: {testType2.IsInstanceOfType(obj)}");
            Console.WriteLine($"Testing with {testType3.Name}: {testType3.IsInstanceOfType(obj)}");
            Console.WriteLine($"Testing with {testType4.Name}: {testType4.IsInstanceOfType(obj)}");
            Console.WriteLine($"Testing with {testType5.Name}: {testType5.IsInstanceOfType(obj)}");

            Console.WriteLine("\nDemonstrating IsAssignableFrom");
            Console.WriteLine($"Testing with {testType1.Name}: {testType1.IsAssignableFrom(objType)}");
            Console.WriteLine($"Testing with {testType2.Name}: {testType2.IsAssignableFrom(objType)}");
            Console.WriteLine($"Testing with {testType3.Name}: {testType3.IsAssignableFrom(objType)}");
            Console.WriteLine($"Testing with {testType4.Name}: {testType4.IsAssignableFrom(objType)}");
            Console.WriteLine($"Testing with {testType5.Name}: {testType5.IsAssignableFrom(objType)}");

            Console.WriteLine("\nDemonstrating IsSubclassOf:");
            Console.WriteLine($"Testing with {testType1.Name}: {objType.IsSubclassOf(testType1)}");
            Console.WriteLine($"Testing with {testType2.Name}: {objType.IsSubclassOf(testType2)}");
            Console.WriteLine($"Testing with {testType3.Name}: {objType.IsSubclassOf(testType3)}");
            Console.WriteLine($"Testing with {testType4.Name}: {objType.IsSubclassOf(testType4)}");
            Console.WriteLine($"Testing with {testType5.Name}: {objType.IsSubclassOf(testType5)}");
        }
    }
}
