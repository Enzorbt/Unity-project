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
            
            // AGE UPGRADE
            iaThinker.AgeUpgrade();

            // UNLOCK UNIT
            if (!iaThinker.IsUnlock)
            {
                iaThinker.UnlockNewUnit();
            }
            
            // SPAWN UNIT 
            if (iaThinker.DetectUnitsAndAllies() != 0) // SPAWN PAR DEFAULT
            {                
                iaThinker.Spawn(UnitChoice.antiarmor, true);
            }
            
            if (iaThinker.DetectUnitsAndAllies() == 3) // REPONCE AU JOUEUR
            {                
                iaThinker.Spawn(UnitChoice.melee, false);
                iaThinker.Spawn(UnitChoice.melee, false);
                iaThinker.Spawn(UnitChoice.range, false);
            }
            
            
            // LAUCH CAPACITY
            if (iaThinker.DetectUnitsAndAllies() >= 6) // LANCE ECLAIRE SI +6 UNITE ADVAIRSE
            {
                iaThinker.SpecialCapacity(CapacityChoice.lightning, true);
            }
            
            if (iaThinker.DetectUnitsAndAllies() == 10) // LANCE BOULE DE FEUX SI 10 UNITE ADVAIRSE
            {
                iaThinker.SpecialCapacity(CapacityChoice.fire, true);
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
                if (actionChoice == ActionChoice.spawn) // SPAWN RANDOM
                {
                    if (!iaThinker.Spawn((UnitChoice)iaThinker.getRand(0, 3), true))
                    {
                        setAction(ActionChoice.spawn);
                    }
                    else
                    {
                        setAction(ActionChoice.age);
                    }
                }
                else if (actionChoice == ActionChoice.upgrade) // UPGRADE
                {
                    if (!iaThinker.AgeUpgrade())
                    {
                        setAction(ActionChoice.upgrade);
                    }
                    else
                    {
                        setAction(ActionChoice.spawn);
                    }
                }
            }
            yield return new WaitForSeconds(delayTime);
            
            iaThinker.Gold += 5; 
            
            iaThinker.IsThinking = false;
        }
    }
}