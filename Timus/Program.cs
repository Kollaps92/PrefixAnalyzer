using System;
using System.Collections.Generic;
using System.Linq;

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

            int ii = 0;
            foreach (var prefix in prefixes)
            {
                ii++;
                var words = tree.GetTopUsages(prefix);

                foreach (var word in words)
                {
                    Console.WriteLine(word);
                }

                if (ii != prefixCount)
                {
                    Console.WriteLine();
                }
            }
        }

        public interface IStorage
        {
            string[] GetTopUsages(string prefix);

            void AddToDictionary(Word word);
        }

        public class PrefixTree : IStorage
        {
            //private int _topWordsCount = 10;
            private readonly PrefixTreeNode _root;

            public PrefixTree()
            {
                _root = new PrefixTreeNode();
            }

            public string[] GetTopUsages(string prefix)
            {
                var current = _root;
                foreach (var character in prefix)
                {
                    if (!current.Childs.ContainsKey(character))
                        return new string[0];

                    current = current.Childs[character];
                }

                current.TopWords.Sort();

                while (current.TopWords.Count() > 10)
                {
                    current.TopWords.Remove(current.TopWords.Last());
                }

                return current.TopWords.Select(w => w.Value).ToArray();
            }

            public void AddToDictionary(Word word)
            {
                var current = _root;

                foreach (var character in word.Value)
                {
                    current.AddWordToTop(word);

                    if (!current.Childs.ContainsKey(character))
                        current.Childs.Add(character, new PrefixTreeNode());

                    current = current.Childs[character];
                }

                current.AddWordToTop(word);
            }
        }

        public class PrefixTreeNode
        {
            public Dictionary<char, PrefixTreeNode> Childs { get; private set; }
            public List<Word> TopWords { get; private set; }

            public PrefixTreeNode()
            {
                Childs = new Dictionary<char, PrefixTreeNode>();
                TopWords = new List<Word>();
            }

            public void AddWordToTop(Word word)
            {
                TopWords.Add(word);
            }
        }

        public class Word : IComparable<Word>
        {
            public string Value { get; set; }
            public int Usages { get; set; }

            public int CompareTo(Word other)
            {
                if (Usages > other.Usages)
                    return -1;
                if (Usages < other.Usages)
                    return 1;
                return string.CompareOrdinal(Value, other.Value);
            }
        }
    }
}
