// Defense STRATEGIE 

// FINIR : 
    // VERIF PRIX / XP 
    // Amelioration : BUY GOLD AMELIORATION, TURRET, ARMOR AND RANGE AMELLIORATION

using Common;
using UnityEngine;

namespace IA.Event
{
    public class IABrain2 : Brain
    {
        public override void Think(Thinker thinker)
        {
            if (thinker is not IAThinker) return;

            // DETECTION 
            GameObject[] unitsAndAllies = GameObject.FindGameObjectsWithTag("Unit, Allies");
            
            // SPAWN UNIT 
            if (unitsAndAllies.Length != 0)
            {
                IAThinker.Spawn(2);
                IAThinker.Spawn(1);
            }
            
            // LANCE CAPACITE SPECIAL
            if (unitsAndAllies.Length >= 5)
            {
                IAThinker.SpecialCapacity(0);
            }

            // Comporetement Applicatif
            var index = 0;

            int setIndex(int pindex)
            {
                index = pindex;
                return index;
            }

            if (IAThinker.Gold > 300)
            {
                if (index == 0)
                {
                    if (IAThinker.Turret())
                    {
                        setIndex(0);
                    }
                    else
                    {
                        setIndex(1);
                    }
                }
                else if (index == 1)
                {
                    if (IAThinker.AgeUpgrade())
                    {
                        setIndex(0);
                    }
                    else
                    {
                        setIndex(1);
                    }
                }
                else if (index == 2)
                {
                    if (IAThinker.UnlockNewUnit())
                    {
                        setIndex(0);
                    }
                    else
                    {
                        setIndex(1);
                    }
                }
            }
        }
    }
}