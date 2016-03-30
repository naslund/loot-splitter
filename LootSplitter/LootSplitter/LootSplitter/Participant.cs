using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootSplitter
{
    public class Participant
    {
        public string Name { get; set; }

        public decimal Waste { get; set; }
    }

    public class ParticipantOutput
    {
        public string Name { get; set; }
        public decimal Profit { get; set; }
    }
}
