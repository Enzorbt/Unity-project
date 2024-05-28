using System.Collections.Generic;
using Common;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts;
using UnityEngine;

// FINIR 
    // LOGIQUE 
    // ALWAY KEEP MONEY FOR BUYING MELEE TO DEFENCE 

    // VERIF PRIX / XP 
    // Amelioration : BUY GOLD AMELIORATION, TURRET, ARMOR AND RANGE AMELLIORATION
    // Capacité Type

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
            iaThinker.SpawnDifficult(iaThinker.PlayerUnit);
            
            // CAPACITE
            if (unitsAndAllies.Length >= 7)
            {
                iaThinker.SpecialCapacity(0);
                // Rembourse XP
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