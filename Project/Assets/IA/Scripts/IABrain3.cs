// Attack STRATEGIE 

// FINIR : 
    // Amelioration : BUY GOLD AMELIORATION, TURRET, ARMOR AND RANGE AMELLIORATION


using Common;
using UnityEngine;

namespace IA.Event
{
    [CreateAssetMenu(menuName = "Brains/IABrain3")]
    public class IABrain3 : Brain
    {
        public override void Think(Thinker thinker)
        {
            if (thinker is not IAThinker iaThinker) return;
            
            // DETECTION 
            GameObject[] unitsAndAllies = GameObject.FindGameObjectsWithTag("Unit, Allies");
            
            // SPAWN UNIT 
            if (unitsAndAllies.Length != 0)
            {
                iaThinker.Spawn(0);
                iaThinker.Spawn(0);
                iaThinker.Spawn(1);

            }
            
            // A tester
            // LANCE CAPACITE SPECIAL
            if (unitsAndAllies.Length >= 6)
            {
                iaThinker.SpecialCapacity(1, true);
            }
            if (unitsAndAllies.Length == 10)
            {
                iaThinker.SpecialCapacity(0, true);
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
                    if (!iaThinker.Spawn(iaThinker.getRand(0, 3)))
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