using UnityEngine;

public class SpawnerTurret : MonoBehaviour
{
    public GameObject turretPrefab;
    public Vector3 spawnPosition = Vector3.zero;
    private int _spawnNumber;
    private int _spawnLimit = 1;

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
            GameObject spawnedTurret = Instantiate(turretPrefab, spawnPosition, Quaternion.identity);
            _spawnNumber++;
            spawnedTurret.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Veuillez assigner un prefab à la variable 'turretPrefab' dans l'inspecteur.");
        }
    }
}