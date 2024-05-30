// Capacité Type

using System.Collections.Generic;
using ScriptableObjects.Turret;
using ScriptableObjects.Unit;
using Supinfo.Project.Common;
using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using Supinfo.Project.Scripts.ScriptableObjects.Experience;
using UnityEngine;
using Random = System.Random;

namespace IA.Event
{
    public class IAThinker : ThinkerWithDelay
    {
        protected internal  float Gold {get; set;}
        protected internal  float Xp {get; set;}
        
        public Queue<UnitStatSo> PlayerUnits { get; set; }
        protected  int Age {get; set;}
        protected  int TurretNumber {get; set;}
        public bool IsUnlock {get; set;}

        public Dictionary<UpgradeType, int> UpgradeDict;

        private EventsFoundation _eventsFoundation;
        private Random aleatoire = new Random();
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

        private void Awake()
        {
            Gold = 2500;
            Xp = 1000;
            _eventsFoundation = GetComponent<EventsFoundation>();
        }

        private void Start()
        {
            PlayerUnits= new Queue<UnitStatSo>();
        }

        // RAND 
        public  int getRand(int minValue, int maxValue)
        {
            return aleatoire.Next(minValue, maxValue);
        }

        // DETECTION
        public int DetectUnitsAndAllies()
        {
            var unitsAndAllies = GameObject.FindGameObjectsWithTag("Unit,Allies");
            return unitsAndAllies.Length;
        }

        // AGE
        public bool AgeUpgrade()
        {
            if (Xp >= experienceStatSo.ExperienceLevel[Age])
            {
                _eventsFoundation.UpgradeAge();
                Age++;
                Xp -= experienceStatSo.ExperienceLevel[Age];
                return true;
            }
            return false;
        }
        
        // CAPACITY 
        public bool SpecialCapacity(int index, bool buff)
        {
            switch (index)
            {
                case 0: 
                    if (Xp >= (experienceStatSo.ExperienceLevel[Age]*30)/100)
                    {
                        _eventsFoundation.UseCapacity(capacityFireSo);
                        Xp -= (experienceStatSo.ExperienceLevel[Age] * 30) / 100;
                        if (buff)
                        {
                            Xp += (experienceStatSo.ExperienceLevel[Age] * 20) / 100;
                        }
                        return true;
                    }
                    return false;
                case 1:
                    if (Xp >= (experienceStatSo.ExperienceLevel[Age]*60)/100)
                    {
                        _eventsFoundation.UseCapacity(capacityFlashSo);
                        Xp -= (experienceStatSo.ExperienceLevel[Age] * 60) / 100;
                        if (buff)
                        {
                            Xp += (experienceStatSo.ExperienceLevel[Age]*40)/100;
                        }
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
                        _eventsFoundation.SpawnUnit(meleeStatSo);
                        Gold -= meleeStatSo.Price;
                        return true;
                    }
                    return false;
                case 1 : 
                    // SPAWN RANGE 
                    if (Gold >= rangeStatSo.Price)
                    {
                        _eventsFoundation.SpawnUnit(rangeStatSo);
                        Gold -= rangeStatSo.Price;
                        return true;
                    }
                    return false;
                case 2 : 
                    // SPAWN ARMOR
                    if (Gold >= armorStatSo.Price && IsUnlock)
                    {
                        _eventsFoundation.SpawnUnit(armorStatSo);
                        Gold -= armorStatSo.Price;
                        return true;
                    }
                    return false;
                case 3 : 
                    // SPAWN ANTI-ARMOR
                    if (Gold >= antiArmorStatSo.Price)
                    {
                        _eventsFoundation.SpawnUnit(antiArmorStatSo);
                        Gold -= antiArmorStatSo.Price;
                        return true;
                    }
                    return false;
            }

            return false;
        }
        
        // UNLOCK UNIT 
        public  bool UnlockNewUnit()
        {
            if (!IsUnlock && Gold >= 100)
            {
                IsUnlock = true;
                Gold -= 100;
            }

            return IsUnlock;
        }
        
        // TURRET
        public  bool Turret()
        {
            if (Gold >= turretStatSo.Price)
            {
                _eventsFoundation.SpawnTurret(turretStatSo);
                Gold -= turretStatSo.Price;
                TurretNumber++;
                return true;
            }
            return false;
        }
        
        // UPGRADE
        public  void Upgrade(UpgradeType upgradeType) // METTRE VERIFICATION DES PRIX (SCRIPTBLE OBJECT)
        {
            _eventsFoundation.Upgrade(upgradeType);
        }
        
        // SPAWN FOR DIFFICULT 
        public bool SpawnDifficult()
        {
            // COUNTER (Unité forte contre celle que le joeur pose)
            if (PlayerUnits.Peek().Type == antiArmorStatSo.Type.StrongAgainst && Gold >= antiArmorStatSo.Price) // ARMOR
            {
                _eventsFoundation.SpawnUnit(antiArmorStatSo);
                PlayerUnits.Dequeue();
                Gold -= antiArmorStatSo.Price;
                return true;
            }
            if (PlayerUnits.Peek().Type == rangeStatSo.Type.StrongAgainst && Gold >= rangeStatSo.Price) // ANTI ARMOR
            {
                _eventsFoundation.SpawnUnit(rangeStatSo);
                PlayerUnits.Dequeue();
                Gold -= rangeStatSo.Price;
                return true;
            }
            if (PlayerUnits.Peek().Type == meleeStatSo.Type.StrongAgainst && Gold >= meleeStatSo.Price) // RANGE
            {
                _eventsFoundation.SpawnUnit(meleeStatSo);
                PlayerUnits.Dequeue();
                Gold -= meleeStatSo.Price;
                return true;
            }
            if (PlayerUnits.Peek().Type == armorStatSo.Type.StrongAgainst && Gold >= armorStatSo.Price && IsUnlock) // MELEE
            {
                _eventsFoundation.SpawnUnit(armorStatSo);
                PlayerUnits.Dequeue();
                Gold -= armorStatSo.Price;
                return true;
            }
            
            // SI LE JOUER NE PLACE RIEN TANK (ARMOR + 2 RANGE)
            if (!PlayerUnits.Peek())
            {
                float goldTank = armorStatSo.Price + (rangeStatSo.Price)*2;
                if (Gold >= goldTank && IsUnlock)
                {
                    _eventsFoundation.SpawnUnit(armorStatSo);
                    _eventsFoundation.SpawnUnit(rangeStatSo);
                    _eventsFoundation.SpawnUnit(rangeStatSo);
                    Gold -= goldTank;
                }
                return true;
            }

            return false;
        }
        public void OnAlliesSpawn(Component sender, object data)
        {
            if(data is not UnitStatSo unitStatSo) return;
            PlayerUnits.Enqueue(unitStatSo);
        }

        public void OnRecieveGold(Component sender, object data)
        {
            if(data is not float gold) return;
            Gold += gold;
        }
        
        public void OnRecieveXp(Component sender, object data)
        {
            if(data is not float xp) return;
            Xp += xp;
        }
    }
}