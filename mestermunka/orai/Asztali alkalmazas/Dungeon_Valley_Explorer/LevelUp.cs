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
                        LevelUpHunter(hero);
                        break;
                    case "Wizard":
                        LevelUpWizard(hero);
                        break;
                    case "Paladin":
                        LevelUpPaladin(hero);
                        break;
                    case "Bounty Hunter":
                        LevelUpBountyHunter(hero);
                        break;
                    case "Warlock":
                        LevelUpWarlock(hero);
                        break;
                    default: 
                        break;
                }
            }
            if (hero.Lvl == 2 && hero.Exp >= 1000)
            {
                switch (hero.heroClass)
                {
                    case "Fighter":
                        LevelUpFighter(hero);
                        break;
                    case "Hunter":
                        LevelUpHunter(hero);
                        break;
                    case "Wizard":
                        LevelUpWizard(hero);
                        break;
                    case "Paladin":
                        LevelUpPaladin(hero);
                        break;
                    case "Bounty Hunter":
                        LevelUpBountyHunter(hero);
                        break;
                    case "Warlock":
                        LevelUpWarlock(hero);
                        break;
                    default:
                        break;
                }
            }
            if (hero.Lvl == 3 && hero.Exp >= 4000)
            {
                switch (hero.heroClass)
                {
                    case "Fighter":
                        LevelUpFighter(hero);
                        break;
                    case "Hunter":
                        LevelUpHunter(hero);
                        break;
                    case "Wizard":
                        LevelUpWizard(hero);
                        break;
                    case "Paladin":
                        LevelUpPaladin(hero);
                        break;
                    case "Bounty Hunter":
                        LevelUpBountyHunter(hero);
                        break;
                    case "Warlock":
                        LevelUpWarlock(hero);
                        break;
                    default:
                        break;
                }
            }
            if (hero.Lvl == 4 && hero.Exp >= 10000)
            {
                switch (hero.heroClass)
                {
                    case "Fighter":
                        LevelUpFighter(hero);
                        break;
                    case "Hunter":
                        LevelUpHunter(hero);
                        break;
                    case "Wizard":
                        LevelUpWizard(hero);
                        break;
                    case "Paladin":
                        LevelUpPaladin(hero);
                        break;
                    case "Bounty Hunter":
                        LevelUpBountyHunter(hero);
                        break;
                    case "Warlock":
                        LevelUpWarlock(hero);
                        break;
                    default:
                        break;
                }
            }
            return hero;
        }

        //Fighter starts here ------------------------------------------------------------------------------------------

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
                default:
                    break;
            }
            return hero;
        }

        public static Hero LevelUpFighterToLevelTwo(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        public static Hero LevelUpFighterToLevelThree(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        public static Hero LevelUpFighterToLevelFour(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        public static Hero LevelUpFighterToLevelFive(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        //Fighter ends here --------------------------------------------------------------------------------------------

        //Hunter starts here -------------------------------------------------------------------------------------------

        public static Hero LevelUpHunter(Hero hero)
        {
            switch (hero.Lvl)
            {
                case 1:
                    LevelUpHunterToLevelTwo(hero);
                    break;
                case 2:
                    LevelUpHunterToLevelThree(hero);
                    break;
                case 3:
                    LevelUpHunterToLevelFour(hero);
                    break;
                case 4:
                    LevelUpHunterToLevelFive(hero);
                    break;
                default:
                    break;
            }
            return hero;
        }

        public static Hero LevelUpHunterToLevelTwo(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        public static Hero LevelUpHunterToLevelThree(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        public static Hero LevelUpHunterToLevelFour(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        public static Hero LevelUpHunterToLevelFive(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        //Hunter ends here ---------------------------------------------------------------------------------------------

        //Wizard starts here -------------------------------------------------------------------------------------------

        public static Hero LevelUpWizard(Hero hero)
        {
            switch (hero.Lvl)
            {
                case 1:
                    LevelUpWizardToLevelTwo(hero);
                    break;
                case 2:
                    LevelUpWizardToLevelThree(hero);
                    break;
                case 3:
                    LevelUpWizardToLevelFour(hero);
                    break;
                case 4:
                    LevelUpWizardToLevelFive(hero);
                    break;
                default:
                    break;
            }
            return hero;
        }

        public static Hero LevelUpWizardToLevelTwo(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        public static Hero LevelUpWizardToLevelThree(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        public static Hero LevelUpWizardToLevelFour(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        public static Hero LevelUpWizardToLevelFive(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        //Wizard ends here ---------------------------------------------------------------------------------------------

        //Paladin starts here ------------------------------------------------------------------------------------------

        public static Hero LevelUpPaladin(Hero hero)
        {
            switch (hero.Lvl)
            {
                case 1:
                    LevelUpPaladinToLevelTwo(hero);
                    break;
                case 2:
                    LevelUpPaladinToLevelThree(hero);
                    break;
                case 3:
                    LevelUpPaladinToLevelFour(hero);
                    break;
                case 4:
                    LevelUpPaladinToLevelFive(hero);
                    break;
                default:
                    break;
            }
            return hero;
        }

        public static Hero LevelUpPaladinToLevelTwo(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        public static Hero LevelUpPaladinToLevelThree(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        public static Hero LevelUpPaladinToLevelFour(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        public static Hero LevelUpPaladinToLevelFive(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        //Paladin ends here --------------------------------------------------------------------------------------------

        //Bounty Hunter starts here ------------------------------------------------------------------------------------

        public static Hero LevelUpBountyHunter(Hero hero)
        {
            switch (hero.Lvl)
            {
                case 1:
                    LevelUpBountyHunterToLevelTwo(hero);
                    break;
                case 2:
                    LevelUpBountyHunterToLevelThree(hero);
                    break;
                case 3:
                    LevelUpBountyHunterToLevelFour(hero);
                    break;
                case 4:
                    LevelUpBountyHunterToLevelFive(hero);
                    break;
                default:
                    break;
            }
            return hero;
        }

        public static Hero LevelUpBountyHunterToLevelTwo(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        public static Hero LevelUpBountyHunterToLevelThree(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        public static Hero LevelUpBountyHunterToLevelFour(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        public static Hero LevelUpBountyHunterToLevelFive(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        //Bounty Hunter ends here --------------------------------------------------------------------------------------

        //Warlock starts here ------------------------------------------------------------------------------------------

        public static Hero LevelUpWarlock(Hero hero)
        {
            switch (hero.Lvl)
            {
                case 1:
                    LevelUpWarlockToLevelTwo(hero);
                    break;
                case 2:
                    LevelUpWarlockToLevelThree(hero);
                    break;
                case 3:
                    LevelUpWarlockToLevelFour(hero);
                    break;
                case 4:
                    LevelUpWarlockToLevelFive(hero);
                    break;
                default:
                    break;
            }
            return hero;
        }

        public static Hero LevelUpWarlockToLevelTwo(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        public static Hero LevelUpWarlockToLevelThree(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        public static Hero LevelUpWarlockToLevelFour(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        public static Hero LevelUpWarlockToLevelFive(Hero hero)
        {
            hero.Lvl += 1;
            return hero;
        }

        //Warlock ends here --------------------------------------------------------------------------------------------
    }
}
