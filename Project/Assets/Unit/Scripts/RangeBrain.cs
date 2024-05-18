using Common;
using Interfaces;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    [CreateAssetMenu(menuName = "Brains/RangeBrain")]
    public class RangeBrain : UnitBrain
    {
        
        public override void Think(Thinker thinker)
        {
            if (thinker is not UnitThinker unitThinker) return;
            unitThinker.TryGetComponent(out IUnitDetection detection);
            var tags = unitThinker.transform.tag.Split(",");
            
            // Detection d'enemies (proche) 
            var target = detection?.Detect(unitThinker.Direction, 1, tags[1]=="Allies"?"Unit,Enemies":"Unit,Allies");
            if (target is not null)
            {
                unitThinker.TryGetComponent(out IShooter shooter);
                shooter?.Shoot(
                    unitThinker.UnitStatSo.Damage,
                    unitThinker.UnitStatSo.HitSpeed, 
                    2, 
                    target.transform);
                return;
            }
            
            // Detection d'enemies (loin)
            target = detection?.Detect(unitThinker.Direction, unitThinker.UnitStatSo.Range, tags[1]=="Allies"?"Unit,Enemies":"Unit,Allies");
            if (target is not null)
            {
                unitThinker.TryGetComponent(out IShooter shooter);
                shooter?.Shoot(
                    unitThinker.UnitStatSo.Damage,
                    unitThinker.UnitStatSo.HitSpeed, 
                    2, 
                    target.transform);
            }
            
            // Detection de chateau 
            target = detection?.Detect(unitThinker.Direction, unitThinker.UnitStatSo.Range, tags[1]=="Allies"?"Castle,Enemies":"Castle,Allies");
            if (target is not null)
            {
                unitThinker.TryGetComponent(out IShooter shooter);
                shooter?.Shoot(
                    unitThinker.UnitStatSo.Damage,
                    unitThinker.UnitStatSo.HitSpeed, 
                    2, 
                    target.transform);
            }
            
            // Detection d'allies
            target = detection?.Detect(unitThinker.Direction, 1, tags[1]=="Allies"?"Unit,Allies":"Unit,Enemies");
            if (target is not null) return;
            
            // Detection de chateau (Proche)
            target = detection?.Detect(unitThinker.Direction, 1, tags[1]=="Allies"?"Castle,Enemies":"Castle,Allies");
            if (target is not null) return;
            
            // Mouvement basic
            unitThinker.TryGetComponent(out IMovement movement);
            movement?.Move(unitThinker.Direction, unitThinker.UnitStatSo.WalkSpeed);
        }

        protected override void Attack(UnitThinker unitThinker, Collider2D target)
        {
            unitThinker.TryGetComponent(out IShooter shooter);
            shooter?.Shoot(
                unitThinker.UnitStatSo.Damage,
                unitThinker.UnitStatSo.HitSpeed, 
                2, 
                target.transform);
        }
    }
}