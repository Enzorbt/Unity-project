// UPGRADE 

using System.Collections;
using Supinfo.Project.Common;
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
                case ActionChoice.age:
                    iaThinker.AgeUpgrade();
                    Debug.Log("AGE");
                    break;
                case ActionChoice.capacity:
                    iaThinker.SpecialCapacity((CapacityChoice)iaThinker.getRand(0, 1), false);
                    Debug.Log("SPECIAL");
                    break;
                case ActionChoice.spawn:
                    iaThinker.Spawn((UnitChoice)iaThinker.getRand(0, 3));
                    Debug.Log("SPAWN");
                    break;
                case ActionChoice.unlock:
                    iaThinker.UnlockNewUnit();
                    Debug.Log("UNLOCK");
                    break;
                case ActionChoice.turret:
                    iaThinker.Turret();
                    Debug.Log("TURRET");
                    break;
                case ActionChoice.upgrade:
                    // IAThinker.Upgrade(UpgradeType);
                    break;
            }

            iaThinker.Gold += 5; 
            yield return new WaitForSeconds(delayTime);
            
            iaThinker.IsThinking = false;
        }
    }
}