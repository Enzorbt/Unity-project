namespace Supinfo.Project.Scripts.Interfaces
{
    /// <summary>
    /// Interface for objects that can upgrade the game's age.
    /// </summary>
    public interface IAgeUpgradable
    {
        /// <summary>
        /// Upgrades the game's current age, potentially unlocking new units, turrets, or capacities.
        /// </summary>
        public void UpgradeAge();
    }
}