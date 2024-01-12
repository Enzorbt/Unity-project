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
        // Public
        public GameObject unit;

        public int cooldownTime = 2;

        public Vector3 direction;

        // Private

        private Vector3 _spawnPosition;

        private int _spawnNumber;

        private SpriteRenderer _unitSpriteRenderer;

        private bool _isCooldown = false;

        // Public methods


        // MonoBehaviour methods
        private void Start()
        {
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
                if (CanSpawn())
                {
                    StartCoroutine(SpawnWithCoolDown(cooldownTime));
                }
            }
        }

        // Private methods
        private bool CanSpawn()
        {
            return _spawnNumber < 10 && !_isCooldown;
        }


        // Coroutines
        IEnumerator SpawnWithCoolDown(int time)
        {
            _isCooldown = true;

            // Wait for "time" seconds
            yield return new WaitForSeconds(time);

            unit.GetComponent<Unit>().SetDirection(direction);
            // faire spawn une unité
            Instantiate(unit, _spawnPosition, new Quaternion());
            _spawnNumber++;

            _isCooldown = false;
        }
    }
}