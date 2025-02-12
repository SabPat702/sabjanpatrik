using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Valley_Explorer
{
    public class Race
    {
        public int Id { get; set; }
        public string RaceName { get; set; }
        public string Description { get; set; }
        public List<string> Fatal { get; set; }
        public List<string> Weak { get; set; }
        public List<string> Resist { get; set; }
        public List<string> Endure { get; set; }
        public List<string> Nulls { get; set; }

        public Race(string oneLine)
        {
            string[] linecutter = oneLine.Split('@');
            Id = Convert.ToInt32(linecutter[0]);
            RaceName = linecutter[1];
            Description = linecutter[2];
            string[] fatalcutter = linecutter[3].Split(',');
            foreach (string fatal in fatalcutter)
            {
                Fatal.Add(fatal);
            }
            string[] weakcutter = linecutter[4].Split(',');
            foreach (string weak in weakcutter)
            {
                Weak.Add(weak);
            }
            string[] resistcutter = linecutter[5].Split(',');
            foreach (string resist in resistcutter)
            {
                Resist.Add(resist);
            }
            string[] endurecutter = linecutter[6].Split(',');
            foreach (string endure in endurecutter)
            {
                Endure.Add(endure);
            }
            string[] nullscutter = linecutter[7].Split(',');
            foreach (string nulls in nullscutter)
            {
                Nulls.Add(nulls);
            }
        }

        public Race()
        {
            Fatal = new List<string>();
            Weak = new List<string>();
            Resist = new List<string>();
            Endure = new List<string>();
            Nulls = new List<string>();
        }
    }
}
