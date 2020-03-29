using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    /// <summary>Interface for service classes that perform converting operation.</summary>
    public interface IDatabaseService
    {
        /// <summary>Converts the specified file to another file.</summary>
        /// <param name="sourcePath">The path of source file.</param>
        void Convert(string sourcePath);

        /// <summary>
        /// Gets the stat for the database.
        /// </summary>
        void GetStat();
    }
}
