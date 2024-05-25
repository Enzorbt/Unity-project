using Common;
using Supinfo.Project.Scripts;

namespace IA.Event
{
    public class IABrain1 : Brain
    {
        public override void Think(Thinker thinker)
        {
            if (thinker is not IAThinker iaThinker)return;
            switch (IAThinker.getRand(0,5))
            {
                case 0:
                    IAThinker.AgeUpgrade();
                    break;
                case 1:
                    IAThinker.SpecialCapacity(IAThinker.getRand(0, 1));
                    break;
                case 2:
                    IAThinker.Spawn(IAThinker.getRand(0, 3));
                    break;
                case 3:
                {
                    IAThinker.UnlockNewUnit();
                }
                    break;
                case 4:
                    IAThinker.Turret();
                    break;
                case 5:
                    // Régler le ploblème capacité
                    // IAThinker.Upgrade(UpgradeType);
                    break;
            }
        }
    }
}