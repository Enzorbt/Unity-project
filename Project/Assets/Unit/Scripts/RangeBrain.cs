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
            
            // Detection d'enemies d'abord (car c'est un archer, il ne pas avancer)
            var target = detection?.Detect(unitThinker.Direction, unitThinker.UnitStatSo.Range, tags[1]=="Allies"?"Unit,Enemies":"Unit,Allies");
            if (target)
            {
                Attack(unitThinker, target);
                return;
            }
            
            // puis Detection d'allies 
            target = detection?.Detect(unitThinker.Direction, 1, tags[1]=="Allies"?"Unit,Allies":"Unit,Enemies");
            if (target) return;
            
            // Detection de chateau 
            target = detection?.Detect(unitThinker.Direction, unitThinker.UnitStatSo.Range, tags[1]=="Allies"?"Castle,Enemies":"Castle,Allies");
            if (target)
            {
                Attack(unitThinker, target);
                return;
            }
            
            // Mouvement basic
            unitThinker.TryGetComponent(out IMovement movement);
            movement?.Move(unitThinker.Direction, unitThinker.UnitStatSo.WalkSpeed);
        }
        
        protected override void Attack(UnitThinker unitThinker, Collider2D target)
        {
            var tags = unitThinker.transform.tag.Split(",");
            
            unitThinker.TryGetComponent(out IShooter shooter);
            shooter?.Shoot(unitThinker.UnitStatSo.Damage, tags[1]=="Allies"?"Unit,Allies":"Unit,Enemies", unitThinker.UnitStatSo.HitSpeed, tags[1]=="Allies"?Vector3.right : Vector3.left, 3);
        }
    }
}