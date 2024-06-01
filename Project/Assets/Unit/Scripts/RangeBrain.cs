using Common;
using Interfaces;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    /// <summary>
    /// The RangeBrain class defines the behavior for ranged units.
    /// It extends the UnitBrain class and overrides the Think method to implement custom logic.
    /// </summary>
    [CreateAssetMenu(menuName = "Brains/RangeBrain")]
    public class RangeBrain : UnitBrain
    {
        /// <summary>
        /// Overrides the Think method to implement the ranged unit's behavior.
        /// </summary>
        /// <param name="thinker">The thinker object controlling the unit.</param>
        public override void Think(Thinker thinker)
        {
            if (thinker is not UnitThinker unitThinker) return;

            // Attempt to get the detection component
            unitThinker.TryGetComponent(out IUnitDetection detection);

            // Split the tags to determine the unit's affiliation
            var tags = unitThinker.transform.tag.Split(",");

            // Detect nearby enemies
            var target = detection?.Detect(unitThinker.Direction, 1, tags[1] == "Allies" ? "Unit,Enemies" : "Unit,Allies");
            if (target is not null)
            {
                unitThinker.TryGetComponent(out IShooter shooter);
                shooter?.Shoot(
                    unitThinker.Damage,
                    unitThinker.HitSpeed,
                    unitThinker.WalkSpeed + 1,
                    target.transform, unitThinker.UnitType);
                return;
            }

            // Detect distant enemies
            target = detection?.Detect(unitThinker.Direction, unitThinker.Range, tags[1] == "Allies" ? "Unit,Enemies" : "Unit,Allies");
            if (target is not null)
            {
                unitThinker.TryGetComponent(out IShooter shooter);
                shooter?.Shoot(
                    unitThinker.Damage,
                    unitThinker.HitSpeed,
                    unitThinker.WalkSpeed + 1,
                    target.transform, unitThinker.UnitType);
                return;
            }

            // Detect castles
            target = detection?.Detect(unitThinker.Direction, unitThinker.Range, tags[1] == "Allies" ? "Castle,Enemies" : "Castle,Allies");
            if (target is not null)
            {
                unitThinker.TryGetComponent(out IShooter shooter);
                shooter?.Shoot(
                    unitThinker.Damage,
                    unitThinker.HitSpeed,
                    unitThinker.WalkSpeed + 1,
                    target.transform, unitThinker.UnitType);
                return;
            }

            // Detect nearby allies
            target = detection?.Detect(unitThinker.Direction, 1, tags[1] == "Allies" ? "Unit,Allies" : "Unit,Enemies");
            if (target is not null) return;

            // Detect nearby castles
            target = detection?.Detect(unitThinker.Direction, 1, tags[1] == "Allies" ? "Castle,Enemies" : "Castle,Allies");
            if (target is not null) return;

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
            // Attempt to get the shooter component
            unitThinker.TryGetComponent(out IShooter shooter);

            // Perform the shoot action
            shooter?.Shoot(
                unitThinker.Damage,
                unitThinker.HitSpeed,
                unitThinker.WalkSpeed + 1,
                target.transform,
                unitThinker.UnitType);
        }
    }
}
