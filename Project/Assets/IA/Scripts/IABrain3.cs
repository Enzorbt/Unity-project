// Amelioration : BUY GOLD AMELIORATION, TURRET, ARMOR AND RANGE AMELLIORATION


using System.Collections;
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
            if (iaThinker.DetectUnitsAndAllies() != 0)
            {                
                iaThinker.Spawn(3);
                Debug.Log("SPAWN");

            }
            
            if (iaThinker.DetectUnitsAndAllies() == 3)
            {                
                iaThinker.Spawn(0);
                iaThinker.Spawn(0);
                iaThinker.Spawn(1);
                Debug.Log("SPAWN");
            }
            
            // LANCE CAPACITE SPECIAL
            if (iaThinker.DetectUnitsAndAllies() >= 6)
            {
                iaThinker.SpecialCapacity(1, true);
                Debug.Log("CAPACITY 1");
            }
            if (iaThinker.DetectUnitsAndAllies() == 10)
            {
                iaThinker.SpecialCapacity(0, true);
                Debug.Log("CAPACITY 2");
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
                        Debug.Log("SPAWN");
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
                        Debug.Log("AGE");
                    }
                }
            }
            yield return new WaitForSeconds(delayTime);
            iaThinker.Gold += 5; 
            iaThinker.IsThinking = false;
        }
    }
}