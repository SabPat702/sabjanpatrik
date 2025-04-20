using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Dungeon_Valley_Explorer
{
    internal class DamageCalculator
    {
        static List<string> physicalDamageTypes = new List<string> { "Blunt", "Pierce", "Slash" };
        static List<string> magicalDamageTypes = new List<string> { "Fire" };
        static Random random = new Random();

        public static List<int> PreDamageCalculation(List<Target> targets, DamageSource damageSource)
        {
            List<int> damages = new List<int>();

            foreach (SpecialEffect specialEffect in damageSource.SpecialEffects)
            {
                if (specialEffect.Affect.Contains("Pre Damage Calculation"))
                {
                    switch (specialEffect.SpecialEffectName)
                    {
                        default:
                            break;
                    }
                }
            }

            foreach (BuffDebuff buffDebuff in damageSource.BuffsDebuffs)
            {
                if (buffDebuff.Affect.Contains("Pre Damage Calculation"))
                {
                    switch (buffDebuff.BuffDebuffName)
                    {
                        default:
                            break;
                    }
                }
            }

            foreach (Passive passive in damageSource.Passives)
            {
                if (passive.Affect.Contains("Pre Damage Calculation"))
                {
                    switch (passive.PassiveName)
                    {
                        default:
                            break;
                    }
                }
            }

            foreach (Target target in targets)
            {
                foreach (SpecialEffect specialEffect in target.SpecialEffects)
                {
                    if (specialEffect.Affect.Contains("Pre Damage Calculation"))
                    {
                        switch (specialEffect.SpecialEffectName)
                        {
                            default:
                                break;
                        }
                    }
                }

                foreach (BuffDebuff buffDebuff in target.BuffsDebuffs)
                {
                    if (buffDebuff.Affect.Contains("Pre Damage Calculation"))
                    {
                        switch (buffDebuff.BuffDebuffName)
                        {
                            default:
                                break;
                        }
                    }
                }

                foreach (Passive passive in target.Passives)
                {
                    if (passive.Affect.Contains("Pre Damage Calculation"))
                    {
                        switch (passive.PassiveName)
                        {
                            default:
                                break;
                        }
                    }
                }

                damages.Add(DamageCalculation(target, damageSource));
            }

            return damages;
        }

        public static int DamageCalculation(Target target, DamageSource damageSource)
        {
            int damage = random.Next(damageSource.ATK / 2, damageSource.ATK);
            if (random.Next(0, 100) < damageSource.CritChance)
            {
                damage = (int)(damage * damageSource.CritDamage);
            }
            damage = DMGCalcDamageTypeChecker(damage, target, damageSource);

            if (physicalDamageTypes.Contains(damageSource.DamageType))
            {
                damage = damage - target.DEF;
            }
            else if (magicalDamageTypes.Contains(damageSource.DamageType))
            {
                damage = damage - target.MDEF;
            }
            if (target.Guard == true)
            {
                damage = (int)Math.Round(damage * 0.5, 0);
            }
            damage = DMGCalcSpecialEffectChecker(damage, target, damageSource);
            damage = DMGCalcEffectChecker(damage, target, damageSource);
            damage = DMGCalcPassiveChecker(damage, target, damageSource);

            return damage;
        }

        public static int DMGCalcDamageTypeChecker(int damage, Target target, DamageSource damageSource)
        {
            if (Initializer.races[target.Race.Id].Fatal.Contains(damageSource.DamageType))
            {
                damage = damage * 2;
            }
            else if (Initializer.races[target.Race.Id].Weak.Contains(damageSource.DamageType))
            {
                damage = (int)Math.Round(damage * 1.5, 0);
            }
            else if (Initializer.races[target.Race.Id].Resist.Contains(damageSource.DamageType))
            {
                damage = (int)Math.Round(damage * 0.75, 0);
            }
            else if (Initializer.races[target.Race.Id].Endure.Contains(damageSource.DamageType))
            {
                damage = (int)Math.Round(damage * 0.25, 0);
            }
            else if (Initializer.races[target.Race.Id].Nulls.Contains(damageSource.DamageType))
            {
                damage = 0;
            }
            else
            {

            }
            return damage;
        }

        public static int DMGCalcEffectChecker(int damage, Target target, DamageSource damageSource)
        {
            foreach (BuffDebuff buffdebuff in target.BuffsDebuffs)
            {
                if (buffdebuff.Affect == "Damage Calculation")
                {
                    switch (buffdebuff.BuffDebuffName)
                    {
                        default:

                            break;
                    }
                }
            }
            foreach (BuffDebuff buffdebuff in damageSource.BuffsDebuffs)
            {
                if (buffdebuff.Affect == "Damage Calculation")
                {
                    switch (buffdebuff.BuffDebuffName)
                    {
                        default:

                            break;
                    }
                }
            }
            return damage;
        }

        public static int DMGCalcSpecialEffectChecker(int damage, Target target, DamageSource damageSource)
        {
            foreach (SpecialEffect specialEffect in damageSource.SpecialEffects)
            {
                if (specialEffect.Affect == "Damage Calculation")
                {
                    switch (specialEffect.SpecialEffectName)
                    {
                        default:

                            break;
                    }
                }
            }
            return damage;
        }

        public static int DMGCalcPassiveChecker(int damage, Target target, DamageSource damageSource)
        {
            foreach (Passive passive in target.Passives)
            {
                if (passive.Affect == "Damage Calculation")
                {
                    switch (passive.PassiveName)
                    {
                        default:

                            break;
                    }
                }
            }
            foreach (Passive passive in damageSource.Passives)
            {
                if (passive.Affect == "Damage Calculation")
                {
                    switch (passive.PassiveName)
                    {
                        default:

                            break;
                    }
                }
            }
            return damage;
        }
    }
}
