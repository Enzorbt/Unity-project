using Common;
using Interfaces;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    [CreateAssetMenu(menuName = "Brains/MeleeBrain")]
    public class MeleeBrain : UnitBrain
    {
        public override void Think(Thinker thinker)
        {
            if (thinker is not UnitThinker unitThinker) return;
            unitThinker.TryGetComponent(out IUnitDetection detection);
            var tags = unitThinker.transform.tag.Split(",");
            
            // Detection d'allies 
            var target = detection?.Detect(unitThinker.Direction, 1, tags[1]=="Allies"?"Unit,Allies":"Unit,Enemies");
            if (target) return;
            
            // Detection d'enemies
            target = detection?.Detect(unitThinker.Direction, unitThinker.UnitStatSo.Range, tags[1]=="Allies"?"Unit,Enemies":"Unit,Allies");
            if (target)
            {
                Attack(unitThinker, target);
                return;
            }
            
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
            unitThinker.TryGetComponent(out IAttacker attacker);
            target.TryGetComponent(out IDamageable damageable);
            if(damageable is null)return;
            attacker?.Attack(unitThinker.UnitStatSo.Damage, damageable, unitThinker.UnitStatSo.HitSpeed);
        }
        
    }
}