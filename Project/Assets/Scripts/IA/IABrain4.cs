using System.Collections.Generic;
using Common;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts;
using UnityEngine;

// FINIR 
    // LOGIQUE 
    // ALWAY KEEP MONEY FOR BUYING MELEE TO DEFENCE 

    // Amelioration : BUY GOLD AMELIORATION, TURRET, ARMOR AND RANGE AMELLIORATION

namespace IA.Event
{
    public class IABrain4 : Brain
    {
        public override void Think(Thinker thinker)
        {
            if (thinker is not IAThinker iaThinker)return;

            // DETECTION 
            GameObject[] unitsAndAllies = GameObject.FindGameObjectsWithTag("Unit, Allies");
            
            // SPAWN UNIT 
            iaThinker.UnlockNewUnit();
            iaThinker.SpawnDifficult(iaThinker.PlayerUnit);
            
            // CAPACITE
            if (unitsAndAllies.Length >= 7)
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
        }
    }
}