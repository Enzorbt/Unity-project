using UnityEngine;
using System.Collections.Generic;
using ScriptableObjects.Turret;

public class SpawnerTurret : MonoBehaviour
{
    public GameObject spawnPosition;
    private int _spawnNumber;
    private int _spawnLimit = 4;
    private List<GameObject> _spawnedTurrets = new List<GameObject>();
    
    private bool CanSpawn()
    {
        return _spawnNumber < _spawnLimit;
    }

    public void SpawnTurret(Component sender, object data)
    {
        if (data is not TurretStatSo turretStatSo) return;
        if (!CanSpawn()) return;
        
        if (turretStatSo.Prefab != null)
        {
            var yPosition = spawnPosition.transform.position.y + _spawnNumber * 0.9f; 
            var newSpawnPosition = new Vector3(spawnPosition.transform.position.x, yPosition, _spawnNumber);
            var spawnedTurret = Instantiate(turretStatSo.Prefab, newSpawnPosition, Quaternion.identity);
            var tags = transform.tag.Split(',');
            
            // update turret sprite
            var spriteRenderer = spawnedTurret.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = turretStatSo.Sprite;
            
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