using System;
using System.Diagnostics;
using System.IO;
using PrefixAnalyzer.Core;

namespace PrefixAnalyzer.Tests
{
    class Program
    {
        static void Main()
        {
            var watch = new Stopwatch();

            var tree = new PrefixTree();

            using (StreamReader sr = new StreamReader("C:\\TestFile.txt"))
            {
                int dictionaryLength;
                Int32.TryParse(sr.ReadLine(), out dictionaryLength);

                for (int i = 0; i < dictionaryLength; i++)
                {
                    string[] dictionaryPair = sr.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    int usageCount;
                    Int32.TryParse(dictionaryPair[1], out usageCount);

                    tree.AddToDictionary(new Word() { Value = dictionaryPair[0], Usages = usageCount });
                }

                int prefixCount;
                Int32.TryParse(sr.ReadLine(), out prefixCount);

                string[] prefixes = new string[prefixCount];

                for (int i = 0; i < prefixCount; i++)
                {
                    prefixes[i] = sr.ReadLine();
                }

                long secs = 0;
                watch.Start();
                foreach (var prefix in prefixes)
                {
                    var words = tree.GetTopUsages(prefix);

                    foreach (var word in words)
                    {
                        Console.WriteLine(word);
                    }

                    Console.WriteLine();
                    secs = watch.ElapsedMilliseconds;
                }

                Console.WriteLine(secs);
            }

            Console.WriteLine(watch.ElapsedMilliseconds);
        }
    }
}
