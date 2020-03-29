using System;
using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DAL.Readers;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyResolver
{
    public class DependencyResolver
    {
        public IServiceProvider CreateEntityDataBaseConverter()
        {
            return new ServiceCollection()
                .AddSingleton<IDatabaseService, DatabaseConverterService>()
                .AddSingleton<IReader<string>, FileReader>()
                .AddSingleton<IWriter<string>, TradesWriterEntity>()
                .AddSingleton<IValidator<string>, TradeValidator>()
                .AddSingleton<IDatabaseReader<Tuple<int, string, int, double>>, DatabaseReaderAdoNet>()
                .BuildServiceProvider();
        }

        public IServiceProvider CreateAdoNetDataBaseConverter()
        {
            return new ServiceCollection()
                .AddSingleton<IDatabaseService, DatabaseConverterService>()
                .AddSingleton<IReader<string>, FileReader>()
                .AddSingleton<IWriter<string>, TradesWriterAdoNet>()
                .AddSingleton<IValidator<string>, TradeValidator>()
                .AddSingleton<IDatabaseReader<Tuple<int, string, int, double>>, DatabaseReaderEntity>()
                .BuildServiceProvider();
        }
    }
}
