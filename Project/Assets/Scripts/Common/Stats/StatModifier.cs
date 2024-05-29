using System;
using UnityEngine;

namespace Supinfo.Project.Scripts.Common.Stats
{
    /// <summary>
    /// Enumeration representing the type of modification applied by a StatModifier.
    /// Flat: Adds a flat value.
    /// PercentAdd: Adds a percentage value, which is additive.
    /// PercentMult: Multiplies by a percentage value.
    /// </summary>
    [Serializable]
    public enum StatModType
    {
        Flat,
        PercentAdd,
        PercentMult,
    }
    
    /// <summary>
    /// Class representing a modifier for a stat.
    /// It includes the value of the modification, the type of modification, the order of application, and the source of the modification.
    /// </summary>
    [Serializable]
    public class StatModifier
    {
        /// <summary>
        /// The value of the stat modification.
        /// </summary>
        [SerializeField] private int value;

        public float Value => value;

        /// <summary>
        /// The type of stat modification, defined in StatModType.
        /// </summary>
        [SerializeField] private StatModType type;
        public StatModType Type => type;
        
        /// <summary>
        /// The order in which this modifier is applied among other modifiers.
        /// </summary>
        [SerializeField] private int order;
        public int Order => order;

        /// <summary>
        /// The source of this modifier, can be used to identify where the modification comes from.
        /// </summary>
        private object source;
        public object Source { get; }
    }
}
