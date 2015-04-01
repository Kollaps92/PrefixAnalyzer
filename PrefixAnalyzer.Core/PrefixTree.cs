using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace PrefixAnalyzer.Core
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
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
            TopWords.Sort();

            while (TopWords.Count() > 10)
            {
                TopWords.Remove(TopWords.Last());
            }
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
