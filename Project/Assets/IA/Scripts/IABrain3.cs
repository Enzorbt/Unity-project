// Attack STRATEGIE 

// FINIR : 
    // Amelioration : BUY GOLD AMELIORATION, TURRET, ARMOR AND RANGE AMELLIORATION


    using System.Collections;
    using Common;
using Supinfo.Project.Common;
using UnityEngine;

namespace IA.Event
{
    [CreateAssetMenu(menuName = "Brains/IABrain3")]
    public class IABrain3 : BrainWithDelay
    {
        public override IEnumerator ThinkWithDelay(ThinkerWithDelay thinker)
        {
            if (thinker is not IAThinker iaThinker)yield break;
            iaThinker.IsThinking = true;
            iaThinker.UnlockNewUnit();

            
            // SPAWN UNIT 
            if (iaThinker.PlayerUnits.Count != 0)
            {
                iaThinker.Spawn(0);
                iaThinker.Spawn(0);
                iaThinker.Spawn(1);

            }
            
            // A tester
            // LANCE CAPACITE SPECIAL
            if (iaThinker.PlayerUnits.Count >= 6)
            {
                iaThinker.SpecialCapacity(1, true);
            }
            if (iaThinker.PlayerUnits.Count == 10)
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
                        setIndex(0);
                    }
                }
            }
            yield return new WaitForSeconds(delayTime);
            iaThinker.IsThinking = false;
        }
    }
}