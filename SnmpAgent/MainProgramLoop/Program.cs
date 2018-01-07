using Microsoft.Extensions.DependencyInjection;
using SnmpAgent.MibParsing.Implementation;
using SnmpAgent.MibParsing.Interface;

namespace SnmpAgent.MainProgramLoop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddTransient<IMibPicker, MibPicker>()
                .AddTransient<IMibReader,MibReader>()
                .AddTransient<IMibModelProvider, MibModelProvider>()
                .AddTransient<ICustomRegexRunner, CustomRegexRunner>()
                .AddTransient<IOidCreator, OidCreator>()
                .AddTransient<IDependencyTreeCreator, DependencyTreeCreator>()
                .AddTransient<IFilesManager,FilesManager>()
                .AddSingleton<ISnmpRunner, SnmpRunner>()
                .BuildServiceProvider();

            //do the actual work here
            var snmpRunner = serviceProvider.GetService<ISnmpRunner>();

            snmpRunner.Run();
        }
    }
}