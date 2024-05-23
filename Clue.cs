using System;
using System.Diagnostics.Metrics;

namespace L4816
{
    public class Clue
	{
        public string ClueName { get; set; }
		public int ClueLength { get; set; }
        public Func<long, long> Formula1 { get; set; }
        public Func<long, long, long> Formula2 { get; set; }

        public Clue(string clue, int cluelength, Func<long, long> formula)
        {
            ClueName = clue;
            ClueLength = cluelength;
            Formula1 = formula;
        }
        public Clue(string clue, int cluelength, Func<long, long,long> formula)
        {
            ClueName = clue;
            ClueLength = cluelength;
            Formula2 = formula;
        }
    }
}

