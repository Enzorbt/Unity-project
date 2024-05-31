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

        public int SpawnCounter { get; set; }
        public int UpgradeCounter { get; set; }
        
        public Queue<UnitStatSo> PlayerUnits { get; set; }
        private int Age {get; set;}
        public  int TurretNumber {get; set;}
        public bool IsUnlock {get; set;}
        
        public Dictionary<UpgradeType, int> UpgradeDict;

        private EventsFoundation _eventsFoundation;
        private Random aleatoire = new Random();

        private Dictionary<UpgradeType, int> upgradeDictionary = new Dictionary<UpgradeType, int>();
        
        public ActionChoice Action { get; set; }
        
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

            for (int index = 0; index < 10; index++)
            {
                upgradeDictionary.Add((UpgradeType)index, 0);
            }

            Action = ActionChoice.turret;
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

        // UPGRADE AGE
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
        
        // LAUNCH CAPACITY 
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
        
        // SPAWN UNIT
        public bool Spawn(UnitChoice unitChoice, bool buff)
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
                    if (buff == true)
                    {
                        _eventsFoundation.SpawnUnit(meleeStatSo);
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
                    if (buff == true)
                    {
                        _eventsFoundation.SpawnUnit(rangeStatSo);
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
                    if (buff == true)
                    {
                        _eventsFoundation.SpawnUnit(armorStatSo);
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
                    if (buff == true)
                    {
                        _eventsFoundation.SpawnUnit(antiArmorStatSo);
                        return true;
                    }
                    return false;
            }

            return false;
        }
        
        // UNLOCK UNIT 
        public  bool UnlockNewUnit()
        {
            if (!IsUnlock && Gold >= 300)
            {
                IsUnlock = true;
                Gold -= 300;
            }

            return IsUnlock;
        }
        
        // TURRET
        public bool Turret()
        {
            if (Gold >= turretStatSo.Price && TurretNumber <= 4)
            {
                _eventsFoundation.SpawnTurret(turretStatSo);
                Gold -= turretStatSo.Price;
                TurretNumber++;
                return true;
            }
            return false;
        }
        
        // UPGRADE
        public bool Upgrade(UpgradeType upgradeType)
        {
            if (upgradeDictionary[upgradeType] < 3 && upgradePricesSo.GetPrice(upgradeType, upgradeDictionary[upgradeType]) >= Gold)
            {
                Gold -= upgradePricesSo.GetPrice(upgradeType, upgradeDictionary[upgradeType]);
                upgradeDictionary[upgradeType]++;
                _eventsFoundation.Upgrade(upgradeType);
                Debug.Log("upgraded " + upgradeType);
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