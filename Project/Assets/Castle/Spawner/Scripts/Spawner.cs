using System;
using System.Collections;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.ScriptableObjects.Unit;
using UnityEngine;

namespace Supinfo.Project.Castle.Spawner.Scripts
{
    /// <summary>
    /// The Spawner class handles the spawning of units in the scene. 
    /// It manages the spawn position, spawn limits, cooldowns, and instantiation of units.
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        // Public fields

        /// <summary>
        /// Prefab of the unit to be spawned.
        /// </summary>
        public GameObject unit;

        /// <summary>
        /// Cooldown time (in seconds) between spawns.
        /// </summary>
        public int cooldownTime = 2;

        /// <summary>
        /// Direction for the spawned unit to face.
        /// </summary>
        public Vector3 direction;

        // Private fields

        /// <summary>
        /// The position where units will be spawned.
        /// </summary>
        private Vector3 _spawnPosition;
        
        /// <summary>
        /// Number of units currently spawned.
        /// </summary>
        private int _spawnNumber;

        /// <summary>
        /// Maximum number of units that can be spawned.
        /// </summary>
        private int _spawnLimit = 10;

        /// <summary>
        /// SpriteRenderer of the unit to be spawned. Used for calculating spawn position.
        /// </summary>
        private SpriteRenderer _unitSpriteRenderer;

        /// <summary>
        /// Flag to check if the spawner is in cooldown period.
        /// </summary>
        private bool _isCooldown = false;

        /// <summary>
        /// Transform to hold the spawned units.
        /// </summary>
        private Transform _unitsContainer;

        // MonoBehaviour methods

        /// <summary>
        /// Start is called before the first frame update.
        /// It initializes the spawn position and the container for the spawned units.
        /// </summary>
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

        /// <summary>
        /// Update is called once per frame.
        /// It checks for spawn inputs and initiates the spawning process.
        /// </summary>
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

        /// <summary>
        /// Checks if a new unit can be spawned based on the spawn limit and cooldown.
        /// </summary>
        /// <returns>True if a unit can be spawned, false otherwise.</returns>
        private bool CanSpawn()
        {
            return _spawnNumber < _spawnLimit && !_isCooldown;
        }

        // Coroutines

        /// <summary>
        /// Coroutine to handle the cooldown between spawns.
        /// Spawns a unit after the specified cooldown time.
        /// </summary>
        /// <param name="time">The cooldown time in seconds.</param>
        /// <returns>IEnumerator for coroutine.</returns>
        IEnumerator SpawnWithCoolDown(int time)
        {
            _isCooldown = true;

            // Wait for "time" seconds
            yield return new WaitForSeconds(time);
                
            // Spawn a unit
            GameObject unitSpawned = Instantiate(unit, _spawnPosition, new Quaternion(), _unitsContainer);
            _spawnNumber++;

            _isCooldown = false;
        }

        /// <summary>
        /// Method to spawn a unit. Can be triggered externally.
        /// </summary>
        /// <param name="sender">The component that triggered the spawn.</param>
        /// <param name="data">Data or parameters for spawning, typically the unit prefab.</param>
        public void SpawnUnit(Component sender, object data)
        {
            // check if the sent data is a UnitSpawnSo
            UnitSpawnSo unitSpawnSo = data as UnitSpawnSo;

            if (unitSpawnSo != null)
            {
                var coolDown = unitSpawnSo.BuildTime.GetValue();
                var unitPrefab = unitSpawnSo.GetPrefabWithAge(); // add age here to get different prefab

                // for now the unit is spawn instantly
                GameObject unitSpawned = Instantiate(unitPrefab, _spawnPosition, new Quaternion(), _unitsContainer);
                
                // add the unit to the unit to spawn queue
            }
            
            
        }
    }
}
