using System.Collections.Generic;
using Common;
using ScriptableObjects.Turret;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using Supinfo.Project.Scripts.ScriptableObjects.Experience;
using UnityEngine;
using Random = System.Random;

namespace IA.Event
{
    public abstract class IAThinker : Thinker
    {
        protected internal static float Gold {get; set;}
        protected internal static float Xp {get; set;}
        
        protected static int Age {get; set;}
        protected static int TurretNumber {get; set;}
        protected static bool IsUnlock {get; set;}

        public Dictionary<UpgradeType, int> UpgradeDict; // Unity
        
        private static EventsFoundation eventInstance = new EventsFoundation();
        static Random aleatoire = new Random();
        public int index;
        
        // STATS SO UNIT
        [SerializeField] private static UnitStatSo meleeStatSo;
        [SerializeField] private static UnitStatSo rangeStatSo;
        [SerializeField] private static UnitStatSo armorStatSo;
        [SerializeField] private static UnitStatSo antiArmorStatSo;

        // STATS SO XP AGE 
        [SerializeField] private static ExperienceStatSo experienceStatSo;

        // STATS SO Turret
        [SerializeField] private static TurretStatSo turretStatSo;

        // STATS SO SPECIAL CAPACITY
        [SerializeField] private static CapacitySo capacityFireSo;
        [SerializeField] private static CapacitySo capacityFlashSo;
        
        // RAND 
        public static int getRand(int minValue, int maxValue)
        {
            return aleatoire.Next(minValue, maxValue);
        }
        
        // AGE
        public static bool AgeUpgrade()
        {
            if (Xp >= experienceStatSo.ExperienceLevel[Age])
            {
                eventInstance.UpgradeAge();
                Age++;
                return true;
            }
            return false;
        }
        
        // CAPACITY 
        public static bool SpecialCapacity(int index)
        {
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
        
        // SPAWN 
        public static bool Spawn(int index)
        {
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
        
        // UNLOCK UNIT 
        public static bool UnlockNewUnit()
        {
            // AJOUTER VERIF ARGENT + LOGIQUE 
            if (!IsUnlock)
            {
                IsUnlock = true;
            }

            return IsUnlock;
        }
        
        // TURRET
        public static bool Turret()
        {
            if (Gold >= turretStatSo.Price)
            {
                eventInstance.SpawnTurret(turretStatSo);
                TurretNumber++;
                return true;
            }
            return false;
        }
        
        // UPGRADE
        public static void Upgrade(UpgradeType upgradeType) // METTRE VERIFICATION DES PRIX (SCRIPTBLE OBJECT)
        {
            eventInstance.Upgrade(upgradeType);
        }
    }
}