// Amelioration : BUY GOLD AMELIORATION, TURRET, ARMOR AND RANGE AMELLIORATION


using System.Collections;
using Supinfo.Project.Common;
using UnityEngine;

namespace IA.Event
{
    [CreateAssetMenu(menuName = "Brains/IABrain3")]
    public class IABrain3 : BrainWithDelay
    {
        public override IEnumerator ThinkWithDelay(ThinkerWithDelay thinker)
        {
            if (thinker is not IAThinker iaThinker)yield break;
            
            iaThinker.IsThinking = true;
            
            if (!iaThinker.IsUnlock)
            {
                iaThinker.UnlockNewUnit();
                Debug.Log("UNLOCK");
            }
            
            // SPAWN UNIT 
            if (iaThinker.DetectUnitsAndAllies() != 0)
            {                
                iaThinker.Spawn(UnitChoice.antiarmor);
                Debug.Log("SPAWN");

            }
            
            if (iaThinker.DetectUnitsAndAllies() == 3)
            {                
                iaThinker.Spawn(UnitChoice.melee);
                iaThinker.Spawn(UnitChoice.melee);
                iaThinker.Spawn(UnitChoice.range);
                Debug.Log("SPAWN");
            }
            
            // LANCE CAPACITE SPECIAL
            if (iaThinker.DetectUnitsAndAllies() >= 6)
            {
                iaThinker.SpecialCapacity(CapacityChoice.lightning, true);
                Debug.Log("CAPACITY 2");
            }
            if (iaThinker.DetectUnitsAndAllies() == 10)
            {
                iaThinker.SpecialCapacity(CapacityChoice.fire, true);
                Debug.Log("CAPACITY 1");
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
                if (actionChoice == ActionChoice.spawn)
                {
                    if (!iaThinker.Spawn((UnitChoice)iaThinker.getRand(0, 3)))
                    {
                        setAction(ActionChoice.spawn);
                    }
                    else
                    {
                        setAction(ActionChoice.age);
                        Debug.Log("SPAWN");
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
                        setAction(ActionChoice.spawn);
                        Debug.Log("AGE");
                    }
                }
            }
            yield return new WaitForSeconds(delayTime);
            iaThinker.Gold += 5; 
            iaThinker.IsThinking = false;
        }
    }
}