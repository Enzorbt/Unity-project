// Attack : Stratégie COUNTER + TANK (Armor + RANGE) 


// VERIF PRIX / XP 

using ScriptableObjects.Turret;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using Supinfo.Project.Scripts.ScriptableObjects.Experience;
using UnityEngine;

namespace IA.Event
{
    public class IAThinker4 : IAThinker
    {
        private EventsFoundation eventInstance = new EventsFoundation();
        
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
        
        public bool UnlockNewUnit()
        {
            // AJOUTER VERIF ARGENT + LOGIQUE 
            if (!IsUnlock)
            {
                IsUnlock = true;
            }

            return IsUnlock;
        }
        
        public bool Turret()
        {
            if (Gold >= turretStatSo.Price)
            {
                eventInstance.SpawnTurret(turretStatSo);
                TurretNumber++;
                return true;
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

        public bool SpecialCapacity()
        {
            if (Xp >= (experienceStatSo.ExperienceLevel[Age]*30)/100)
            {
                eventInstance.UseCapacity(capacityFireSo);
                return true;
            }

            return false;
        }
        
        public void Upgrade(UpgradeType upgradeType) // METTRE VERIFICATION DES PRIX (SCRIPTBLE OBJECT)
        {
            eventInstance.Upgrade(upgradeType);
        }
        
        public bool Spawn(UnitStatSo playerUnit)
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
    }
}        