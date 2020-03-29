using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using DAL.Interfaces;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class TradesWriterAdoNet : IWriter<string>
    {

        public void Write(IEnumerable<string> collection)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=UserDB;Integrated Security=True;Pooling=False";
            string insertCommand = string.Format("Insert Into Trades" +
                                                 "(Countries, Trades_Number, Trades_Price) Values(@Countries, @Number, @Price)");

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            foreach (var tradeData in collection)
            {
                string[] paramsArray = tradeData.Split(',');
                using SqlCommand command = new SqlCommand(insertCommand, connection);
                command.Parameters.AddWithValue("@Countries", paramsArray[0]);
                command.Parameters.AddWithValue("@Number", int.Parse(paramsArray[1]));
                command.Parameters.AddWithValue("@Price",
                    double.Parse(paramsArray[2], CultureInfo.InvariantCulture));

                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
}
