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


        //Basic Ai -----------------------------------------------------------------------------------------------------

        public static string BasicMonsterAction(string monsterAction, Monster activeMonster)
        {
            Random random = new Random();

            int value = random.Next(1,100);

            if (value <= 33)
            {
                monsterAction = "Block";
            }
            else if (value <= 66)
            {
                monsterAction = "Skill";
            }
            else if (value <= 100 && activeMonster.Magics.Contains(Initializer.magics.Where(x => x.MagicName == "None").Select(x => x).First()) == false)
            {
                monsterAction = "Magic";
            }
            else
            {
                monsterAction = "Default";
            }

            return monsterAction;
        }

        public static Skill BasicSkillChoosing(Monster activeMonster)
        {
            Skill chosenSkill = new Skill();

            List<Skill> choosableSkills = new List<Skill>();
            foreach (Skill skill in activeMonster.Skills)
            {
                if (activeMonster.SP - skill.SPCost >= 0)
                {
                    choosableSkills.Add(skill);
                }
            }

            Random random = new Random();

            chosenSkill = choosableSkills[random.Next(0,choosableSkills.Count-1)];

            return chosenSkill;
        }

        public static Magic BasicMagicChoosing(Monster activeMonster)
        {
            Magic chosenMagic = new Magic();


            List<Magic> choosableMagics = new List<Magic>();
            foreach (Magic magic in activeMonster.Magics)
            {
                if (activeMonster.MP - magic.MPCost >= 0)
                {
                    choosableMagics.Add(magic);
                }
            }

            Random random = new Random();

            chosenMagic = choosableMagics[random.Next(0, choosableMagics.Count - 1)];


            return chosenMagic;
        }

        public static List<Target> BasicMagicTargetChoosing(Magic magic, List<Monster> activeMonsters, List<Hero> party, Monster activeMonster)
        {
            List<Target> targets = new List<Target>();

            switch (magic.Range)
            {
                case "None":
                    targets.Add(new Target(activeMonster));
                    break;
                case "Melee":

                    break;
                case "Ranged":

                    break;
                case "Both":

                    break;
                case "Party":
                    foreach (Monster monster in activeMonsters)
                    {
                        targets.Add(new Target(monster));
                    }
                    break;
                case "Other":
                    if (activeMonsters.Count == 1)
                    {

                    }
                    else
                    {

                    }
                    break;
                default:
                    break;
            }

            return targets;
        }

        public static List<Target> BasicSkillTargetChoosing(Skill skill, List<Monster> activeMonsters, List<Hero> party, Monster activeMonster)
        {
            List<Target> targets = new List<Target>();

            switch (skill.Range)
            {
                case "None":
                    break;
                case "Melee":
                    break;
                case "Ranged":
                    break;
                case "Both":
                    break;
                case "Party":
                    break;
                case "Other":
                    break;
                default:
                    break;
            }

            return targets;
        }

        //Basic Ai -----------------------------------------------------------------------------------------------------
    }
}
