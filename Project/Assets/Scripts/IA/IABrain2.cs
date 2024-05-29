// Defense STRATEGIE 

// FINIR : 
    // Amelioration : BUY GOLD AMELIORATION, TURRET, ARMOR AND RANGE AMELLIORATION

using Common;
using UnityEngine;

namespace IA.Event
{
    public class IABrain2 : Brain
    {
        public override void Think(Thinker thinker)
        {
            if (thinker is not IAThinker iaThinker) return;

            // DETECTION 
            GameObject[] unitsAndAllies = GameObject.FindGameObjectsWithTag("Unit, Allies");
            
            // SPAWN UNIT 
            if (unitsAndAllies.Length != 0)
            {
                iaThinker.Spawn(2);
                iaThinker.Spawn(1);
            }
            
            // LANCE CAPACITE SPECIAL
            if (unitsAndAllies.Length >= 5)
            {
                iaThinker.SpecialCapacity(0, false);
            }

            // Comporetement Applicatif
            var index = 0;

            int setIndex(int pindex)
            {
                index = pindex;
                return index;
            }

            if (iaThinker.Gold > 300)
            {
                if (index == 0)
                {
                    if (!iaThinker.Turret())
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
                    if (!iaThinker.AgeUpgrade())
                    {
                        setIndex(1);
                    }
                    else
                    {
                        setIndex(2);
                    }
                }
                else if (index == 2)
                {
                    if (!iaThinker.UnlockNewUnit())
                    {
                        setIndex(2);
                    }
                    else
                    {
                        setIndex(0);
                    }
                }
            }
        }
    }
}