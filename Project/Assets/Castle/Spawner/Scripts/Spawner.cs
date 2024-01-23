using System;
using System.Collections;
using Supinfo.Project.Scripts.Events;
using UnityEngine;

namespace Supinfo.Project.Castle.Spawner.Scripts
{
    /// <summary>
    /// The Spawner class handles the spawn of the units in the scene
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        // Public fields
        public GameObject unit;

        public int cooldownTime = 2;

        public Vector3 direction;

        // Private fields

        private Vector3 _spawnPosition;
        
        private int _spawnNumber;
        private int _spawnLimit = 10;

        private SpriteRenderer _unitSpriteRenderer;

        private bool _isCooldown = false;

        private Transform _unitsContainer;
        private Vector3 _transformLocalScale;

        // Public methods


        // MonoBehaviour methods
        private void Start()
        {
            _unitSpriteRenderer = unit.GetComponent<SpriteRenderer>();
            Bounds spriteBounds = _unitSpriteRenderer.bounds;
            Vector3 spawnPoint = transform.Find("SpawnPoint").transform.position;
            _unitsContainer = transform.parent.transform.Find("Units").transform;

            float posX = spawnPoint.x;
            float posY = spawnPoint.y + spriteBounds.extents.y;

            _spawnPosition = new Vector3(posX, posY, 0);
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (CanSpawn())
                {
                    StartCoroutine(SpawnWithCoolDown(cooldownTime));
                }
            }
        }

        // Private methods
        private bool CanSpawn()
        {
            return _spawnNumber < _spawnLimit && !_isCooldown;
        }


        // Coroutines
        IEnumerator SpawnWithCoolDown(int time)
        {
            _isCooldown = true;

            // Wait for "time" seconds
            yield return new WaitForSeconds(time);
                
            // spawn a unit
                // reference the instantiated object to keep track of it 
            GameObject unitSpawned = Instantiate(unit, _spawnPosition, new Quaternion(), _unitsContainer);
            
            _spawnNumber++;

            _isCooldown = false;
        }

        public void SpawnUnit(Component sender,object data)
        {
            GameObject unitSpawned = Instantiate((GameObject)data, _spawnPosition, new Quaternion(), _unitsContainer);
    
        }
    }
}