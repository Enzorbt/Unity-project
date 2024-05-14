using System.Collections;
using ScriptableObjects.Unit;
using UnityEngine;

namespace Supinfo.Project.Castle.Spawner.Scripts
{
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
        private string _unitTag;

        private void Start()
        {
            _unitSpriteRenderer = unit.GetComponent<SpriteRenderer>();
            Bounds spriteBounds = _unitSpriteRenderer.bounds;
            Vector3 spawnPoint = transform.Find("SpawnPoint").transform.position;
            _unitsContainer = transform.parent.transform.Find("Units").transform;

            float posX = spawnPoint.x;
            float posY = spawnPoint.y + spriteBounds.extents.y;

            _spawnPosition = new Vector3(posX, posY, 0);
            _unitTag = gameObject.tag;
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

        private bool CanSpawn()
        {
            return _spawnNumber < _spawnLimit && !_isCooldown;
        }

        IEnumerator SpawnWithCoolDown(int time)
        {
            _isCooldown = true;
            yield return new WaitForSeconds(time);
            GameObject unitSpawned = Instantiate(unit, _spawnPosition, Quaternion.identity, _unitsContainer);
            unitSpawned.tag = _unitTag;
            _spawnNumber++;
            _isCooldown = false;
        }

        public void SpawnUnit(Component sender, object data)
        {
            UnitStatSo unitStatSo = data as UnitStatSo;

            if (unitStatSo != null)
            {
                var coolDown = unitStatSo.BuildTime;
                var unitPrefab = unitStatSo.GetPrefab();
                GameObject unitSpawned = Instantiate(unitPrefab, _spawnPosition, Quaternion.identity, _unitsContainer);
                unitSpawned.tag = _unitTag;
            }
        }
    }
}
