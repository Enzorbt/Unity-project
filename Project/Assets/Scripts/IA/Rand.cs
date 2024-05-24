using ScriptableObjects.Unit;
using UnityEngine;
using Random = System.Random;

namespace IA.Event
{
    public class Rand
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

        private int getRand(int minValue, int maxValue)
        {
            return aleatoire.Next(minValue, maxValue);
        }

        public void Spawn()
        {
            int minValue = 1;
            int maxValue = 4;
            index = getRand(minValue, maxValue);
            switch (index)
            {
                case 1 : 
                    // SPAWN MELEE
                    eventInstance.SpawnUnit(meleeStatSo);
                    break;
                case 2 : 
                    // SPAWN RANGE 
                    eventInstance.SpawnUnit(rangeStatSo);
                    break;
                case 3 : 
                    // SPAWN ARMOR
                    eventInstance.SpawnUnit(armorStatSo);
                    break;
                case 4 : 
                    // SPAWN ARMOR
                    eventInstance.SpawnUnit(antiArmorStatSo);    
                    break;
                default:
                    // SPAWN MELEE
                    eventInstance.SpawnUnit(meleeStatSo);
                    break;
            }
        }
   
    }
}

// VERIF PRIX / XP 


// IA RANDOM 
// Fonction choisi un nombre entre p_input_1 et p_input_2

// Brain / Switch Fonctionement :

// Switch 4 Action possible (Appel fonction rand pour choisir) 
// Vérifier si action possible, si pas le cas re appel rand  (XP/ GOLD)
    // 1- Spawn Unit 
    // 2- Turret 
    // 3- Capacity
    // 4- Upgrade Age 
    // 5- Amelioration
    // Default spawn Unit 


// Switch Unit (Appel fonction rand pour choisir) 
// Vérifier si action possible, si pas le cas re appel rand (XP/ GOLD) 
    // 1- Melee
    // 2- Range
    // 3- Armor
    // 4- Anti-Armor
    // Default spawn Unit     

// Même Logique pour Capacity, Amelioration, Turret   