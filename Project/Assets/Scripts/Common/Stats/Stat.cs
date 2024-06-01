using System;
using System.Collections.Generic;
using UnityEngine;

namespace Supinfo.Project.Scripts.Common.Stats
{
    /// <summary>
    /// Holds all the information of a given statistic, like its base value and its modifiers.
    /// </summary>
    [Serializable]
    public class Stat
    {
        /// <summary>
        /// The last age that was use.
        /// </summary>
        private int _lastAge = 0;
        
        /// <summary>
        /// The last upgrade index that was use.
        /// </summary>
        private int _lastUpgrade = 0;
        
        /// <summary>
        /// State of the calculation, true if already calculated, false otherwise.
        /// </summary>
        private bool _hasBeenCalculated = false;

        /// <summary>
        /// The base value of the statistic.
        /// </summary>
        [SerializeField] private float baseValue;
        
        /// <summary>
        /// The current value of the statistic.
        /// </summary>
        private float _value;
        
        /// <summary>
        /// returns the value of the stat
        /// </summary>
        /// <param name="age">the current age</param>
        /// <param name="upgrades">the current upgrade count for this Stat</param>
        /// <returns>The value of the statistic.</returns>
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
        
        /// <summary>
        /// The list of age modifiers.
        /// </summary>
        [SerializeField] private List<StatModifier> ageStatModifiers;
        
        /// <summary>
        /// The list of upgrade modifiers.
        /// </summary>
        [SerializeField] private List<StatModifier> upgradeStatModifiers;

        /// <summary>
        /// Calculates the final value for the statistic.
        /// </summary>
        /// <param name="age">the current age</param>
        /// <param name="upgrades">the current upgrade count for this Stat</param>
        /// <returns>The calculated value</returns>
        protected virtual float CalculateFinalValue(int age, int upgrades)
        {
            float finalValue = baseValue;

            // if age or upgrades is greater than the number of modifiers in the list, set it to the max
            if (age >= ageStatModifiers.Count)
            {
                age = ageStatModifiers.Count - 1;
            }

            if (upgrades >= upgradeStatModifiers.Count)
            {
                upgrades = upgradeStatModifiers.Count - 1;
            }
            
            finalValue = CalculateValue(finalValue, ageStatModifiers, age);
            finalValue = CalculateValue(finalValue, upgradeStatModifiers, upgrades);
            
            // Rounding gets around float calculation errors 
            return finalValue;
        }

        /// <summary>
        /// Calculate value for a given value and modifiers to apply.
        /// </summary>
        /// <param name="val">The value to modify.</param>
        /// <param name="list">The list of modifiers to use.</param>
        /// <param name="number">The number of modifiers to take into account.</param>
        /// <returns>The calculated value.</returns>
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

