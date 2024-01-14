using System;
using UnityEngine;

namespace Supinfo.Project.Stats
{
    [Serializable]
    public enum StatModType
    {
        Flat = 100,
        PercentAdd = 200,
        PercentMult = 300,
    }
    
    [Serializable]
    public class StatModifier
    {
        [SerializeField] private int value;
        public float Value { get; }
        [SerializeField] private StatModType type;
        public StatModType Type { get; }
        
        [SerializeField] private int order;
        public int Order { get; }
        private object source;
        public object Source { get; }
        
        
        
        public StatModifier(float value, StatModType type, int order, object source = null)
        {
            Value = value;
            Type = type;
            Order = order;
            Source = source;
        }
        
        // other constructors with pre determined values for order
        public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
    }
}