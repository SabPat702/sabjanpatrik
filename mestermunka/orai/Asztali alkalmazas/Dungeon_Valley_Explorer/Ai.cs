using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Valley_Explorer
{
    public class Ai
    {
        public int Id { get; set; }
        public string AiName { get; set; }

        public Ai(string oneline)
        {
            string[] linecutter = oneline.Split('@');
            Id = Convert.ToInt32(linecutter[0]);
            AiName = linecutter[1];
        }

        public Ai()
        {
        
        }
    }
}
