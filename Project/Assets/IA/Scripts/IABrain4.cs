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
            
            // UNLOCK UNIT 
            if (!iaThinker.IsUnlock)
            {
                iaThinker.UnlockNewUnit();
                Debug.Log("UNLOCK");
            }
            
            // SPAWN UNIT 
            // COUNTER (Unité forte contre celle que le joueur pose)

            if (iaThinker.PlayerUnits.Count != 0)
            {
                Debug.Log("START");
                if (iaThinker.PlayerUnits.Peek().Type == iaThinker.antiArmorStatSo.Type.StrongAgainst) // ARMOR && iaThinker.Gold >= iaThinker.antiArmorStatSo.Price
                {
                    iaThinker.Spawn(UnitChoice.antiarmor);
                    // iaThinker.Gold -= iaThinker.antiArmorStatSo.Price;
                    Debug.Log("ANTI-ARMOR");
                    iaThinker.PlayerUnits.Dequeue();
                }
                else if (iaThinker.PlayerUnits.Peek().Type == iaThinker.rangeStatSo.Type.StrongAgainst) // ANTI ARMOR && iaThinker.Gold >= iaThinker.rangeStatSo.Price
                {
                    iaThinker.Spawn(UnitChoice.range);
                    // iaThinker.Gold -= iaThinker.rangeStatSo.Price;
                    Debug.Log("RANGE");
                    iaThinker.PlayerUnits.Dequeue();
                }
                else if (iaThinker.PlayerUnits.Peek().Type == iaThinker.meleeStatSo.Type.StrongAgainst) // RANGE && iaThinker.Gold >= iaThinker.meleeStatSo.Price
                {
                    iaThinker.Spawn(UnitChoice.melee);
                    // iaThinker.Gold -= iaThinker.meleeStatSo.Price;
                    Debug.Log("MELEE");
                    iaThinker.PlayerUnits.Dequeue();
                }
                else if (iaThinker.PlayerUnits.Peek().Type == iaThinker.armorStatSo.Type.StrongAgainst) // MELEE && iaThinker.Gold >= iaThinker.armorStatSo.Price && iaThinker.IsUnlock
                {
                    iaThinker.Spawn(UnitChoice.armor);
                    // iaThinker.Gold -= iaThinker.armorStatSo.Price;
                    Debug.Log("ARMOR");
                    iaThinker.PlayerUnits.Dequeue();
                }   
            }
            
            // SI LE JOUER NE PLACE RIEN TANK (ARMOR + RANGE)
            
            if (iaThinker.DetectUnitsAndAllies() == 0)
            {
                Debug.Log(iaThinker.Gold);
                float goldTank = iaThinker.armorStatSo.Price + iaThinker.rangeStatSo.Price;
                if (iaThinker.Gold >= goldTank && iaThinker.IsUnlock)
                {
                    iaThinker.Spawn(UnitChoice.armor);
                    iaThinker.Spawn(UnitChoice.range);
                    // iaThinker.Gold -= goldTank;
                    Debug.Log("SPAWN RIEN");
                }
            }
            
            
            // CAPACITE
            if (iaThinker.DetectUnitsAndAllies() >= 7)
            {
                iaThinker.SpecialCapacity(CapacityChoice.fire, true);
                Debug.Log("CAPACITY");
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
                if (actionChoice == ActionChoice.turret)
                {
                    if (!iaThinker.Turret())
                    {
                        setAction(ActionChoice.turret);
                    }
                    else
                    {
                        setAction(ActionChoice.age);
                        Debug.Log("TURRET");
                    }
                }
                else if (actionChoice == ActionChoice.age)
                {
                    if (!iaThinker.AgeUpgrade())
                    {
                        setAction(ActionChoice.age);
                    }
                    else
                    {
                        setAction(ActionChoice.turret);
                        Debug.Log("AGE");
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
            iaThinker.Gold += 5; 
            iaThinker.IsThinking = false;
        }
    }
}