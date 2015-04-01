using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefixAnalyzer.Core
{
    public class Initialize
    {
        public static IStorage GetStorage()
        {
            return new PrefixTree();
        }
    }
}
