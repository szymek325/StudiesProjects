using Microsoft.Extensions.DependencyInjection;
using SnmpAgent.Helpers.View;
using SnmpAgent.MainProgramLoop.Implementation;
using SnmpAgent.MainProgramLoop.Interface;
using SnmpAgent.MibParsing.Helpers.Implementation;
using SnmpAgent.MibParsing.Helpers.Interface;
using SnmpAgent.MibParsing.Implementation;
using SnmpAgent.MibParsing.Interface;

namespace SnmpAgent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                
                //MibParsing
                .AddTransient<ICustomRegexRunner, CustomRegexRunner>()
                .AddTransient<IDependencyTreeCreator, DependencyTreeCreator>()
                .AddTransient<IFilesManager, FilesManager>()
                .AddTransient<IMibModelProvider, MibModelProvider>()
                .AddTransient<IMibParsingRunner, MibParsingRunner>()
                .AddTransient<IMibPicker, MibPicker>()
                .AddTransient<IMibReader, MibReader>()
                .AddTransient<IObjectTypesParser, ObjectTypesParser>()
                .AddTransient<IMainObjectIdentifiersCreator, MainObjectIdentifiersCreator>()
                    //mib viewing
                .AddTransient<IDependencyTreeViewer, DependencyTreeViewer>()
                .AddTransient<INodeFinder,NodeFinder>()
                .AddTransient<IMibViewMenu,MibViewMenu>()
                //main
                .AddSingleton<ISnmpRunner, SnmpRunner>()
                .BuildServiceProvider();

            //do the actual work here
            var snmpRunner = serviceProvider.GetService<ISnmpRunner>();

            snmpRunner.Run();
        }
    }
}