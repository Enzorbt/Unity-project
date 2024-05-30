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
                    iaThinker.Spawn(3);
                    // iaThinker.Gold -= iaThinker.antiArmorStatSo.Price;
                    Debug.Log("ANTI-ARMOR");
                    iaThinker.PlayerUnits.Dequeue();
                }
                else if (iaThinker.PlayerUnits.Peek().Type == iaThinker.rangeStatSo.Type.StrongAgainst) // ANTI ARMOR && iaThinker.Gold >= iaThinker.rangeStatSo.Price
                {
                    iaThinker.Spawn(1);
                    // iaThinker.Gold -= iaThinker.rangeStatSo.Price;
                    Debug.Log("RANGE");
                    iaThinker.PlayerUnits.Dequeue();
                }
                else if (iaThinker.PlayerUnits.Peek().Type == iaThinker.meleeStatSo.Type.StrongAgainst) // RANGE && iaThinker.Gold >= iaThinker.meleeStatSo.Price
                {
                    iaThinker.Spawn(0);
                    // iaThinker.Gold -= iaThinker.meleeStatSo.Price;
                    Debug.Log("MELEE");
                    iaThinker.PlayerUnits.Dequeue();
                }
                else if (iaThinker.PlayerUnits.Peek().Type == iaThinker.armorStatSo.Type.StrongAgainst) // MELEE && iaThinker.Gold >= iaThinker.armorStatSo.Price && iaThinker.IsUnlock
                {
                    iaThinker.Spawn(2);
                    // iaThinker.Gold -= iaThinker.armorStatSo.Price;
                    Debug.Log("ARMOR");
                    iaThinker.PlayerUnits.Dequeue();
                }   
            }
            
            // SI LE JOUER NE PLACE RIEN TANK (ARMOR + 2 RANGE)
            
            if (iaThinker.DetectUnitsAndAllies() == 0)
            {
                Debug.Log(iaThinker.Gold);
                float goldTank = iaThinker.armorStatSo.Price + (iaThinker.rangeStatSo.Price) * 2;
                if (iaThinker.Gold >= goldTank && iaThinker.IsUnlock)
                {
                    iaThinker.Spawn(2);
                    iaThinker.Spawn(1);
                    iaThinker.Spawn(1);
                    // iaThinker.Gold -= goldTank;
                    Debug.Log("SPAWN RIEN");
                }
            }
            
            
            // CAPACITE
            if (iaThinker.DetectUnitsAndAllies() >= 7)
            {
                iaThinker.SpecialCapacity(0, true);
                Debug.Log("CAPACITY");
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
                    // if (!iaThinker.Turret())
                    // {
                    //     setIndex(0);
                    // }
                    // else
                    // {
                    //     setIndex(1);
                    //     Debug.Log("TURRET");
                    // }
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