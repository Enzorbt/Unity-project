// Amelioration : BUY GOLD AMELIORATION, TURRET, ARMOR AND RANGE AMELLIORATION

using System.Collections;
using Supinfo.Project.Common;
using Supinfo.Project.Scripts;
using UnityEngine;

namespace IA.Event
{
    [CreateAssetMenu(menuName = "Brains/IABrain4")]
    public class IABrain4 : BrainWithDelay
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
            
            
            // SPAWN UNIT 
            
            // COUNTER (Unité forte contre celle que le joueur pose)
            
            if (iaThinker.PlayerUnits.Count > 0 && iaThinker.DetectUnitsAndEnemies() < 5 && iaThinker.SpawnCounter > 10)
            {
                if (iaThinker.PlayerUnits.Peek().Type == iaThinker.antiArmorStatSo.Type.StrongAgainst) // ARMOR
                {
                    iaThinker.Spawn(UnitChoice.antiarmor, true);
                    iaThinker.PlayerUnits.Dequeue();
                    iaThinker.SpawnCounter = 0;
                }
                else if (iaThinker.PlayerUnits.Peek().Type == iaThinker.rangeStatSo.Type.StrongAgainst) // ANTI ARMOR
                {
                    iaThinker.Spawn(UnitChoice.range, true);
                    iaThinker.PlayerUnits.Dequeue();
                    iaThinker.SpawnCounter = 0;
                }
                else if (iaThinker.PlayerUnits.Peek().Type == iaThinker.meleeStatSo.Type.StrongAgainst) // RANGE
                {
                    iaThinker.Spawn(UnitChoice.melee, true);
                    iaThinker.PlayerUnits.Dequeue();
                    iaThinker.SpawnCounter = 0;
                }
                else if (iaThinker.PlayerUnits.Peek().Type == iaThinker.armorStatSo.Type.StrongAgainst) // MELEE
                {
                    iaThinker.Spawn(UnitChoice.armor, true);
                    iaThinker.PlayerUnits.Dequeue();
                    iaThinker.SpawnCounter = 0;
                }   
            }
            
            // SI LE JOUEUR NE PLACE RIEN TANK (ARMOR + RANGE)
            
            if (iaThinker.DetectUnitsAndAllies() == 0 && iaThinker.DetectUnitsAndEnemies() == 0 && iaThinker.SpawnCounter > 7)
            {
                if (iaThinker.IsUnlock)
                {
                    iaThinker.Spawn(UnitChoice.armor, true);
                    iaThinker.Spawn(UnitChoice.range, true);
                    iaThinker.Spawn(UnitChoice.range, true);
                }
            
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