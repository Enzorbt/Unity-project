// Amelioration : BUY GOLD AMELIORATION, TURRET, ARMOR AND RANGE AMELLIORATION

using System.Collections;
using Supinfo.Project.Common;
using UnityEngine;

namespace IA.Event
{
    [CreateAssetMenu(menuName = "Brains/IABrain2")]
    public class IABrain2 : BrainWithDelay
    {
        public override IEnumerator ThinkWithDelay(ThinkerWithDelay thinker)
        {
            if (thinker is not IAThinker iaThinker) yield break;
            Debug.Log("Xp : " + iaThinker.Xp);
            Debug.Log("Gold : " +iaThinker.Gold);
            
            iaThinker.IsThinking = true;

            iaThinker.AgeUpgrade();
            
            if (!iaThinker.IsUnlock)
            {
                iaThinker.UnlockNewUnit();
            }
            
            // SPAWN UNIT 
            if (iaThinker.DetectUnitsAndAllies() != 0)
            {
                iaThinker.Spawn(UnitChoice.armor);
                iaThinker.Spawn(UnitChoice.range);
            }
            
            // LANCE CAPACITE SPECIAL
            if (iaThinker.DetectUnitsAndAllies() >= 5)
            {
                iaThinker.SpecialCapacity(CapacityChoice.fire, false);
            }

            // Comporetement Applicatif
            var actionChoice = ActionChoice.age;

            ActionChoice setAction(ActionChoice pactionChoice)
            {
                actionChoice = pactionChoice;
                return actionChoice;
            }

            if (iaThinker.Gold > 300)
            {
                if (actionChoice == ActionChoice.turret)
                {
                    if (!iaThinker.Turret())
                    {
                        setAction(ActionChoice.turret);
                    }
                    else
                    {
                        setAction(ActionChoice.age);
                    }
                }
                else if (actionChoice == ActionChoice.age)
                {
                    if (!iaThinker.AgeUpgrade())
                    {
                        setAction(ActionChoice.age);
                    }
                    else
                    {
                        setAction(ActionChoice.upgrade);
                    }
                }
                else if (actionChoice == ActionChoice.upgrade)
                {
                    if (!iaThinker.AgeUpgrade())
                    {
                        setAction(ActionChoice.upgrade);
                    }
                    else
                    {
                        setAction(ActionChoice.turret);
                    }
                }
            }
            
            yield return new WaitForSeconds(delayTime);
            iaThinker.Gold += 5; 
            iaThinker.IsThinking = false;
        }
    }
}