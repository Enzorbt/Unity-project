using System;
using UnityEngine;

/// <summary>
/// The Spawner class handles the spawn of the units in the scene
/// </summary>
public class Spawner : MonoBehaviour
{
    public GameObject unit;

    private Vector3 _spawnPosition;

    private int _spawnNumber;

    private SpriteRenderer _unitSpriteRenderer;

    private void Start()
    {
        //
        _unitSpriteRenderer = unit.GetComponent<SpriteRenderer>();
        Bounds spriteBounds = _unitSpriteRenderer.bounds;
        Vector3 childObjectPosition = transform.Find("SpawnPoint").transform.position;
        
        float posX = childObjectPosition.x;
        float posY = childObjectPosition.y + spriteBounds.extents.y;
        
        _spawnPosition = new Vector3(posX, posY, 0);
    } 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Touche E appuyée");

            if (CanSpawn())
            {
                // faire spawn une unité
                Instantiate(unit, _spawnPosition, new Quaternion());
                _spawnNumber++; 
            }
        }
    }

    private bool CanSpawn()
    {
        return _spawnNumber < 10;
    }
}
