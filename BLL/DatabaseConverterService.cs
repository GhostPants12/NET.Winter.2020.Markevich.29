using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace BLL
{
    public class DatabaseConverterService : IDatabaseService
    {
        private readonly IValidator<string> validator;
        private readonly IReader<string> reader;
        private readonly IWriter<string> writer;
        private IDatabaseReader<Tuple<int, string, int, double>> dbReader;

        public DatabaseConverterService(IValidator<string> validator, IReader<string> reader,
            IWriter<string> writer, IDatabaseReader<Tuple<int, string, int, double>> dbReader)
        {
            this.validator = validator;
            this.reader = reader;
            this.writer = writer;
            this.dbReader = dbReader;
        }

        public void Convert(string sourcePath)
        {
            List<string> stringsToWrite =  new List<string>(reader.ReadInfo(sourcePath));
            this.writer.Write(stringsToWrite.Where((s => this.validator.Validate(s))));
        }

        public void GetStat()
        {
            List<Tuple<int, string, int, double>> database = 
                new List<Tuple<int, string, int, double>>(dbReader.ReadFromDatabase());dbReader.ReadFromDatabase();
            Console.WriteLine($"Elements count: {database.Count}");
            Console.WriteLine($"Max Id: {database.Max((tuple => tuple.Item1))}");
            Console.WriteLine($"Currencies with most trades: {database.First((tuple => tuple.Item3 == database.Max((tuple1 => tuple1.Item3)))).Item2}");
            Console.WriteLine($"Currencies with highest trade price: {database.First(tuple => tuple.Item4 == database.Max((tuple1 => tuple1.Item4))).Item2}");
        }
    }
}
