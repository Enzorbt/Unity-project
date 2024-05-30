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
            
            var index = iaThinker.getRand(0, 5);
            switch (index)
            {
                case 0:
                    iaThinker.AgeUpgrade();
                    Debug.Log("AGE");
                    break;
                case 1:
                    iaThinker.SpecialCapacity(iaThinker.getRand(0, 1), false);
                    Debug.Log("SPECIAL");
                    break;
                case 2:
                    iaThinker.Spawn(iaThinker.getRand(0, 3));
                    Debug.Log("SPAWN");
                    break;
                case 3:
                    iaThinker.UnlockNewUnit();
                    Debug.Log("UNLOCK");
                    break;
                case 4:
                    iaThinker.Turret();
                    Debug.Log("TURRET");
                    break;
                case 5:
                    // IAThinker.Upgrade(UpgradeType);
                    break;
            }

            iaThinker.Gold += 5; 
            yield return new WaitForSeconds(delayTime);
            
            iaThinker.IsThinking = false;
        }
    }
}