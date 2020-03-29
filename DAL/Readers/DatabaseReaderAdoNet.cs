using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using DAL.Interfaces;
using Microsoft.Data.SqlClient;

namespace DAL.Readers
{
    public class DatabaseReaderAdoNet : IDatabaseReader<Tuple<int, string, int, double>>
    {
        public IEnumerable<Tuple<int, string, int, double>> ReadFromDatabase()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=UserDB;Integrated Security=True;Pooling=False";
            string selectCommand = "SELECT Id, Countries, Trades_Number, Trades_Price FROM dbo.Trades";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command =
                    new SqlCommand(selectCommand, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            // Call Read before accessing data.
            while (reader.Read())
            {
                IDataRecord record = reader;
                yield return new Tuple<int, string, int, double>(int.Parse(record[0].ToString()), record[1].ToString(),
                    int.Parse(record[2].ToString()), double.Parse(record[3].ToString(), CultureInfo.InvariantCulture));
            }

            // Call Close when done reading.
            reader.Close();
        }
    }
}
