using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using static System.Console;

namespace ReflectionExamples
{
    static class ReflectingAssemblies
    {
        const string _assemblyName = "Imanami.Foundation.dll";

        public static void Run()
        {
            DemoGettingTypes();
        }

        static void DemoGettingTypes()
        {
            // getting top level types from an assembly file
            var ass = Assembly.ReflectionOnlyLoadFrom(_assemblyName);
            WriteLine($"Loaded in reflection only context: {ass.ReflectionOnly}");
            Type[] types;
            try
            {
                types = ass.GetTypes();
            }
            catch(ReflectionTypeLoadException ex)
            {
                types = ex.Types.Where(t => t != null).ToArray();
            }
            int count = 0;
            foreach (var type in types)
            {
                WriteLine($"{type}");
                count++;
                if(count == 10)
                {
                    WriteLine($"... and {types.Length - count} other types");
                    break;
                }
            }
            
        }
    }
}
