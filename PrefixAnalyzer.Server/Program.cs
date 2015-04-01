using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using PrefixAnalyzer.Core;

namespace PrefixAnalyzer.Server
{
    public class PrefixAnalyzerServer
    {
        static void Main()
        {
            var prefixAnalyzerService = new ServiceHost(typeof(PrefixTree));
            prefixAnalyzerService.Open();
        }
    }
}