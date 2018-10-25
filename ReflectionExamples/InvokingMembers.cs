using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ReflectionExamples
{
    static class InvokingMembers
    {
        // Following types are to be demonstrated
        // static fields, instance methods, property, indexer, and miscs

        static (T1, T2) Echo<T1, T2>(T1 t1, T2 t2)
        {
            return (t1, t2);
        }

        public static void Run()
        {
            //DemoFields();
            //DemoInstanceMethods();
            //DemoProperty();
            //DemoAccessor();
            //DemoRefType();
            DemoGeneric();
        }

        static void DemoFields()
        {
            InitSample(out StringCollection s);
            var field = s.GetType().GetField("initialCapacity");
            Console.WriteLine($"Field information:");
            Console.WriteLine($" Type: {field.FieldType}");
            Console.WriteLine($" Is Literal: {field.IsLiteral}");
            Console.WriteLine($" Is Special Name: {field.IsSpecialName}");
            Console.WriteLine($" Is Static: {field.IsStatic}");
            try
            {
                Console.WriteLine(" Trying to set the value...");
                field.SetValue(s, 55);
            }
            catch(Exception ex)
            {
                Console.WriteLine(" Could not set.");
                Console.WriteLine($" {ex.GetType().Name}; {ex.Message}");
            }
            Console.WriteLine($" Value: {field.GetValue(s)}");
        }

        static void DemoInstanceMethods()
        {
            InitSample(out StringCollection s);
            var method = s.GetType().GetMethod("AddString");
            Console.WriteLine($" Is Abstract: {method.IsAbstract}");
            Console.WriteLine($" Is Constructor : {method.IsConstructor}");
            Console.WriteLine($" Return Type: {method.ReturnType.Name}");

            try
            {
                Console.WriteLine(" Trying to invoke the method");
                method.Invoke(s, new object[] { "sdfds", "dsfds" });
            }
            catch(Exception ex)
            {
                Console.WriteLine(" Could not invoke.");
                Console.WriteLine($" {ex.GetType().Name}; {ex.Message}");
            }

            try
            {
                Console.WriteLine(" Trying to invoke again");
                method.Invoke(s, new object[] { 12 });
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Could not invoke.");
                Console.WriteLine($" {ex.GetType().Name}; {ex.Message}");
            }
            method.Invoke(s, new[] { "dsfds" });
            Console.WriteLine(" Invoked successfully");
        }

        static void DemoProperty()
        {
            InitSample(out StringCollection s);
            var property = s.GetType().GetProperty("Count");
            try
            {
                Console.WriteLine(" Trying to set property");
                property.SetValue(s, "sss");
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Could not set.");
                Console.WriteLine($" {ex.GetType().Name}; {ex.Message}");
            }
            try
            {
                Console.WriteLine(" Trying to set property again");
                property.SetValue(s, 15);
                Console.WriteLine("Property set successfully....EVEN ITS SET ACCESSOR IS PRIVATE");
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Could not set.");
                Console.WriteLine($" {ex.GetType().Name}; {ex.Message}");
            }
            Console.WriteLine($" Value: {property.GetValue(s)}");
        }

        static void DemoAccessor()
        {
            InitSample(out StringCollection s);
            var indexer = s.GetType().GetProperty("Item");
            try
            {
                Console.WriteLine(" Trying to set using indexer");
                indexer.SetValue(s, 15);
                Console.WriteLine(" Indexer set successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Could not set.");
                Console.WriteLine($" {ex.GetType().Name}; {ex.Message}");
            }
            Console.WriteLine($" Value: {indexer.GetValue(s, new object[] { 0 })}");
        }

        static void DemoRefType()
        {
            // invoking int.TryParse("23", int out x) dynamically
            var @params = new[] { typeof(string), typeof(int).MakeByRefType() };
            var args = new object[] { "23", 0 };
            var method = typeof(int).GetMethod("TryParse", @params);
            var result = method.Invoke(null, args);
            Console.WriteLine($"Parsed: {result}; value: {args[1]}");
        }

        static void DemoGeneric()
        {
            var method = typeof(InvokingMembers).GetMethod("Echo", BindingFlags.NonPublic | BindingFlags.Static);
            Console.WriteLine($"Is Generic Method: {method.IsGenericMethod}");
            Console.WriteLine($"Contains Generic Parmeters: {method.ContainsGenericParameters}");
            (string t1, int t2) returnValue;
            try
            {
                Console.WriteLine("Trying to invoke the generic method...");
                returnValue = ((string, int))method.Invoke(null, new object[] { "some string", 12 });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! Could not invoke. {ex.GetType().Name}; {ex.Message}");
                Console.WriteLine("Making closed type");
                var closed = method.MakeGenericMethod(typeof(string), typeof(int));
                Console.WriteLine($"Is Generic Method: {closed.IsGenericMethod}");
                Console.WriteLine($"Contains Generic Parmeters: {closed.ContainsGenericParameters}");
                Console.WriteLine("Invoke the closed method...");
                returnValue = ((string, int))closed.Invoke(null, new object[] { "some string", 12 });
            }
            Console.WriteLine($"Return value: ({returnValue.t1}, {returnValue.t2})");
        }

        static void InitSample(out StringCollection sample)
        {
            sample = new StringCollection();
            sample.AddString("dsfdsfs");
            sample.AddString("dserewrewoiu");
        }
    }
}