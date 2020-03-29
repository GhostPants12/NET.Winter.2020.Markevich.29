using System;
using System.Collections.Generic;
using System.IO;
using DAL.Interfaces;

namespace DAL
{
    public class FileReader : IReader<string>
    {
        public IEnumerable<string> ReadInfo(string path)
        {
            string buf;
            using (StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read)))
            {
                while (!sr.EndOfStream)
                {
                    buf = sr.ReadLine();
                    yield return buf;
                }
            }
        }
    }
}
