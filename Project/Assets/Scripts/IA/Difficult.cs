// Attack : Stratégie COUNTER + TANK (Armor + RANGE) 


// VERIF PRIX / XP 




// Base : Acheté Deux tourelles
// Amélioration : Acheté amélioration RANGE ET ARMOR EN PRIORITÉ, PUIS LE GOLD AMELIORATION puis les autres 
// Quand +7 UNITÉ Adversaise USE CAPACITY IF POSSIBLE 
// UPGRADE AGE + TOT POSSIBLE 
// ALWAY KEEP MONEY FOR BUYING MELEE TO DEFENCE 

// UNLOCK 

// Spawn Unit : 

    // SI JOUEUR PLACE ANTI-ARMOR   
        // SPAWN JOUEUR PLACE RANGE

    // SI JOUEUR PLACE RANGE  
        // SPAWN JOUEUR PLACE MELEE

    // SI JOUEUR PLACE MELEE  
        // SPAWN JOUEUR PLACE ARMOR

    // SI JOUEUR PLACE ARMOR 
        // SPAWN JOUEUR PLACE ANTI-ARMOR 
        
    // SI JOUEUR NE PLACE RIEN  
        // SPAWN ARMOR 
        // SPAWN RANGE * 2        

using ScriptableObjects.Unit;
using UnityEngine;
using Random = System.Random;

namespace IA.Event
{
    public class Difficult : IABase
    {
        private EventsFoundation eventInstance = new EventsFoundation();
        Random aleatoire = new Random();
        private int _indexSpawn;
        public int index;
        
        // STATS SO UNIT
        [SerializeField] private UnitStatSo meleeStatSo;
        [SerializeField] private UnitStatSo rangeStatSo;
        [SerializeField] private UnitStatSo armorStatSo;
        [SerializeField] private UnitStatSo antiArmorStatSo;

        public void Upgrade()
        {
            
        }
        
        public void Spawn(UnitStatSo playerUnit)
        {
            // COUNTER (Unité forte contre celle que le joeur pose)
            if (playerUnit.Type == antiArmorStatSo.Type.StrongAgainst && Gold >= antiArmorStatSo.Price) // ARMOR
            {
                eventInstance.SpawnUnit(antiArmorStatSo);
            }
            if (playerUnit.Type == rangeStatSo.Type.StrongAgainst) // ANTI ARMOR
            {
                eventInstance.SpawnUnit(rangeStatSo);
            }
            if (playerUnit.Type == meleeStatSo.Type.StrongAgainst) // RANGE
            {
                eventInstance.SpawnUnit(meleeStatSo);
            }
            if (playerUnit.Type == armorStatSo.Type.StrongAgainst) // MELEE
            {
                eventInstance.SpawnUnit(armorStatSo);
            }
            
            // SI LE JOUER NE PLACE TANK (ARMOR + 2 RANGE)
            if (playerUnit == null)
            {
                eventInstance.SpawnUnit(armorStatSo);
                eventInstance.SpawnUnit(rangeStatSo);
                eventInstance.SpawnUnit(rangeStatSo);
            }
        }
    }
}        