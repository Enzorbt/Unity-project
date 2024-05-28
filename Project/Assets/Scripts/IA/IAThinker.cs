using System.Collections.Generic;
using Common;
using ScriptableObjects.Turret;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using Supinfo.Project.Scripts.ScriptableObjects.Experience;
using UnityEngine;
using Random = System.Random;
// Capacité Type

namespace IA.Event
{
    public abstract class IAThinker : Thinker
    {
        protected internal  float Gold {get; set;}
        protected internal  float Xp {get; set;}
        
        public UnitStatSo PlayerUnit { get; set; }
        protected  int Age {get; set;}
        protected  int TurretNumber {get; set;}
        protected  bool IsUnlock {get; set;}

        public Dictionary<UpgradeType, int> UpgradeDict; // Unity

        public EventsFoundation eventInstance = new EventsFoundation();
         Random aleatoire = new Random();
        public int index;
        
        // STATS SO UNIT
        [SerializeField] public UnitStatSo meleeStatSo;
        [SerializeField] public  UnitStatSo rangeStatSo;
        [SerializeField] public  UnitStatSo armorStatSo;
        [SerializeField] public  UnitStatSo antiArmorStatSo;

        // STATS SO XP AGE 
        [SerializeField] public  ExperienceStatSo experienceStatSo;

        // STATS SO Turret
        [SerializeField] public  TurretStatSo turretStatSo;

        // STATS SO SPECIAL CAPACITY
        [SerializeField] public  CapacitySo capacityFireSo;
        [SerializeField] public  CapacitySo capacityFlashSo;
        
        // RAND 
        public  int getRand(int minValue, int maxValue)
        {
            return aleatoire.Next(minValue, maxValue);
        }
        
        // AGE
        public  bool AgeUpgrade()
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
        public  bool SpecialCapacity(int index)
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
        public bool Spawn(int index)
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
        public  bool UnlockNewUnit()
        {
            // AJOUTER VERIF ARGENT + LOGIQUE 
            if (!IsUnlock)
            {
                IsUnlock = true;
            }

            return IsUnlock;
        }
        
        // TURRET
        public  bool Turret()
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
        public  void Upgrade(UpgradeType upgradeType) // METTRE VERIFICATION DES PRIX (SCRIPTBLE OBJECT)
        {
            eventInstance.Upgrade(upgradeType);
        }
        
        // SPAWN FOR DIFFICULT 
        public bool SpawnDifficult(UnitStatSo playerUnit)
        {
            // COUNTER (Unité forte contre celle que le joeur pose)
            if (playerUnit.Type == antiArmorStatSo.Type.StrongAgainst && Gold >= antiArmorStatSo.Price) // ARMOR
            {
                eventInstance.SpawnUnit(antiArmorStatSo);
                return true;
            }
            if (playerUnit.Type == rangeStatSo.Type.StrongAgainst && Gold >= rangeStatSo.Price) // ANTI ARMOR
            {
                eventInstance.SpawnUnit(rangeStatSo);
                return true;
            }
            if (playerUnit.Type == meleeStatSo.Type.StrongAgainst && Gold >= meleeStatSo.Price) // RANGE
            {
                eventInstance.SpawnUnit(meleeStatSo);
                return true;
            }
            if (playerUnit.Type == armorStatSo.Type.StrongAgainst && Gold >= armorStatSo.Price) // MELEE
            {
                eventInstance.SpawnUnit(armorStatSo);
                return true;
            }
            
            // SI LE JOUER NE PLACE TANK (ARMOR + 2 RANGE)
            if (playerUnit == null)
            {
                float goldTank = armorStatSo.Price + (rangeStatSo.Price)*2;
                if (Gold >= goldTank)
                {
                    eventInstance.SpawnUnit(armorStatSo);
                    eventInstance.SpawnUnit(rangeStatSo);
                    eventInstance.SpawnUnit(rangeStatSo);   
                }
                return true;
            }

            return false;
        }
        public void onAlliesSpawn(Component sender, object data)
        {
            if(data is not UnitStatSo unitStatSo) return;
            PlayerUnit = unitStatSo;
        }
    }
}