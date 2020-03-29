using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace DAL.Interfaces
{
    /// <summary>Interface for writer class.</summary>
    /// <typeparam name="T">Type of value to write.</typeparam>
    public interface IWriter<T>
    {
        /// <summary>Writes the sequence of T.</summary>
        /// <param name="collection">The collection.</param>
        void Write(IEnumerable<T> collection);
    }
}
