using Common;
using Interfaces;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    [CreateAssetMenu(menuName = "Brains/UnitBrain")]
    public class UnitBrain : Brain
    {
        public override void Think(Thinker thinker)
        {
            if (thinker is not UnitThinker unitThinker) return;
            unitThinker.TryGetComponent(out IUnitDetection detection);
            var tags = unitThinker.transform.tag.Split(",");
            
            // Detection d'allies 
            var target = detection?.Detect(unitThinker.Direction, unitThinker.UnitAttack.Range, tags[1]=="Allies"?"Unit,Allies":"Unit,Enemies");
            if (target) return;
            
            // Detection d'enemies
            target = detection?.Detect(unitThinker.Direction, unitThinker.UnitAttack.Range, tags[1]=="Allies"?"Unit,Enemies":"Unit,Allies");
            if (target)
            {
                unitThinker.TryGetComponent(out IAttacker attacker);
                target.TryGetComponent(out IDamageable damageable);
                if(damageable is null)return;
                attacker?.Attack(unitThinker.UnitAttack.Damage, damageable);
                return;
            }
            
            // Detection de chateau 
            target = detection?.Detect(unitThinker.Direction, unitThinker.UnitAttack.Range, tags[1]=="Allies"?"Castle,Enemies":"Castle,Allies");
            if (target)
            {
                unitThinker.TryGetComponent(out IAttacker attacker);
                target.TryGetComponent(out IDamageable damageable);
                if(damageable is null)return;
                attacker?.Attack(unitThinker.UnitAttack.Damage, damageable);
                return;
            }
            
            // Mouvement basic
            if (target is null)
            {
                unitThinker.TryGetComponent(out IMovement movement);
                movement?.Move(unitThinker.Direction, unitThinker.UnitMovement.WalkSpeed);
            }
        }
    }
}