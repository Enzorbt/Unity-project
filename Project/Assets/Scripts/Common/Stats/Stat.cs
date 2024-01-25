using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Supinfo.Project.Scripts.Common.Stats
{
    [Serializable]
    public class Stat
    {
        [SerializeField] 
        private float baseValue;
        private float BaseValue
        {
            get => baseValue;
            set => baseValue = value;
        }

        private float _lastBaseValue = float.MinValue;
        private bool _isDirty = true;
        
        private float _value;
        public float GetValue() // or GetValue(int age = 0, int upgrades = 0)
        {
            // here we check if the value is dirty, if so we clean it
            if(_isDirty || Math.Abs(_lastBaseValue - BaseValue) > 1e-9) { // if the absolute value of A - B is more than tolerance (10 to the power -9 here), then we know the value is dirty
                Debug.Log("Value is calculated");
                _lastBaseValue = BaseValue;
                _value = CalculateFinalValue(_currentAge, _currentUpgrade);
                // or _value = CalculateFinalValue(age, upgrades);
                _isDirty = false; 
            }
            return _value;
        }
        
        // statModifiers shouldn't change and so we use StatModifier which is read-only, it is a reference and will be changed according to changes in statModifiers
        [SerializeField]
        private List<StatModifier> ageStatModifiers;
        public ReadOnlyCollection<StatModifier> AgeStatModifiers { get; }
        
        [SerializeField]
        private List<StatModifier> upgradeStatModifiers; // Collection that can modify
        public ReadOnlyCollection<StatModifier> UpgradeStatModifiers { get; } // readonly Collection to access the data

        // Age and Upgrade counts
        private int _currentAge = 0;
        public void CurrentAge(int age)
        {
            _currentAge = age;
        }

        private int _currentUpgrade = 0;
        public void CurrentUpgrade(int upgrade)
        {
            _currentUpgrade = upgrade;
        }
        
        // Constructors
        public Stat()
        {
            ageStatModifiers = new List<StatModifier>();
            AgeStatModifiers = ageStatModifiers.AsReadOnly(); // create a reference to ageStatModifiers that can only be read
            
            upgradeStatModifiers = new List<StatModifier>();
            UpgradeStatModifiers = upgradeStatModifiers.AsReadOnly();
        }

        public Stat(float baseValue) : this()
        {
            BaseValue = baseValue;
        }
        
        
        // for script base assignation 

        public virtual void AddModifier(StatModifier mod)
        {
            _isDirty = true;
            ageStatModifiers.Add(mod);
            ageStatModifiers.Sort(CompareModifierOrder);
        }
     
        public virtual bool RemoveModifier(StatModifier mod)
        {
            if (ageStatModifiers.Remove(mod))
            {
                _isDirty = true;
                return true;
            }
            return false;
        }
        
        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
                return -1;
            if (a.Order > b.Order)
                return 1;
            return 0; // if (a.Order == b.Order)
        }


        // for script stats access, age between 0 and X
        protected virtual float CalculateFinalValue(int age, int upgrades)
        {
            float finalValue = BaseValue;

            // if age or upgrades is greater than the number of modifiers in the list, set it to the max
            if (age > AgeStatModifiers.Count)
            {
                Debug.LogWarning("Age adjusted in the calculation value");
                age = AgeStatModifiers.Count;
            }

            if (upgrades > UpgradeStatModifiers.Count)
            {
                Debug.LogWarning("Upgrades number adjusted in the calculation value");
                upgrades = UpgradeStatModifiers.Count;
            }
            
            finalValue = CalculateValue(finalValue, AgeStatModifiers, age);
            finalValue = CalculateValue(finalValue, UpgradeStatModifiers, upgrades);
            
            // Rounding gets around float calculation errors 
            return finalValue;
        }

        private float CalculateValue(float val, ReadOnlyCollection<StatModifier> list, int number)
        {
            float tempValue = val;
            float sumPercentAdd = 0;
            
            for (int i = 0; i < number; i++)
            {
                StatModifier mod = list[i];

                if (mod.Type == StatModType.Flat)
                {
                    tempValue += mod.Value;
                }
                else if (mod.Type == StatModType.PercentMult)
                {
                    tempValue *= 1 + mod.Value;
                }
                else if (mod.Type == StatModType.PercentAdd)
                {
                    sumPercentAdd += mod.Value; // Start adding together all modifiers of this type

                    // If we're at the end of the list OR the next modifer isn't of this type
                    if (i + 1 >= list.Count || list[i + 1].Type != StatModType.PercentAdd)
                    {
                        tempValue *= 1 + sumPercentAdd; // Multiply the sum with the "finalValue", like we do for "PercentMult" modifiers
                        sumPercentAdd = 0; // Reset the sum back to 0
                    }
                }
            }

            return (float)Math.Round(tempValue, 4);
        }

        // to reset the game ?
        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            var wasRemoved = false;

            for (int i = ageStatModifiers.Count - 1; i >= 0; i--)
            {
                if (ageStatModifiers[i].Source == source)
                {
                    _isDirty = true;
                    wasRemoved = true;
                    ageStatModifiers.RemoveAt(i);
                }
            }
            
            return wasRemoved;
        }
        
        

    }
}

