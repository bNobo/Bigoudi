using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigoudi
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> elements, Action<T> execute)
        {
            foreach (var element in elements) 
            { 
                execute(element); 
            }
        }
    }
}
