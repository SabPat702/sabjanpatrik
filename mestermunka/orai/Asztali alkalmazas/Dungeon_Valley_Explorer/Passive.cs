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
            string[] linecutter = oneLine.Split('@');
            Id = Convert.ToInt32(linecutter[0]);
            PassiveName = linecutter[1];
            Description = linecutter[2];
            Affect = linecutter[3];
        }

        public Passive()
        {

        }

        //Sleep Passives start here ------------------------------------------------------------------------------------



        //Sleep Passives end here --------------------------------------------------------------------------------------

        //Combat Passives start here -----------------------------------------------------------------------------------

        public static int SwordProficiency(int damage)
        {
            damage = (int)Math.Round(damage * 1.2, 0);
            return damage;
        }

        //Combat Passives end here -------------------------------------------------------------------------------------
    }
}
