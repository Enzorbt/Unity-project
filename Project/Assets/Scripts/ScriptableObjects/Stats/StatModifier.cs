using System;
using UnityEngine;

namespace Supinfo.Project.Scripts.Stats
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
        Flat = 100,
        PercentAdd = 200,
        PercentMult = 300,
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
        public float Value { get; }

        /// <summary>
        /// The type of stat modification, defined in StatModType.
        /// </summary>
        [SerializeField] private StatModType type;
        public StatModType Type { get; }
        
        /// <summary>
        /// The order in which this modifier is applied among other modifiers.
        /// </summary>
        [SerializeField] private int order;
        public int Order { get; }

        /// <summary>
        /// The source of this modifier, can be used to identify where the modification comes from.
        /// </summary>
        private object source;
        public object Source { get; }
        
        /// <summary>
        /// Constructor for creating a StatModifier with specified value, type, order, and source.
        /// </summary>
        /// <param name="value">The modification value.</param>
        /// <param name="type">The type of modification.</param>
        /// <param name="order">The order of application.</param>
        /// <param name="source">The source of the modifier (optional).</param>
        public StatModifier(float value, StatModType type, int order, object source = null)
        {
            Value = value;
            Type = type;
            Order = order;
            Source = source;
        }
        
        /// <summary>
        /// Constructor for creating a StatModifier with specified value, type, and source, using the type's enum value as the order.
        /// </summary>
        /// <param name="value">The modification value.</param>
        /// <param name="type">The type of modification.</param>
        /// <param name="source">The source of the modifier.</param>
        public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
    }
}
