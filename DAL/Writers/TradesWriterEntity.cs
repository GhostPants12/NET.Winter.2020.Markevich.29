using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Types;

namespace DAL
{
    public class TradesWriterEntity : IWriter<string>
    {
        public void Write(IEnumerable<string> collection)
        {
            using UserDBContext db = new UserDBContext();
            foreach (var str in collection)
            {
                string[] strArr = str.Split(',');
                db.Trades.Add(new Trades(){
                    Countries = strArr[0], 
                    TradesNumber = int.Parse(strArr[1]),
                    TradesPrice = double.Parse(strArr[2], CultureInfo.InvariantCulture)
                });
            }

            db.SaveChanges();
        }
    }
}
