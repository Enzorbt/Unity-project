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

            if (iaThinker.PlayerUnits.Count != 0)
            {
                if (iaThinker.PlayerUnits.Peek().Type == iaThinker.antiArmorStatSo.Type.StrongAgainst) // ARMOR
                {
                    iaThinker.Spawn(UnitChoice.antiarmor);
                    iaThinker.Gold -= iaThinker.antiArmorStatSo.Price;
                    iaThinker.PlayerUnits.Dequeue();
                }
                else if (iaThinker.PlayerUnits.Peek().Type == iaThinker.rangeStatSo.Type.StrongAgainst) // ANTI ARMOR
                {
                    iaThinker.Spawn(UnitChoice.range);
                    iaThinker.Gold -= iaThinker.rangeStatSo.Price;
                    iaThinker.PlayerUnits.Dequeue();
                }
                else if (iaThinker.PlayerUnits.Peek().Type == iaThinker.meleeStatSo.Type.StrongAgainst) // RANGE
                {
                    iaThinker.Spawn(UnitChoice.melee);
                    iaThinker.Gold -= iaThinker.meleeStatSo.Price;
                    iaThinker.PlayerUnits.Dequeue();
                }
                else if (iaThinker.PlayerUnits.Peek().Type == iaThinker.armorStatSo.Type.StrongAgainst) // MELEE
                {
                    iaThinker.Spawn(UnitChoice.armor);
                    iaThinker.Gold -= iaThinker.armorStatSo.Price;
                    iaThinker.PlayerUnits.Dequeue();
                }   
            }
            
            // SI LE JOUEuR NE PLACE RIEN TANK (ARMOR + RANGE)
            
            if (iaThinker.DetectUnitsAndAllies() == 0)
            {
                float goldTank = iaThinker.armorStatSo.Price + iaThinker.rangeStatSo.Price;
                if (iaThinker.Gold >= goldTank && iaThinker.IsUnlock)
                {
                    iaThinker.Spawn(UnitChoice.armor);
                    iaThinker.Spawn(UnitChoice.range);
                    iaThinker.Gold -= goldTank;
                }
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
            
            iaThinker.Gold += 5; 
            
            iaThinker.IsThinking = false;
        }
    }
}