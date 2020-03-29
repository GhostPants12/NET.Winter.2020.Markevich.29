using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces;
using Types;

namespace DAL.Readers
{
    public class DatabaseReaderEntity : IDatabaseReader<Tuple<int, string, int, double>>
    {
        public IEnumerable<Tuple<int, string, int, double>> ReadFromDatabase()
        {
            using UserDBContext db = new UserDBContext();
            foreach (var trade in db.Trades)
            {
                yield return new Tuple<int, string, int, double>(trade.Id, trade.Countries, trade.TradesNumber, trade.TradesPrice);
            }
        }
    }
}
