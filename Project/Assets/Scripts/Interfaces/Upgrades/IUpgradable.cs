namespace Supinfo.Project.Scripts.Interfaces.Upgrades
{
    public interface IUpgradable
    {
        public void Upgrade(UpgradeType type); // upgrade a stat of the upgrade type (from enum)
    }
}