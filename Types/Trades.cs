using System;
using System.Collections.Generic;

namespace Types
{
    public partial class Trades
    {
        public int Id { get; set; }
        public string Countries { get; set; }
        public int TradesNumber { get; set; }
        public double TradesPrice { get; set; }
    }
}
