using System.Collections.Generic;
using Common;
using Supinfo.Project.Scripts;


// Quand +7 UNITÉ Adversaise USE CAPACITY IF POSSIBLE 

// ALWAY KEEP MONEY FOR BUYING MELEE TO DEFENCE 

namespace IA.Event
{
    public class IABrain4 : Brain

    {
        public override void Think(Thinker thinker)
        {
            if (thinker is not IAThinker4 iaThinker)return;
            foreach (KeyValuePair<UpgradeType, int> entry in iaThinker.UpgradeDict)
            {
                if (entry.Value < 3)
                {
                    IAThinker.Upgrade(entry.Key);
                }
            }
        }
    }
}