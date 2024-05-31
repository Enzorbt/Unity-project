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
            if (iaThinker.AgeCounter > 30)
            {
                iaThinker.AgeUpgrade();
                iaThinker.AgeCounter = 0;
            }
            
            // UNLOCK UNIT
            if (!iaThinker.IsUnlock)
            {
                iaThinker.UnlockNewUnit();
            }
            
            
            // SPAWN UNIT (TANK STRATEGY)
            if (iaThinker.DetectUnitsAndAllies() > 0 && iaThinker.DetectUnitsAndEnemies() < 2 && iaThinker.SpawnCounter > 15)
            {
                iaThinker.Spawn((UnitChoice)iaThinker.getRand(0, 3), false);
                iaThinker.Spawn((UnitChoice)iaThinker.getRand(0, 3), false);
                iaThinker.Spawn((UnitChoice)iaThinker.getRand(0, 3), true);
                iaThinker.SpawnCounter = 0;
            }
            
            
            // LAUNCH CAPACITY
            if (iaThinker.DetectUnitsAndAllies() >= 5)
            {
                iaThinker.SpecialCapacity(CapacityChoice.fire, false);
            }
            
            // Comporetement Applicatif
            
            if (iaThinker.TurretCounter > 7) // TURRET
            {
                iaThinker.Turret();
                iaThinker.TurretCounter = 0;
            }
            
            if (iaThinker.UpgradeCounter > 5) // UPGRADE
            {
                var upgrade = (UpgradeType)iaThinker.getRand(0, 10);
                iaThinker.Upgrade(upgrade);
                iaThinker.UpgradeCounter = 0;
            }
            
            yield return new WaitForSeconds(delayTime);
            
            iaThinker.IsThinking = false;
            
            iaThinker.AgeCounter++;
            iaThinker.SpawnCounter++;
            iaThinker.UpgradeCounter++;
            iaThinker.TurretCounter++;
            iaThinker.Gold += 5; 
            
        }
    }
}