using System;
using UnityEngine;

namespace Supinfo.Project.Scripts.ScriptableObjects.UnitTypes
{
    /// <summary>
    /// The UnitType class is a ScriptableObject that defines different types of units in the game.
    /// Each unit type has a unique name and a set of other unit types it is strong against.
    /// This allows for the creation of a diverse set of units with varying strengths and weaknesses.
    /// </summary>
    [CreateAssetMenu(fileName = "UnitType", menuName = "ScriptableObject/Units/UnitType", order = 2)]
    [Serializable]
    public class UnitType : ScriptableObject
    {
        /// <summary>
        /// The name of the unit type. This name is used to identify and differentiate between various unit types.
        /// </summary>
        [Header("UnitType")] 
        [SerializeField] private string _name;
        
        /// <summary>
        /// Array of UnitType objects that this unit type is strong against.
        /// This array can be used to determine combat effectiveness and strategy in the game,
        /// allowing certain unit types to have advantages over others.
        /// </summary>
        [Header("UnitTypes it is strong against")] 
        [SerializeField] private UnitType _strongAgainst;
        public UnitType StrongAgainst => _strongAgainst;
    }
}