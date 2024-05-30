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
                iaThinker.Spawn(UnitChoice.armor);
                iaThinker.Spawn(UnitChoice.range);
            }
            
            
            // LAUCH CAPACITY
            if (iaThinker.DetectUnitsAndAllies() >= 5)
            {
                iaThinker.SpecialCapacity(CapacityChoice.fire, false);
            }

            
            // Comporetement Applicatif
            var actionChoice = ActionChoice.age;

            ActionChoice setAction(ActionChoice pactionChoice)
            {
                actionChoice = pactionChoice;
                return actionChoice;
            }

            if (iaThinker.Gold > 300)
            {
                if (actionChoice == ActionChoice.turret) // TURRET
                {
                    if (!iaThinker.Turret())
                    {
                        setAction(ActionChoice.turret);
                    }
                    else
                    {
                        setAction(ActionChoice.upgrade);
                    }
                }
                else if (actionChoice == ActionChoice.upgrade) // UPGRADE
                {
                    var upgradeIndex = iaThinker.getRand(0,6);
                    switch (upgradeIndex)
                    {
                        case 0:
                            for (int index = 1; index <= 3;)
                            {
                                iaThinker.Upgrade(UpgradeType.GoldGiven);
                                index++;
                            }
                            break;
                        case 1:
                            for (int index = 1; index <= 3;)
                            {
                                iaThinker.Upgrade(UpgradeType.TurretAttack);
                                index++;
                            } 
                            break;
                        case 2:
                            for (int index = 1; index <= 3;)
                            {
                                iaThinker.Upgrade(UpgradeType.TurretRange);
                                index++;
                            } 
                            break;
                        case 3:
                            for (int index = 1; index <= 3;)
                            {
                                iaThinker.Upgrade(UpgradeType.ArmorAttack);
                                index++;
                            } 
                            break;
                        case 4:
                            for (int index = 1; index <= 3;)
                            {
                                iaThinker.Upgrade(UpgradeType.ArmorHealth);
                                index++;
                            } 
                            break;
                        case 5:
                            for (int index = 1; index <= 3;)
                            {
                                iaThinker.Upgrade(UpgradeType.RangeAttack);
                                index++;
                            } 
                            break;
                        case 6:
                            for (int index = 1; index <= 3;)
                            {
                                iaThinker.Upgrade(UpgradeType.RangeRange);
                                index++;
                            } 
                            break;
                    }
                }                    
            }
            yield return new WaitForSeconds(delayTime);
            iaThinker.Gold += 5; 
            iaThinker.IsThinking = false;
        }
    }
}