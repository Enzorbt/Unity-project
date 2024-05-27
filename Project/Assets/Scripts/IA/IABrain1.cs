using Common;
using Supinfo.Project.Scripts;

namespace IA.Event
{
    public class IABrain1 : Brain
    {
        public override void Think(Thinker thinker)
        {
            if (thinker is not IAThinker iaThinker)return;
            switch (iaThinker.getRand(0,5))
            {
                case 0:
                    iaThinker.AgeUpgrade();
                    break;
                case 1:
                    iaThinker.SpecialCapacity(iaThinker.getRand(0, 1));
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
                    // Régler le ploblème capacité
                    // IAThinker.Upgrade(UpgradeType);
                    break;
            }
        }
    }
}