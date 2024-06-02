using UnityEngine;
using System.Collections.Generic;
using ScriptableObjects.Turret;
using Supinfo.Project.Turret.Scripts;

/// <summary>
/// The SpawnerTurret class handles the spawning of turrets.
/// </summary>
public class SpawnerTurret : MonoBehaviour
{
    /// <summary>
    /// The position where turrets will be spawned.
    /// </summary>
    public GameObject spawnPosition;

    /// <summary>
    /// The current number of spawned turrets.
    /// </summary>
    private int _spawnNumber;

    /// <summary>
    /// The maximum number of turrets that can be spawned.
    /// </summary>
    private int _spawnLimit = 4;

    /// <summary>
    /// List to keep track of spawned turrets.
    /// </summary>
    private List<GameObject> _spawnedTurrets = new List<GameObject>();
    
    /// <summary>
    /// Checks if turrets can be spawned.
    /// </summary>
    /// <returns>True if turrets can be spawned; otherwise, false.</returns>
    private bool CanSpawn()
    {
        return _spawnNumber < _spawnLimit;
    }

    /// <summary>
    /// Spawns a turret.
    /// </summary>
    /// <param name="sender">The sender of the spawn request.</param>
    /// <param name="data">The data containing turret information.</param>
    public void SpawnTurret(Component sender, object data)
    {
        if (data is not TurretStatSo turretStatSo) return;
        if (!CanSpawn()) return;
        
        if (turretStatSo.Prefab != null)
        {
            // Calculate the Y position for the new turret based on spawn number
            var yPosition = spawnPosition.transform.position.y + _spawnNumber * 1.5f; 
            var newSpawnPosition = new Vector3(spawnPosition.transform.position.x, yPosition, _spawnNumber);
            var spawnedTurret = Instantiate(turretStatSo.Prefab, newSpawnPosition, Quaternion.identity);
            var tags = transform.tag.Split(',');

            // Set the TurretStatSo for the spawned turret
            spawnedTurret.TryGetComponent(out TurretThinker turretThinker);
            turretThinker.TurretStatSo = turretStatSo;
            
            // Update turret sprite
            var spriteRenderer = spawnedTurret.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = turretStatSo.Sprite;
            spriteRenderer.sortingOrder = 7 - _spawnNumber;
            
            spawnedTurret.tag = "Turret," + tags[1];
            _spawnedTurrets.Add(spawnedTurret);
            _spawnNumber++;
        }
        else
        {
            Debug.LogWarning("Please assign a prefab to the variable 'turretPrefab' in the inspector.");
        }
    }
}
