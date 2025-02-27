using System.Collections;
using Supinfo.Project.Common;
using Supinfo.Project.Scripts;
using UnityEngine;

namespace IA.Event
{
    [CreateAssetMenu(menuName = "Brains/IABrain1")]
    public class IABrain1 : BrainWithDelay
    {
        public override IEnumerator ThinkWithDelay(ThinkerWithDelay thinker)
        {
            if (thinker is not IAThinker iaThinker)yield break;
            iaThinker.IsThinking = true;
            
            var actionChoice = (ActionChoice)iaThinker.getRand(0, 5);
            switch (actionChoice)
            {
                case ActionChoice.age: // UPGRADE AGE
                    iaThinker.AgeUpgrade();
                    break;
                case ActionChoice.capacity: // LAUCH CAPACITY
                    iaThinker.SpecialCapacity((CapacityChoice)iaThinker.getRand(0, 1), false);
                    break;
                case ActionChoice.spawn: // SPAWN UNIT
                    iaThinker.Spawn((UnitChoice)iaThinker.getRand(0, 3), false);
                    yield return new WaitForSeconds(0.01f);
                    break;
                case ActionChoice.unlock: // UNLOCK UNIT
                    iaThinker.UnlockNewUnit();
                    break;
                case ActionChoice.turret: // TURRET
                    iaThinker.Turret();
                    break;
                case ActionChoice.upgrade: // UPGRADE
                    if (iaThinker.UpgradeCounter > 20)
                    {
                        iaThinker.Upgrade((UpgradeType)iaThinker.getRand(0, 10));
                        iaThinker.UpgradeCounter = 0;
                    }
                    iaThinker.UpgradeCounter++;
                    break;
            }

            iaThinker.Gold += 10; 
            
            yield return new WaitForSeconds(delayTime);
            
            iaThinker.IsThinking = false;
        }
    }
}