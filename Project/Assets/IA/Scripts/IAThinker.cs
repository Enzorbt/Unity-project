// Capacité Type

using System;
using System.Collections.Generic;
using ScriptableObjects.Turret;
using ScriptableObjects.Unit;
using Supinfo.Project.Common;
using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using Supinfo.Project.Scripts.ScriptableObjects.Experience;
using Supinfo.Project.Scripts.ScriptableObjects.Upgrades;
using UnityEngine;
using Random = System.Random;

namespace IA.Event
{
    public enum UnitChoice
    {
        melee,
        range,
        armor,
        antiarmor
    };
    
    public enum ActionChoice
    {
        spawn,
        capacity,
        age,
        upgrade, 
        unlock,
        turret,
    };
    
    public enum CapacityChoice
    {
        fire, 
        lightning,
    };
    
    public class IAThinker : ThinkerWithDelay
    {
        protected internal float Gold
        {
            get => _gold;
            set
            {
                _gold = value;
                if (_gold < value)
                {
                    _gold = 0;
                }
            }
        }
        private float _gold;

        protected internal float Xp
        {
            get => _xp;
            set
            {
                _xp = value;
                if (_xp < value)
                {
                    _xp = 0;
                }
            }
        }
        private float _xp;

        
        public Queue<UnitStatSo> PlayerUnits { get; set; }
        protected  int Age {get; set;}
        protected  int TurretNumber {get; set;}
        public bool IsUnlock {get; set;}
        
        public Dictionary<UpgradeType, int> UpgradeDict;

        private EventsFoundation _eventsFoundation;
        private Random aleatoire = new Random();
        
        // STATS SO UNIT
        [SerializeField] public UnitStatSo meleeStatSo;
        [SerializeField] public UnitStatSo rangeStatSo;
        [SerializeField] public UnitStatSo armorStatSo;
        [SerializeField] public UnitStatSo antiArmorStatSo;

        // STATS SO XP AGE 
        [SerializeField] public ExperienceStatSo experienceStatSo;

        // STATS SO Turret
        [SerializeField] public TurretStatSo turretStatSo;

        // STATS SO SPECIAL CAPACITY
        [SerializeField] public CapacitySo capacityFireSo;
        [SerializeField] public CapacitySo capacityFlashSo;

        // STATS SO UPGRADE
        [SerializeField] public UpgradePricesSo upgradePricesSo;
        
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
                IsUnlock = false;
                Xp -= experienceStatSo.ExperienceLevel[Age];
                Age++;
                return true;
            }
            return false;
        }
        
        // CAPACITY 
        public bool SpecialCapacity(CapacityChoice capacityChoice, bool buff)
        {
            switch (capacityChoice)
            {
                case CapacityChoice.fire: 
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
                case CapacityChoice.lightning:
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
        public bool Spawn(UnitChoice unitChoice)
        {
            switch (unitChoice)
            {
                case UnitChoice.melee : 
                    // SPAWN MELEE
                    if (Gold >= meleeStatSo.Price)
                    {
                        _eventsFoundation.SpawnUnit(meleeStatSo);
                        Gold -= meleeStatSo.Price;
                        return true;
                    }
                    return false;
                case UnitChoice.range : 
                    // SPAWN RANGE 
                    if (Gold >= rangeStatSo.Price)
                    {
                        _eventsFoundation.SpawnUnit(rangeStatSo);
                        Gold -= rangeStatSo.Price;
                        return true;
                    }
                    return false;
                case UnitChoice.armor : 
                    // SPAWN ARMOR
                    if (Gold >= armorStatSo.Price && IsUnlock)
                    {
                        _eventsFoundation.SpawnUnit(armorStatSo);
                        Gold -= armorStatSo.Price;
                        return true;
                    }
                    return false;
                case UnitChoice.antiarmor : 
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
        public bool Turret()
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
        public bool Upgrade(UpgradeType upgradeType) // METTRE VERIFICATION DES PRIX (SCRIPTBLE OBJECT)
        {
            switch (upgradeType)
            {
                case UpgradeType.MeleeAttack :
                    if (Gold >= 100)
                    {
                        _eventsFoundation.Upgrade(UpgradeType.MeleeAttack);
                        return true;
                    }
                    return false;
                case UpgradeType.MeleeHealth :
                    if (Gold >= 100)
                    {
                        _eventsFoundation.Upgrade(UpgradeType.MeleeHealth);
                        return true;
                    }
                    return false;
                case UpgradeType.RangeAttack :
                    if (Gold >= 100)
                    {
                        _eventsFoundation.Upgrade(UpgradeType.RangeAttack);
                        return true;
                    }
                    return false;
                case UpgradeType.RangeRange :
                    if (Gold >= 100)
                    {
                        _eventsFoundation.Upgrade(UpgradeType.RangeRange);
                        return true;
                    }
                    return false;
                case UpgradeType.AntiArmorAttack :
                    if (Gold >= 100)
                    {
                        _eventsFoundation.Upgrade(UpgradeType.AntiArmorAttack);
                        return true;
                    }
                    return false;
                case UpgradeType.AntiArmorHealth :
                    if (Gold >= 100)
                    {
                        _eventsFoundation.Upgrade(UpgradeType.AntiArmorHealth);
                        return true;
                    }
                    return false;
                case UpgradeType.ArmorAttack :
                    if (Gold >= 100)
                    {
                        _eventsFoundation.Upgrade(UpgradeType.ArmorAttack);
                        return true;
                    }
                    return false;
                case UpgradeType.ArmorHealth :
                    if (Gold >= 100)
                    {
                        _eventsFoundation.Upgrade(UpgradeType.ArmorHealth);
                        return true;
                    }
                    return false;
                case UpgradeType.TurretAttack :
                    if (Gold >= 100)
                    {
                        _eventsFoundation.Upgrade(UpgradeType.TurretAttack);
                        return true;
                    }
                    return false;
                case UpgradeType.TurretRange :
                    if (Gold >= 100)
                    {
                        _eventsFoundation.Upgrade(UpgradeType.TurretRange);
                        return true;
                    }
                    return false;
                case UpgradeType.GoldGiven :
                    if (Gold >= 100)
                    {
                        _eventsFoundation.Upgrade(UpgradeType.GoldGiven);
                        return true;
                    }
                    return false;
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