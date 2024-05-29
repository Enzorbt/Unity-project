using System.Collections;
using Common;
using Supinfo.Project.Common;
using Supinfo.Project.Scripts;
using UnityEngine;

// UPGRADE 

namespace IA.Event
{
    [CreateAssetMenu(menuName = "Brains/IABrain1")]
    public class IABrain1 : BrainWithDelay
    {
        public override IEnumerator ThinkWithDelay(ThinkerWithDelay thinker)
        {
            if (thinker is not IAThinker iaThinker)yield break;
            var index = iaThinker.getRand(0, 5);
            Debug.Log(index);
            switch (index)
            {
                case 0:
                    iaThinker.AgeUpgrade();
                    break;
                case 1:
                    iaThinker.SpecialCapacity(iaThinker.getRand(0, 1), false);
                    break;
                case 2:
                    iaThinker.Spawn(iaThinker.getRand(0, 3));
                    break;
                case 3:
                {
                    iaThinker.UnlockNewUnit();
                }
                    break;
                case 4:
                    iaThinker.Turret();
                    break;
                case 5:
                    // IAThinker.Upgrade(UpgradeType);
                    break;
            }
            yield return new WaitForSeconds(delayTime);
        }
    }
}