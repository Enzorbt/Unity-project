using System;
using System.Collections;
using UnityEngine;

namespace Supinfo.Project
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
            Vector3 childObjectPosition = transform.Find("SpawnPoint").transform.position;
            _unitsContainer = transform.Find("Units").transform;

            float posX = childObjectPosition.x;
            float posY = childObjectPosition.y + spriteBounds.extents.y;

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
            
            // changing the localScale of the instantiated object because scale is proportional to the scale of the parent (here castle who is bigger) so object scale becomes object scale / parent scale.
            _transformLocalScale = transform.localScale;
            Vector3 unitSpawnedLocalScale = unitSpawned.transform.localScale;
            unitSpawned.transform.localScale = new Vector3(unitSpawnedLocalScale.x/_transformLocalScale.x, unitSpawnedLocalScale.y/_transformLocalScale.y, unitSpawnedLocalScale.z/_transformLocalScale.z);
            
            _spawnNumber++;

            _isCooldown = false;
        }
    }
}