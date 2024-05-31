// Amelioration : BUY GOLD AMELIORATION, TURRET, ARMOR AND RANGE AMELLIORATION

using System.Collections;
using Supinfo.Project.Common;
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
            iaThinker.AgeUpgrade();
            
            // UNLOCK UNIT 
            if (!iaThinker.IsUnlock)
            {
                iaThinker.UnlockNewUnit();
            }
            
            
            // SPAWN UNIT 
            
            // COUNTER (Unité forte contre celle que le joueur pose)

            if (iaThinker.PlayerUnits.Count > 0)
            {
                if (iaThinker.PlayerUnits.Peek().Type == iaThinker.antiArmorStatSo.Type.StrongAgainst) // ARMOR
                {
                    iaThinker.Spawn(UnitChoice.antiarmor, true);
                    iaThinker.PlayerUnits.Dequeue();
                }
                else if (iaThinker.PlayerUnits.Peek().Type == iaThinker.rangeStatSo.Type.StrongAgainst) // ANTI ARMOR
                {
                    iaThinker.Spawn(UnitChoice.range, true);
                    iaThinker.PlayerUnits.Dequeue();
                }
                else if (iaThinker.PlayerUnits.Peek().Type == iaThinker.meleeStatSo.Type.StrongAgainst) // RANGE
                {
                    iaThinker.Spawn(UnitChoice.melee, true);
                    iaThinker.PlayerUnits.Dequeue();
                }
                else if (iaThinker.PlayerUnits.Peek().Type == iaThinker.armorStatSo.Type.StrongAgainst) // MELEE
                {
                    iaThinker.Spawn(UnitChoice.armor, true);
                    iaThinker.PlayerUnits.Dequeue();
                }   
            }
            
            // SI LE JOUEUR NE PLACE RIEN TANK (ARMOR + RANGE)
            
            if (iaThinker.DetectUnitsAndAllies() == 0 && iaThinker.SpawnCounter == 7)
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
            if (iaThinker.DetectUnitsAndAllies() >= 7)
            {
                iaThinker.SpecialCapacity(CapacityChoice.fire, true);
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
                        setAction(ActionChoice.age);
                    }
                }
                else if (actionChoice == ActionChoice.capacity) // UPGRADE
                {
                    if (!iaThinker.AgeUpgrade())
                    {
                        setAction(ActionChoice.capacity);
                    }
                    else
                    {
                        setAction(ActionChoice.turret);
                    }
                }
            }
            
            yield return new WaitForSeconds(delayTime);

            iaThinker.SpawnCounter++;
            
            iaThinker.Gold += 5; 
            
            iaThinker.IsThinking = false;
        }
    }
}