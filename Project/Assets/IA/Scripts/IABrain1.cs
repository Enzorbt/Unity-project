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
            Debug.Log("Xp : " + iaThinker.Xp);
            Debug.Log("Gold : " +iaThinker.Gold);       
            iaThinker.IsThinking = true;
            
            var actionChoice = (ActionChoice)iaThinker.getRand(0, 5);
            switch (actionChoice)
            {
                case ActionChoice.age:
                    iaThinker.AgeUpgrade();
                    break;
                case ActionChoice.capacity:
                    iaThinker.SpecialCapacity((CapacityChoice)iaThinker.getRand(0, 1), false);
                    break;
                case ActionChoice.spawn:
                    iaThinker.Spawn((UnitChoice)iaThinker.getRand(0, 3));
                    break;
                case ActionChoice.unlock:
                    iaThinker.UnlockNewUnit();
                    break;
                case ActionChoice.turret:
                    iaThinker.Turret();
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