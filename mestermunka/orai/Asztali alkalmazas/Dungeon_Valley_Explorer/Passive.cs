using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Valley_Explorer
{
    public class Passive
    {
        public int Id { get; set; }
        public string PassiveName { get; set; }
        public string Description { get; set; }
        public string Affect { get; set; }

        public Passive(string oneLine)
        {
            PassiveName = string.Empty;
            Description = string.Empty;
            Affect = string.Empty;
        }

        public Passive()
        {

        }

        public static int SwordProficiency(int damage)
        {
            damage = (int)Math.Round(damage * 1.2, 0);
            return damage;
        }
    }
}
