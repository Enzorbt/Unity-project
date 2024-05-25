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
    }
}        