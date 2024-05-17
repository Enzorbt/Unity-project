using Common;
using Interfaces;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    public abstract class UnitBrain : Brain
    {
        protected abstract void Attack(UnitThinker unitThinker, Collider2D target);
    }
}