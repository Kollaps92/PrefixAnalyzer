using System;
using PrefixAnalyzer.Core;

namespace PrefixAnalyzer
{
    class Program
    {
        public static void Main()
        {
            var tree = new PrefixTree();

            int dictionaryLength;
            Int32.TryParse(Console.ReadLine(), out dictionaryLength);

            for (int i = 0; i < dictionaryLength; i++)
            {
                string[] dictionaryPair = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                int usageCount;
                Int32.TryParse(dictionaryPair[1], out usageCount);

                tree.AddToDictionary(new Word() { Value = dictionaryPair[0], Usages = usageCount });
            }

            int prefixCount;
            Int32.TryParse(Console.ReadLine(), out prefixCount);

            string[] prefixes = new string[prefixCount];

            for (int i = 0; i < prefixCount; i++)
            {
                prefixes[i] = Console.ReadLine();
            }

            foreach (var prefix in prefixes)
            {
                var words = tree.GetTopUsages(prefix);

                foreach (var word in words)
                {
                    Console.WriteLine(word);
                }

                Console.WriteLine();
            }
        }
    }
}
