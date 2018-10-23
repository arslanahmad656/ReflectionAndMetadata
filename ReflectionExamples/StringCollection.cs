using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionExamples
{
    class StringCollection
    {
        private List<string> data;
        public int Count { get; private set; }

        public const int initialCapacity = 5;  // implicitly static

        public static int GetInitialCapacity()
        {
            return initialCapacity;
        }

        public StringCollection()
        {
            data = new List<string>(initialCapacity);
        }

        public void AddString(string str)
        {
            data.Add(str);
        }

        public string this[int i]
        {
            get => data[i];
        }
    }
}
