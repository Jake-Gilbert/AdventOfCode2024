using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayFive
{
    public class Rules
    {
        public Dictionary<int, List<int>> InstructionSets { get; set; } = new Dictionary<int, List<int>>();
        public List<string> Instructions { get; set; }  = new List<string>();
    }
}
