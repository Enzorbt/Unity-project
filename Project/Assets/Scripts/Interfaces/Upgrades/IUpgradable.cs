namespace Supinfo.Project.Scripts.Interfaces.Upgrades
{
    public enum UpgradeType
    {
        Attack,
        Range,
        GoldGiven,
        Health,
        Price
    }
    
    public interface IUpgradable
    {
        public void Upgrade(UpgradeType type); // upgrade a stat of the upgrade type (from enum)
    }
}