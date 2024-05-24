using ScriptableObjects.Turret;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using Supinfo.Project.Scripts.ScriptableObjects.Experience;
using UnityEngine;
using Random = System.Random;

namespace IA.Event
{
    public class IAThinker1 : IAThinker
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

        // STATS SO XP AGE 
        [SerializeField] private ExperienceStatSo experienceStatSo;

        // STATS SO Turret
        [SerializeField] private  TurretStatSo turretStatSo;

        // STATS SO SPECIAL CAPACITY
        [SerializeField] private CapacitySo capacityFireSo;
        [SerializeField] private CapacitySo capacityFlashSo;
        
        
        private int getRand(int minValue, int maxValue)
        {
            return aleatoire.Next(minValue, maxValue);
        }

        public bool Spawn()
        {
            int minValue = 0;
            int maxValue = 3;
            index = getRand(minValue, maxValue);
            switch (index)
            {
                case 0 : 
                    // SPAWN MELEE
                    if (Gold >= meleeStatSo.Price)
                    {
                        eventInstance.SpawnUnit(meleeStatSo);
                        return true;
                    }
                    return false;
                case 1 : 
                    // SPAWN RANGE 
                    if (Gold >= rangeStatSo.Price)
                    {
                        eventInstance.SpawnUnit(rangeStatSo);
                        return true;
                    }
                    return false;
                case 2 : 
                    // SPAWN ARMOR
                    if (Gold >= armorStatSo.Price)
                    {
                        eventInstance.SpawnUnit(armorStatSo);
                        return true;
                    }
                    return false;
                case 3 : 
                    // SPAWN ANTI-ARMOR
                    if (Gold >= antiArmorStatSo.Price)
                    {
                        eventInstance.SpawnUnit(antiArmorStatSo);
                        return true;
                    }
                    return false;
            }

            return false;
        }
        public bool SpecialCapacity()
        {
            int minValue = 0;
            int maxValue = 1;
            index = getRand(minValue, maxValue);
            switch (index)
            {
                case 0: 
                    if (Xp >= (experienceStatSo.ExperienceLevel[Age]*30)/100)
                    {
                        eventInstance.UseCapacity(capacityFireSo);
                        return true;
                    }
                    return false;
                case 1:
                    if (Xp >= (experienceStatSo.ExperienceLevel[Age]*60)/100)
                    {
                        eventInstance.UseCapacity(capacityFlashSo);
                        return true;
                    }
                    return false;   
            }
            return false;
        }
        
        public bool AgeUpgrade()
        {
            if (Xp >= experienceStatSo.ExperienceLevel[Age])
            {
                eventInstance.UpgradeAge();
                Age++;
                return true;
            }

            return false;
        }
    }
}

// VERIF PRIX / XP 


// Brain / Switch Fonctionement :

// Switch 4 Action possible (Appel fonction rand pour choisir) 
// Vérifier si action possible, si pas le cas re appel rand  (XP/ GOLD)
    // 1- Spawn Unit 
    // 2- Turret 
    // 3- Capacity
    // 4- Upgrade Age 
    // 5- Amelioration
    // Default spawn Unit 


// Même Logique pour Amelioration, Turret   