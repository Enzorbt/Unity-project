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
    /// <summary>
    /// Enumeration for unit types, IA unit choices.
    /// </summary>
    public enum UnitChoice
    {
        melee,
        range,
        armor,
        antiarmor
    };
    
    /// <summary>
    /// Enumeration for action choices.
    /// </summary>
    public enum ActionChoice
    {
        spawn,
        capacity,
        age,
        upgrade, 
        unlock,
        turret,
    };
    
    /// <summary>
    /// Enumeration for capacity types, hence capacity choices.
    /// </summary>
    public enum CapacityChoice
    {
        fire, 
        lightning,
    };
    
    /// <summary>
    /// Class tha contains all the method, to run the IA, by calling some game event.
    /// </summary>
    public class IAThinker : ThinkerWithDelay
    {
        // Call other class / script.
        /// <summary>
        /// EventsFoundation class, which manages game events for the AI.
        /// </summary>
        private EventsFoundation _eventsFoundation;
        
        /// <summary>
        /// Native Random class.
        /// </summary>
        private Random aleatoire = new Random(); 
        
        /// <summary>
        /// Declaration of IA Gold.
        /// </summary>
        protected internal float Gold
        {
            get => _gold;
            set // If gold is negative, it will be reset to 0.
            {
                _gold = value;
                if (_gold < value)
                {
                    _gold = 0;
                }
            }
        }
        private float _gold;

        /// <summary>
        /// Declaration of IA XP.
        /// </summary>
        protected internal float Xp
        {
            get => _xp; // If the XP is negative, it will be reset to 0.
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
        
        /// <summary>
        /// Age variable for AI scripts.
        /// </summary>
        private int Age {get; set;}
        
        /// <summary>
        /// Variable number of AI script turrets.
        /// </summary>
        public  int TurretNumber {get; set;}
        
        /// <summary>
        /// Locked unit verification variable for AI scripts.
        /// </summary>
        public bool IsUnlock {get; set;}
        
        /// <summary>
        /// Spawn counteur lets you manage spawn action times. 
        /// </summary>
        public int SpawnCounter { get; set; } 
        
        /// <summary>
        /// Upgrade counter allows you to manage the timing of improvement actions. 
        /// </summary>
        public int UpgradeCounter { get; set; }
        
        /// <summary>
        /// Age counter allows you to manage the times of actions for the passage to higher ages. 
        /// </summary>
        public int AgeCounter { get; set; }

        /// <summary>
        /// Turret counter to manage turret spawn action times.
        /// </summary>
        public int TurretCounter { get; set; } 

        /// <summary>
        /// Queue of allied units, used to find out the type of opposing units.
        /// </summary>
        public Queue<UnitStatSo> PlayerUnits { get; set; }
        
        /// <summary>
        /// Dictionaries that manage improvement types and their improvement indexes.
        /// </summary>
        private Dictionary<UpgradeType, int> upgradeDictionary = new Dictionary<UpgradeType, int>();
        
        // Retrieve So Stats directly from Unity (drag & drop).
        
        /// <summary>
        /// STATS SO UNIT 
        /// </summary>
        [SerializeField] public UnitStatSo meleeStatSo;
        [SerializeField] public UnitStatSo rangeStatSo;
        [SerializeField] public UnitStatSo armorStatSo;
        [SerializeField] public UnitStatSo antiArmorStatSo;

        /// <summary>
        /// STATS SO XP AGE 
        /// </summary>
        [SerializeField] public ExperienceStatSo experienceStatSo;

        /// <summary>
        /// STATS SO Turret
        /// </summary>
        [SerializeField] public TurretStatSo turretStatSo;

        /// <summary>
        /// STATS SO SPECIAL CAPACITY
        /// </summary>
        [SerializeField] public CapacitySo capacityFireSo;
        [SerializeField] public CapacitySo capacityFlashSo;

        /// <summary>
        /// STATS SO UPGRADE
        /// </summary>
        [SerializeField] public UpgradePricesSo upgradePricesSo;
        
        private void Awake()
        {
            // Instantiates XP and Gold values at the start of the game.
            Gold = 2500;
            Xp = 1000;
            
            // Get EventsFoundation to instaciate Game Events.
            _eventsFoundation = GetComponent<EventsFoundation>();

            for (int index = 0; index < 10; index++)
            {
                upgradeDictionary.Add((UpgradeType)index, 0);
            }
        }
        
        private void Start()
        {
            // A new unit tail for allied units.
            PlayerUnits= new Queue<UnitStatSo>();
        }

        // RAND 
        /// <summary>
        /// Return and "Random" Value Between two value.
        /// </summary>
        /// <param name="minValue">Minimum value chosen.</param>
        /// <param name="maxValue">Maximum value chosen.</param>
        /// <returns> The random value chosen.</returns>
        public int getRand(int minValue, int maxValue)
        {
            return aleatoire.Next(minValue, maxValue);
        }

        // DETECTION
        /// <summary>
        /// Function which detect unit allies, with tag and return the number of it (length).
        /// </summary>
        /// <returns>The length off the game object list.</returns>
        public int DetectUnitsAndAllies()
        {
            var unitsAndAllies = GameObject.FindGameObjectsWithTag("Unit,Allies"); // List game objects with the tag “Unit, Allies”.
            return unitsAndAllies.Length; // Returns the size of this list.
        }
        
        /// <summary>
        /// Function which detect unit enemies, with tag and return the number of it (length).
        /// </summary>
        /// <returns>The length off the game object list.</returns>
        public int DetectUnitsAndEnemies()
        {
            var unitsAndEnemies = GameObject.FindGameObjectsWithTag("Unit,Enemies"); // List game objects with the tag “Unit, Enemies”.
            return unitsAndEnemies.Length; // Returns the size of this list.
        }

        // UPGRADE AGE
        /// <summary>
        /// Upgrade and buy manager (gold subtraction) the next age, call the event for this action.
        /// </summary>
        /// <returns>True if is possible and false if is impossible.</returns>
        public bool AgeUpgrade()
        {
            if (Xp >= experienceStatSo.ExperienceLevel[Age]) // Verify that the price of the age upgrade is above IA gold.
            {
                IsUnlock = false; // Lock the lock unit for each age. 
                Xp -= experienceStatSo.ExperienceLevel[Age]; // Buy the age upgrade, buy subtract age upgrade price to IA gold.
                _eventsFoundation.UpgradeAge(); // Call the game event UpgradeAge.
                Age++; // Indente the age counter.
                return true; // Return true when all the action is finish, verify that the action was done.
            }
            return false; // Return false when all the action was not done.
        }
        
        // LAUNCH CAPACITY 
        /// <summary>
        /// Launch capacity by calling the event with capacity type and and buy manager (gold subtraction).
        /// </summary>
        /// <param name="capacityChoice">The type of capacity to use.</param>
        /// <param name="buff">If the price is a little bit reimbursed.</param>
        /// <returns>True if is possible and false if is impossible.</returns>
        public bool SpecialCapacity(CapacityChoice capacityChoice, bool buff)
        {
            switch (capacityChoice)
            {
                case CapacityChoice.fire: 
                    if (Xp >= (experienceStatSo.ExperienceLevel[Age]*30)/100) // Verify that the price of the capacity upgrade is above IA gold. 
                    {
                        Xp -= (experienceStatSo.ExperienceLevel[Age] * 30) / 100; // Buy the capacity, buy subtract capacity price to IA gold.
                        _eventsFoundation.UseCapacity(capacityFireSo); // Call the game event UseCapacity with the capacity type.
                        if (buff)
                        {
                            Xp += (experienceStatSo.ExperienceLevel[Age] * 20) / 100; // If is buff it refunded a part of the capacity cost.
                        }
                        return true; // Return true when all the action is finish, verify that the action was done.
                    }
                    return false; // Return false when all the action was not done.
                case CapacityChoice.lightning:
                    if (Xp >= (experienceStatSo.ExperienceLevel[Age]*60)/100) // Verify that the price of the capacity upgrade is above IA gold. 
                    {
                        Xp -= (experienceStatSo.ExperienceLevel[Age] * 60) / 100; // Buy the capacity, buy subtract capacity price to IA gold.
                        _eventsFoundation.UseCapacity(capacityFlashSo); // Call the game event UseCapacity with the capacity type.
                        if (buff)
                        {
                            Xp += (experienceStatSo.ExperienceLevel[Age]*40)/100; // If is buff it refunded a part of the capacity cost.
                        }
                        return true; // Return true when all the action is finish, verify that the action was done.
                    }
                    return false; // Return false when all the action was not done.
            } 
            return false; // Return false when all the action was not done.
        }
        
        // SPAWN UNIT
        /// <summary>
        /// Spawn unit by calling the event with unit type and buy manager (gold subtraction).
        /// </summary>
        /// <param name="unitChoice">The choice of type of the unit.</param>
        /// <param name="buff">If the price is reimbursed.</param>
        /// <returns>True if is possible and false if is impossible.</returns>
        public bool Spawn(UnitChoice unitChoice, bool buff)
        {
            switch (unitChoice)
            {
                case UnitChoice.melee : 
                    // SPAWN MELEE
                    if (Gold >= meleeStatSo.Price) // Verify that the price of the unit is above IA gold. 
                    {
                        Gold -= meleeStatSo.Price; // Buy the unit, buy subtract unit price to IA gold.
                        _eventsFoundation.SpawnUnit(meleeStatSo); // Call the game event SpawnUnit with the unit type.
                        return true; // Return true when all the action is finish, verify that the action was done.
                    }
                    if (buff)
                    {
                        _eventsFoundation.SpawnUnit(meleeStatSo); // If is buff call the game event SpawnUnit with the unit type, without paying.
                        return true; // Return true when all the action is finish, verify that the action was done.
                    }
                    return false; // Return false when all the action was not done.
                case UnitChoice.range : 
                    // SPAWN RANGE 
                    if (Gold >= rangeStatSo.Price) // Verify that the price of the unit is above IA gold. 
                    {
                        Gold -= rangeStatSo.Price; // Buy the unit, buy subtract unit price to IA gold.
                        _eventsFoundation.SpawnUnit(rangeStatSo); // Call the game event SpawnUnit with the unit type.
                        return true; // Return true when all the action is finish, verify that the action was done.
                    }
                    if (buff)
                    {
                        _eventsFoundation.SpawnUnit(rangeStatSo); // If is buff call the game event SpawnUnit with the unit type, without paying.
                        return true; // Return true when all the action is finish, verify that the action was done.
                    }
                    return false; // Return false when all the action was not done.
                case UnitChoice.armor : 
                    // SPAWN ARMOR
                    if (Gold >= armorStatSo.Price && IsUnlock) // Verify that the price of the unit is above IA gold. 
                    {
                        Gold -= armorStatSo.Price; // Buy the unit, buy subtract unit price to IA gold.
                        _eventsFoundation.SpawnUnit(armorStatSo); // Call the game event SpawnUnit with the unit type.
                        return true; // Return true when all the action is finish, verify that the action was done.
                    }
                    if (buff)
                    {
                        _eventsFoundation.SpawnUnit(armorStatSo); // If is buff call the game event SpawnUnit with the unit type, without paying.
                        return true; // Return true when all the action is finish, verify that the action was done.
                    }
                    return false; // Return false when all the action was not done.
                case UnitChoice.antiarmor : 
                    // SPAWN ANTI-ARMOR
                    if (Gold >= antiArmorStatSo.Price) // Verify that the price of the unit is above IA gold. 
                    {
                        Gold -= antiArmorStatSo.Price; // Buy the unit, buy subtract unit price to IA gold.
                        _eventsFoundation.SpawnUnit(antiArmorStatSo); // Call the game event SpawnUnit with the unit type.
                        return true; // Return true when all the action is finish, verify that the action was done.
                    }
                    if (buff)
                    {
                        _eventsFoundation.SpawnUnit(antiArmorStatSo); // If is buff call the game event SpawnUnit with the unit type, without paying.
                        return true; // Return true when all the action is finish, verify that the action was done.
                    }
                    return false; // Return false when all the action was not done.
            }

            return false;
        }
        
        // UNLOCK UNIT 
        /// <summary>
        /// Unlock unit by calling the event and buy manager (gold subtraction).
        /// </summary>
        /// <returns>True if is possible and false if is impossible.</returns>
        public bool UnlockNewUnit()
        {
            if (!IsUnlock && Gold >= 300) // Verify that the price of the unlock is above IA gold and IsUnlock is false, so that the IA should unlock it.
            {
                Gold -= 300; // Buy the unlock, buy subtract unlock price to IA gold.
                IsUnlock = true; // Set IsUnlock to true.
            }

            return IsUnlock; // Return IsUnlock state (true/false)
        }
        
        // TURRET
        /// <summary>
        /// Spawn turret by calling the event and buy manager (gold subtraction).
        /// </summary>
        /// <returns>True if is possible and false if is impossible.</returns>
        public bool Turret()
        {
            if (Gold >= turretStatSo.Price && TurretNumber <= 4) // Verify that the price of a turret is above IA gold and turret number is above or equal to 4.
            {
                Gold -= turretStatSo.Price;
                TurretNumber++; // Buy the turret, buy subtract turret price to IA gold.
                _eventsFoundation.SpawnTurret(turretStatSo); // Call the game event SpawnTurret with the turret stats so.
                return true; // Return true when all the action is finish, verify that the action was done.
            }
            return false; // Return false when all the action was not done.
        }
        
        // UPGRADE
        /// <summary>
        /// Upgrade unit stats by calling the event with upgrade type and buy manager (gold subtraction).
        /// </summary>
        /// <param name="upgradeType">The choice of type of the upgrade.</param>
        /// <returns>True if is possible and false if is impossible.</returns>
        public bool Upgrade(UpgradeType upgradeType)
        {
            // Verify that the price of an upgrade is above IA gold and upgrade index is above 4.
            if (upgradeDictionary[upgradeType] < 3 && upgradePricesSo.GetPrice(upgradeType, upgradeDictionary[upgradeType]) >= Gold)
            {
                Gold -= upgradePricesSo.GetPrice(upgradeType, upgradeDictionary[upgradeType]); // Buy the upgrade, buy subtract upgrade price to IA gold.
                upgradeDictionary[upgradeType]++; // Indente the upgrade counter.
                _eventsFoundation.Upgrade(upgradeType); // Call the game event Upgrade with the upgrade type.
                return true; // Return true when all the action is finish, verify that the action was done.
            }        
            return false; // Return false when all the action was not done.
        }
        
        /// <summary>
        /// Game event listener function called when the event OnAlliesSpawn is triggered (linked to a GameEventListener component).
        /// </summary>
        /// <param name="sender">The sender of the game event.</param>
        /// <param name="data">The data being transferred.</param>
        public void OnAlliesSpawn(Component sender, object data)
        {
            if(data is not UnitStatSo unitStatSo) return;
            PlayerUnits.Enqueue(unitStatSo);
        }

        /// <summary>
        /// Game event listener function called when the event OnRecieveGold is triggered (linked to a GameEventListener component).
        /// Get the win gold from the game (unit enemies death...).
        /// </summary>
        /// <param name="sender">The sender of the game event.</param>
        /// <param name="data">The data being transferred.</param>
        public void OnRecieveGold(Component sender, object data)
        {
            if(data is not float gold) return; // If the data receive is not gold return
            Gold += gold; // Add the gold receive to IA gold.
        }
        
        /// <summary>
        /// Game event listener function called when the event OnRecieveXp is triggered (linked to a GameEventListener component).
        /// Get the win XP from the game (unit enemies death...).
        /// </summary>
        /// <param name="sender">The sender of the game event.</param>
        /// <param name="data">The data being transferred.</param>
        public void OnRecieveXp(Component sender, object data)
        {
            if(data is not float xp) return; // If the data receive is not XP return
            Xp += xp; // Add the XP receive to IA XP.
        }
    }
}