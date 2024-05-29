using System.Collections;
using System.Collections.Generic;
using Common;
using ScriptableObjects.Unit;
using Supinfo.Project.Common;
using Supinfo.Project.Scripts;
using UnityEngine;

// FINIR 
    // LOGIQUE 
    // ALWAY KEEP MONEY FOR BUYING MELEE TO DEFENCE 

    // Amelioration : BUY GOLD AMELIORATION, TURRET, ARMOR AND RANGE AMELLIORATION

namespace IA.Event
{
    [CreateAssetMenu(menuName = "Brains/IABrain4")]
    public class IABrain4 : BrainWithDelay
    {
        public override IEnumerator ThinkWithDelay(ThinkerWithDelay thinker)
        {
            if (thinker is not IAThinker iaThinker) yield break;
            iaThinker.IsThinking = true;
            
            // SPAWN UNIT 
            iaThinker.UnlockNewUnit();
            iaThinker.SpawnDifficult();
            
            // CAPACITE
            if (iaThinker.PlayerUnits.Count >= 7)
            {
                iaThinker.SpecialCapacity(0, true);
                // Rembourse XP
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
            // foreach (KeyValuePair<UpgradeType, int> entry in iaThinker.UpgradeDict)
            // {
            //     if (entry.Value < 3)
            //     {
            //         iaThinker.Upgrade(entry.Key);
            //     }
            // }
            yield return new WaitForSeconds(delayTime);
            iaThinker.IsThinking = false;
        }
    }
}