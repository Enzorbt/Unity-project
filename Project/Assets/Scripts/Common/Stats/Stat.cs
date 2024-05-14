using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Serialization;

namespace Supinfo.Project.Scripts.Common.Stats
{
    [Serializable]
    public class Stat
    {
        private int _lastAge = 0;
        private int _lastUpgrade = 0;
        
        private bool _hasBeenCalculated = false;

        [SerializeField] 
        private float baseValue;
        
        private float _value;
        
        /// <summary>
        /// returns the value of the stat
        /// </summary>
        /// <param name="age">the current age</param>
        /// <param name="upgrades">the current upgrade count for this Stat</param>
        /// <returns></returns>
        public float GetValue(int age = 0, int upgrades = 0)
        {
            // here we check if the value is dirty, if so we clean it
            if(age != _lastAge || upgrades != _lastUpgrade || !_hasBeenCalculated) {
                _hasBeenCalculated = true;
                _lastAge = age;
                _lastUpgrade = upgrades;
                _value = CalculateFinalValue(age, upgrades);
            }
            return _value;
        }
        
        
        [SerializeField]
        private List<StatModifier> ageStatModifiers;
        
        [SerializeField]
        private List<StatModifier> upgradeStatModifiers;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="age">the current age</param>
        /// <param name="upgrades">the current upgrade count for this Stat</param>
        /// <returns></returns>
        protected virtual float CalculateFinalValue(int age, int upgrades)
        {
            float finalValue = baseValue;

            // if age or upgrades is greater than the number of modifiers in the list, set it to the max
            if (age > ageStatModifiers.Count)
            {
                age = ageStatModifiers.Count;
            }

            if (upgrades > upgradeStatModifiers.Count)
            {
                upgrades = upgradeStatModifiers.Count;
            }
            
            finalValue = CalculateValue(finalValue, ageStatModifiers, age);
            finalValue = CalculateValue(finalValue, upgradeStatModifiers, upgrades);
            
            // Rounding gets around float calculation errors 
            return finalValue;
        }

        private float CalculateValue(float val, List<StatModifier> list, int number)
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

                    // If we're at the last upgrade unlocked OR the next modifer isn't of this type
                    if (i + 1 == number || list[i + 1].Type != StatModType.PercentAdd)
                    {
                        tempValue *= 1 + sumPercentAdd; // Multiply the sum with the "finalValue", like we do for "PercentMult" modifiers
                        sumPercentAdd = 0; // Reset the sum back to 0
                    }
                }
            }

            return (float)Math.Round(tempValue, 4);
        }
        
        

    }
}

