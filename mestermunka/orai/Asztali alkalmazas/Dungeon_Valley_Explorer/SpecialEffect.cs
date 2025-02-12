using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Valley_Explorer
{
    public class SpecialEffect
    {
        public int Id { get; set; }
        public string SpecialEffectName { get; set; }
        public string Description { get; set; }
        public string Affect { get; set; }

        public SpecialEffect(string oneLine)
        {
            string[] linecutter = oneLine.Split('@');
            Id = Convert.ToInt32(linecutter[0]);
            SpecialEffectName = linecutter[1];
            Description = linecutter[2];
            Affect = linecutter[3];
        }

        public SpecialEffect()
        {

        }

        public static int PiercingBlade(int damage, Target target)
        {
            if (target.DEF > 0)
            {
                if (target.Guard == true)
                {
                    damage = (int)Math.Round((damage * 2 + target.DEF) * 0.5, 0);
                }
                else
                {
                    damage = damage + target.DEF;
                }
            }
            return damage;
        }
    }
}
