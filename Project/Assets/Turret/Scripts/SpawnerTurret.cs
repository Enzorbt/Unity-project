using UnityEngine;
using System.Collections.Generic;

public class SpawnerTurret : MonoBehaviour
{
    public GameObject turretPrefab;
    public GameObject spawnPosition;
    private int _spawnNumber;
    private int _spawnLimit = 4;
    private List<GameObject> _spawnedTurrets = new List<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (CanSpawn())
            { 
                SpawnTurret();
            }
        }
    }
    
    private bool CanSpawn()
    {
        return _spawnNumber < _spawnLimit;
    }

    void SpawnTurret()
    {
        if (turretPrefab != null)
        {
            float yPosition = spawnPosition.transform.position.y + _spawnNumber * 1f; 
            Vector3 newSpawnPosition = new Vector3(spawnPosition.transform.position.x, yPosition, spawnPosition.transform.position.z);
            GameObject spawnedTurret = Instantiate(turretPrefab, newSpawnPosition, Quaternion.identity);
            string[] tags = transform.tag.Split(',');
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