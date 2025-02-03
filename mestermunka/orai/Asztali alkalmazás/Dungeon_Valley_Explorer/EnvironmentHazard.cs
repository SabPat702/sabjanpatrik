using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Valley_Explorer
{
    public class EnvironmentHazard
    {
        public int Id { get; set; }
        public string EnvHazardName { get; set; }
        public int ATK { get; set; }
        public int CritChance { get; set; }
        public double CritDamage { get; set; }
        public string DamageType { get; set; }
        public List<SpecialEffect> SpecialEffect { get; set; }

        public EnvironmentHazard(string oneLine)
        {
            EnvHazardName = string.Empty;
            DamageType = string.Empty;
            SpecialEffect = new List<SpecialEffect>();
        }

        public EnvironmentHazard()
        {
            SpecialEffect = new List<SpecialEffect>();
        }
    }
}
