// Amelioration : BUY GOLD AMELIORATION, TURRET, ARMOR AND RANGE AMELLIORATION

using System.Collections;
using Supinfo.Project.Common;
using UnityEngine;

namespace IA.Event
{
    [CreateAssetMenu(menuName = "Brains/IABrain2")]
    public class IABrain2 : BrainWithDelay
    {
        public override IEnumerator ThinkWithDelay(ThinkerWithDelay thinker)
        {
            if (thinker is not IAThinker iaThinker) yield break;
            
            iaThinker.IsThinking = true;

            iaThinker.UnlockNewUnit();
            
            // SPAWN UNIT 
            if (iaThinker.DetectUnitsAndAllies() != 0)
            {
                iaThinker.Spawn(2);
                iaThinker.Spawn(1);
            }
            
            // LANCE CAPACITE SPECIAL
            if (iaThinker.DetectUnitsAndAllies() >= 5)
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
                        setIndex(0);
                    }
                }
            }
            
            yield return new WaitForSeconds(delayTime);
            iaThinker.Gold += 5; 
            iaThinker.IsThinking = false;
        }
    }
}