﻿using Microsoft.Extensions.DependencyInjection;
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
                //MibParsing
                .AddTransient<ICustomRegexRunner, CustomRegexRunner>()
                .AddTransient<IDependencyTreeCreator, DependencyTreeCreator>()
                .AddTransient<IFilesManager, FilesManager>()
                .AddTransient<IMibModelProvider, MibModelProvider>()
                .AddTransient<IMibPicker, MibPicker>()
                .AddTransient<IMibReader,MibReader>()
                .AddTransient<IObjectTypesParser, ObjectTypesParser>()
                .AddTransient<IMainObjectIdentifiersCreator, MainObjectIdentifiersCreator>()
                //main
                .AddSingleton<ISnmpRunner, SnmpRunner>()
                .BuildServiceProvider();

            //do the actual work here
            var snmpRunner = serviceProvider.GetService<ISnmpRunner>();

            snmpRunner.Run();
        }
    }
}