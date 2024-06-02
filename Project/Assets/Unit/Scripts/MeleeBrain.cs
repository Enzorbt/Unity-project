using Common;
using Interfaces;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    /// <summary>
    /// The MeleeBrain class defines the behavior for melee units.
    /// It extends the UnitBrain class and overrides the Think method to implement custom logic.
    /// </summary>
    [CreateAssetMenu(menuName = "Brains/MeleeBrain")]
    public class MeleeBrain : UnitBrain
    {
        /// <summary>
        /// Overrides the Think method to implement the melee unit's behavior.
        /// </summary>
        /// <param name="thinker">The thinker object controlling the unit.</param>
        public override void Think(Thinker thinker)
        {
            if (thinker is not UnitThinker unitThinker) return;

            // Attempt to get the detection component
            unitThinker.TryGetComponent(out IUnitDetection detection);

            // Split the tags to determine the unit's affiliation
            var tags = unitThinker.transform.tag.Split(",");

            // Detect allies
            var target = detection?.Detect(unitThinker.Direction, 1, tags[1] == "Allies" ? "Unit,Allies" : "Unit,Enemies");
            if (target) return;

            // Detect enemies
            target = detection?.Detect(unitThinker.Direction, unitThinker.Range, tags[1] == "Allies" ? "Unit,Enemies" : "Unit,Allies");
            if (target)
            {
                Attack(unitThinker, target);
                return;
            }

            // Detect castles
            target = detection?.Detect(unitThinker.Direction, unitThinker.Range, tags[1] == "Allies" ? "Castle,Enemies" : "Castle,Allies");
            if (target)
            {
                Attack(unitThinker, target);
                return;
            }

            // Basic movement
            unitThinker.TryGetComponent(out IMovement movement);
            movement?.Move(unitThinker.Direction, unitThinker.WalkSpeed);
        }

        /// <summary>
        /// Performs an attack on the specified target.
        /// </summary>
        /// <param name="unitThinker">The thinker object controlling the unit.</param>
        /// <param name="target">The target to be attacked.</param>
        protected override void Attack(UnitThinker unitThinker, Collider2D target)
        {
            // Attempt to get the attacker component
            unitThinker.TryGetComponent(out IAttacker attacker);

            // Attempt to get the damageable component from the target
            target.TryGetComponent(out IDamageable damageable);
            if (damageable is null) return;

            // Perform the attack
            attacker?.Attack(unitThinker.Damage, damageable, unitThinker.HitSpeed, unitThinker.UnitType);
        }
    }
}
