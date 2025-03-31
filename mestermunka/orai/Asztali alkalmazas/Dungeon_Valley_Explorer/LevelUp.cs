using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Valley_Explorer
{
    static class LevelUp
    {
        public static Hero HeroLevelUp(Hero hero)
        {
            if (hero.Lvl == 1 && hero.Exp >= 100)
            {
                switch (hero.heroClass)
                {
                    case "Fighter":
                        LevelUpFighter(hero);
                        break;
                    case "Hunter":
                        break;
                    case "Wizard":
                        break;
                    case "Paladin":
                        break;
                    case "Bounty Hunter":
                        break;
                    case "Warlock":
                        break;
                }
            }
            if (hero.Lvl == 2 && hero.Exp >= 1000)
            {

            }
            if (hero.Lvl == 3 && hero.Exp >= 4000)
            {

            }
            if (hero.Lvl == 4 && hero.Exp >= 10000)
            {

            }
            return hero;
        }

        public static Hero LevelUpFighter(Hero hero)
        {
            switch (hero.Lvl)
            {
                case 1:
                    LevelUpFighterToLevelTwo(hero);
                    break;
                case 2:
                    LevelUpFighterToLevelThree(hero);
                    break;
                case 3:
                    LevelUpFighterToLevelFour(hero);
                    break;
                case 4:
                    LevelUpFighterToLevelFive(hero);
                    break;
            }
            return hero;
        }

        public static Hero LevelUpFighterToLevelTwo(Hero hero)
        {
            return hero;
        }

        public static Hero LevelUpFighterToLevelThree(Hero hero)
        {
            return hero;
        }

        public static Hero LevelUpFighterToLevelFour(Hero hero)
        {
            return hero;
        }

        public static Hero LevelUpFighterToLevelFive(Hero hero)
        {
            return hero;
        }
    }
}
