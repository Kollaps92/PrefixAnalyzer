using System.ServiceModel;

namespace PrefixAnalyzer.Core
{
    [ServiceContract]
    public interface IStorage
    {
        [OperationContract]
        string[] GetTopUsages(string prefix);

        void AddToDictionary(Word word);
    }
}
