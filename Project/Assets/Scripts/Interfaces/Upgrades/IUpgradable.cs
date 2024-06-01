namespace Supinfo.Project.Scripts.Interfaces.Upgrades
{
    /// <summary>
    /// Interface for objects that can be upgraded.
    /// </summary>
    public interface IUpgradable
    {
        /// <summary>
        /// Upgrades the object in the way specified by the upgrade type.
        /// </summary>
        /// <param name="type">The type of upgrade to apply.</param>
        public void Upgrade(UpgradeType type);
    }
}