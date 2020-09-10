using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise3
{
    class Program
    {
        public static IEnumerable<IEnumerable<string>>
                    OnlyBigCollections(List<IEnumerable<string>> toFilter)
        {
            Predicate<IEnumerable<string>> predicate =
            list => list.Count() > 5;

            return toFilter.FindAll(predicate);
        }
    }
}
