using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Valley_Explorer
{
    static class HeroStatCalculation
    {
        public static Hero HeroStatReCalculation (Hero hero)
        {
            hero.MaxHP = hero.InHP;
            hero.MaxMP = hero.InMP;
            hero.MaxSP = hero.InSP;
            hero.DEF = hero.InDEF;
            hero.MDEF = hero.MDEF;

            hero = HeroHPReCalculation(hero);
            hero = HeroMPReCalculation(hero);
            hero = HeroSPReCalculation(hero);
            hero = HeroDEFReCalculation(hero);
            hero = HeroMDEFReCalculation(hero);

            if (hero.HP > hero.MaxHP)
            {
                hero.HP = hero.MaxHP;
            }

            if (hero.MP > hero.MaxMP)
            {
                hero.MP = hero.MaxMP;
            }

            if (hero.SP > hero.MaxSP)
            {
                hero.SP = hero.MaxSP;
            }

            return hero;
        }

        public static Hero HeroHPReCalculation(Hero hero)
        {
            foreach (Passive passive in hero.Passives)
            {
                if (passive.Affect.Contains("HP Calculation"))
                {
                    switch (passive.PassiveName)
                    {
                        default:
                            break;
                    }
                }
            }

            foreach (BuffDebuff buffDebuff in hero.BuffsDebuffs)
            {
                if (buffDebuff.Affect.Contains("HP Calculation"))
                {
                    switch (buffDebuff.BuffDebuffName)
                    {
                        default:
                            break;
                    }
                }
            }

            foreach (Weapon weapon in hero.Weapons)
            {
                foreach (SpecialEffect specialEffect in weapon.SpecialEffects)
                {
                    if (specialEffect.Affect.Contains("HP Calculation"))
                    {
                        switch (specialEffect.SpecialEffectName)
                        {
                            default:
                                break;
                        }
                    }
                }
            }

            foreach (Armor armor in hero.Armors)
            {
                foreach (SpecialEffect specialEffect in armor.SpecialEffects)
                {
                    if (specialEffect.Affect.Contains("HP Calculation"))
                    {
                        switch (specialEffect.SpecialEffectName)
                        {
                            default:
                                break;
                        }
                    }
                }
            }

            return hero;
        }

        public static Hero HeroMPReCalculation(Hero hero)
        {
            foreach (Passive passive in hero.Passives)
            {
                if (passive.Affect.Contains("MP Calculation"))
                {
                    switch (passive.PassiveName)
                    {
                        default:
                            break;
                    }
                }
            }

            foreach (BuffDebuff buffDebuff in hero.BuffsDebuffs)
            {
                if (buffDebuff.Affect.Contains("MP Calculation"))
                {
                    switch (buffDebuff.BuffDebuffName)
                    {
                        default:
                            break;
                    }
                }
            }

            foreach (Weapon weapon in hero.Weapons)
            {
                foreach (SpecialEffect specialEffect in weapon.SpecialEffects)
                {
                    if (specialEffect.Affect.Contains("MP Calculation"))
                    {
                        switch (specialEffect.SpecialEffectName)
                        {
                            default:
                                break;
                        }
                    }
                }
            }

            foreach (Armor armor in hero.Armors)
            {
                foreach (SpecialEffect specialEffect in armor.SpecialEffects)
                {
                    if (specialEffect.Affect.Contains("MP Calculation"))
                    {
                        switch (specialEffect.SpecialEffectName)
                        {
                            default:
                                break;
                        }
                    }
                }
            }

            return hero;
        }

        public static Hero HeroSPReCalculation(Hero hero)
        {
            foreach (Passive passive in hero.Passives)
            {
                if (passive.Affect.Contains("SP Calculation"))
                {
                    switch (passive.PassiveName)
                    {
                        default:
                            break;
                    }
                }
            }

            foreach (BuffDebuff buffDebuff in hero.BuffsDebuffs)
            {
                if (buffDebuff.Affect.Contains("SP Calculation"))
                {
                    switch (buffDebuff.BuffDebuffName)
                    {
                        default:
                            break;
                    }
                }
            }

            foreach (Weapon weapon in hero.Weapons)
            {
                foreach (SpecialEffect specialEffect in weapon.SpecialEffects)
                {
                    if (specialEffect.Affect.Contains("SP Calculation"))
                    {
                        switch (specialEffect.SpecialEffectName)
                        {
                            default:
                                break;
                        }
                    }
                }
            }

            foreach (Armor armor in hero.Armors)
            {
                foreach (SpecialEffect specialEffect in armor.SpecialEffects)
                {
                    if (specialEffect.Affect.Contains("SP Calculation"))
                    {
                        switch (specialEffect.SpecialEffectName)
                        {
                            default:
                                break;
                        }
                    }
                }
            }

            return hero;
        }

        public static Hero HeroDEFReCalculation(Hero hero)
        {
            foreach (Passive passive in hero.Passives)
            {
                if (passive.Affect.Contains("DEF Calculation"))
                {
                    switch (passive.PassiveName)
                    {
                        default:
                            break;
                    }
                }
            }

            foreach (BuffDebuff buffDebuff in hero.BuffsDebuffs)
            {
                if (buffDebuff.Affect.Contains("DEF Calculation"))
                {
                    switch (buffDebuff.BuffDebuffName)
                    {
                        default:
                            break;
                    }
                }
            }

            foreach (Weapon weapon in hero.Weapons)
            {
                foreach (SpecialEffect specialEffect in weapon.SpecialEffects)
                {
                    if (specialEffect.Affect.Contains("DEF Calculation"))
                    {
                        switch (specialEffect.SpecialEffectName)
                        {
                            case "Shield":
                                hero.DEF += weapon.ATK;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            foreach (Armor armor in hero.Armors)
            {
                foreach (SpecialEffect specialEffect in armor.SpecialEffects)
                {
                    if (specialEffect.Affect.Contains("DEF Calculation"))
                    {
                        switch (specialEffect.SpecialEffectName)
                        {
                            default:
                                break;
                        }
                    }
                }
                hero.DEF += armor.DEF;
            }

            return hero;
        }

        public static Hero HeroMDEFReCalculation(Hero hero)
        {
            foreach (Passive passive in hero.Passives)
            {
                if (passive.Affect.Contains("MDEF Calculation"))
                {
                    switch (passive.PassiveName)
                    {
                        default:
                            break;
                    }
                }
            }

            foreach (BuffDebuff buffDebuff in hero.BuffsDebuffs)
            {
                if (buffDebuff.Affect.Contains("MDEF Calculation"))
                {
                    switch (buffDebuff.BuffDebuffName)
                    {
                        default:
                            break;
                    }
                }
            }

            foreach (Weapon weapon in hero.Weapons)
            {
                foreach (SpecialEffect specialEffect in weapon.SpecialEffects)
                {
                    if (specialEffect.Affect.Contains("MDEF Calculation"))
                    {
                        switch (specialEffect.SpecialEffectName)
                        {
                            default:
                                break;
                        }
                    }
                }
            }

            foreach (Armor armor in hero.Armors)
            {
                foreach (SpecialEffect specialEffect in armor.SpecialEffects)
                {
                    if (specialEffect.Affect.Contains("MDEF Calculation"))
                    {
                        switch (specialEffect.SpecialEffectName)
                        {
                            default:
                                break;
                        }
                    }
                }
                hero.MDEF += armor.MDEF;
            }

            return hero;
        }

    }
}
