using System.Collections;
using Supinfo.Project.Common;
using Supinfo.Project.Scripts;
using UnityEngine;

// The condition of counter is controlling time and action priority

namespace IA.Event
{
    [CreateAssetMenu(menuName = "Brains/IABrain3")]
    public class IABrain3 : BrainWithDelay
    {
        public override IEnumerator ThinkWithDelay(ThinkerWithDelay thinker)
        {
            if (thinker is not IAThinker iaThinker)yield break;
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
            
            // SPAWN PAR DEFAULT
            if (iaThinker.DetectUnitsAndAllies() == 0 && iaThinker.DetectUnitsAndEnemies() == 0 && iaThinker.SpawnCounter > 10)
            {                
                iaThinker.Spawn(UnitChoice.melee, true);
                iaThinker.Spawn(UnitChoice.melee, true);
                iaThinker.SpawnCounter = 0;
            }
            
            // SPAWN UNIT (TANK STRATEGY)
            if (iaThinker.DetectUnitsAndAllies() > 0 && iaThinker.DetectUnitsAndEnemies() < 3 && iaThinker.SpawnCounter > 20)
            {
                iaThinker.Spawn(UnitChoice.melee, false);
                iaThinker.Spawn(UnitChoice.range, false);
                iaThinker.Spawn(UnitChoice.range, false);
                iaThinker.SpawnCounter = 0;
            }
            
            // LAUCH CAPACITY
            if (iaThinker.DetectUnitsAndAllies() >= 6) // LANCE ECLAIRE SI +6 UNITE ADVAIRSE
            {
                iaThinker.SpecialCapacity(CapacityChoice.lightning, true);
            }
            
            if (iaThinker.DetectUnitsAndAllies() == 10) // LANCE BOULE DE FEUX SI 10 UNITE ADVAIRSE
            {
                iaThinker.SpecialCapacity(CapacityChoice.fire, true);
            }
            
            // Comporetement Applicatif
            
            if (iaThinker.TurretCounter > 7) // TURRET
            {
                iaThinker.Turret();
                iaThinker.TurretCounter = 0;
            }
            
            if (iaThinker.UpgradeCounter > 5 && iaThinker.Gold > 1000) // UPGRADE
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