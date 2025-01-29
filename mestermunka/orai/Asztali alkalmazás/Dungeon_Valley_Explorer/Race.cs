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
            Description = string.Empty;
            RaceName = string.Empty;
            Fatal = new List<string>();
            Weak = new List<string>();
            Resist = new List<string>();
            Endure = new List<string>();
            Nulls = new List<string>();
        }

        public Race()
        {

        }
    }
}
