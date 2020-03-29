using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IDatabaseReader<T>
    {
        IEnumerable<T> ReadFromDatabase();
    }
}
