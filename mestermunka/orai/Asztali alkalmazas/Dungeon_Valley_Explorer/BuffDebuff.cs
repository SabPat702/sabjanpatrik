using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Valley_Explorer
{
    public class BuffDebuff
    {
        public int Id { get; set; }
        public string BuffDebuffName { get; set; }
        public int Timer { get; set; }
        public string Description { get; set; }
        public string Affect { get; set; }

        public BuffDebuff(string oneLine)
        {
            Description = string.Empty;
            BuffDebuffName = string.Empty;
        }

        public BuffDebuff()
        {

        }

        public static int DamageUp(int damage)
        {
            damage = (int)Math.Round(damage * 1.2, 0);
            return damage;
        }
    }
}
