// Amelioration : BUY GOLD AMELIORATION, TURRET, ARMOR AND RANGE AMELLIORATION

using System.Collections;
using Supinfo.Project.Common;
using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.ScriptableObjects.Upgrades;
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

            // AGE UPGRADE
            iaThinker.AgeUpgrade();
            
            // UNLOCK UNIT
            if (!iaThinker.IsUnlock)
            {
                iaThinker.UnlockNewUnit();
            }
            
            
            // SPAWN UNIT (TANK STRATEGY)
            if (iaThinker.DetectUnitsAndAllies() != 0)
            {
                iaThinker.Spawn(UnitChoice.armor, false);
                iaThinker.Spawn(UnitChoice.range, false);
            }
            
            
            // LAUCH CAPACITY
            if (iaThinker.DetectUnitsAndAllies() >= 5)
            {
                iaThinker.SpecialCapacity(CapacityChoice.fire, false);
            }
            
            // Comporetement Applicatif
            
            if (iaThinker.Action == ActionChoice.turret) // TURRET
            {
                if (!iaThinker.Turret())
                {
                    iaThinker.Action = ActionChoice.turret;
                }
                else
                {
                    iaThinker.Action = ActionChoice.upgrade;
                }
            }
            if (iaThinker.Action == ActionChoice.upgrade) // UPGRADE
            {
                var upgrade = (UpgradeType)iaThinker.getRand(0, 10);
                iaThinker.Upgrade(upgrade);
            }
            
            yield return new WaitForSeconds(delayTime);
            iaThinker.Gold += 5; 
            iaThinker.IsThinking = false;
            
        }
    }
}