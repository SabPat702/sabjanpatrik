using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Valley_Explorer
{
    public class Dungeon
    {
        public int Id { get; set; }
        public string DungeonName { get; set; }
        public int Length { get; set; }
        public string Description { get; set; }
        public int GoldReward { get; set; }
        public int ExpReward { get; set; }

        public Dungeon(string oneLine)
        {
            DungeonName = string.Empty;
            Description = string.Empty;
        }

        public Dungeon()
        {

        }
    }
}
