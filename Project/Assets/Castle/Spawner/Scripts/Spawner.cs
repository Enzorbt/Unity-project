using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
using Supinfo.Project.Unit.Scripts;
using Supinfo.Project.Unit.Scripts.Health;
using UnityEngine;
using UnityEngine.UI;

namespace Supinfo.Project.Castle.Spawner.Scripts
{
    public class Spawner : MonoBehaviour
    {
        /// <summary>
        /// The number of units that are spawned and still alive.
        /// </summary>
        private int _spawnNumber;
        /// <summary>
        /// The maximum number of units that can be spawned.
        /// </summary>
        [SerializeField] private int spawnLimit = 10;

        /// <summary>
        /// The event to raise when the number of units in the queue changes.
        /// </summary>
        [SerializeField] private GameEvent onSpawnQueueStatusChange;
        
        /// <summary>
        /// The status of the spawner (true if the spawner can spawn, false otherwise).
        /// </summary>
        private bool _isSpawning;
        
        /// <summary>
        /// The unit container that holds the units spawned by the spawner.
        /// </summary>
        private Transform _unitsContainer;
        
        /// <summary>
        /// The tag that the spawned units will receive.
        /// </summary>
        private string _unitTag;

        /// <summary>
        /// The Queue that holds the units to spawn (as their scriptable object).
        /// </summary>
        private Queue<UnitStatSo> _unitStatSos;
        
        /// <summary>
        /// The spawn position for the units.
        /// </summary>
        private Vector3 _spawnPoint;

        /// <summary>
        /// The image objects that constitute the queue (UI).
        /// </summary>
        private List<Image> _images = new List<Image>();
        
        /// <summary>
        /// The unique identifier for the base (Allies/Enemies).
        /// </summary>
        [SerializeField] private BaseIdentifier baseId;
        
        private void Start()
        {
            _unitStatSos = new Queue<UnitStatSo>();
            _unitTag = "Unit," + gameObject.tag.Split(",")[1];
            _unitsContainer = transform.parent.transform.Find("Units").transform;
            _spawnPoint = transform.Find("SpawnPoint").transform.position;

            // queue management (for allies only)
            if (baseId == BaseIdentifier.BaseEnemies) return;
            for (int i = 0; i < 4; i++)
            {
                _images.Add(GameObject.FindGameObjectWithTag("QueueImage" + i).GetComponent<Image>());
                _images[i].enabled = false;
            }
        }
        
        private void Update()
        {
            if (!_isSpawning && _spawnNumber <= spawnLimit && _unitStatSos.Count != 0)
            {
                StartCoroutine(SpawnWithCoolDown());
            }

            _spawnNumber = _unitsContainer.childCount;
        }

        /// <summary>
        /// Function to calculate the spawn point of the unit (that depends on the size of the prefab, and the size of its sprite).
        /// </summary>
        /// <param name="unit">The unit to spawn.</param>
        /// <returns>The spawn point of the unit, raised to match its size.</returns>
        private Vector3 GetSpawnPoint(GameObject unit)
        {
            var unitSpriteRenderer = unit.GetComponent<SpriteRenderer>();
            var spriteBounds = unitSpriteRenderer.bounds;
            
            var posX = _spawnPoint.x;
            var posY = _spawnPoint.y + spriteBounds.extents.y;

            return new Vector3(posX, posY, 0);
        }

        /// <summary>
        /// The main function of the spawner class, make the unit spawn (with a cooldown).
        /// </summary>
        /// <returns>Wait for seconds.</returns>
        private IEnumerator SpawnWithCoolDown()
        {
            _isSpawning = true;
            
            var coolDown = _unitStatSos.Peek().BuildTime;
            yield return new WaitForSeconds(coolDown);
            
            var unitStatSo = _unitStatSos.Dequeue();
            if (baseId == BaseIdentifier.BaseAllies)
            {
                UpdateQueueUI();
            }
            
            var unitPrefab = unitStatSo.GetPrefab();
            
            GameObject unitSpawned = Instantiate(unitPrefab, GetSpawnPoint(unitPrefab), Quaternion.identity, _unitsContainer);
            unitSpawned.tag = _unitTag;
            
            // give unit its stats
            unitSpawned.TryGetComponent(out UnitThinker unitThinker);
            
            if (unitThinker is null) yield break;
            
            unitThinker.Damage = unitStatSo.Damage;
            unitThinker.WalkSpeed = unitStatSo.WalkSpeed;
            unitThinker.Range = unitStatSo.Range;
            unitThinker.HitSpeed = unitStatSo.HitSpeed;
            unitThinker.UnitType = unitStatSo.Type;

            unitSpawned.TryGetComponent(out SpriteRenderer spriteRenderer);
            
            if (spriteRenderer is null) yield break;

            spriteRenderer.sprite = unitStatSo.Sprite;
            
            unitSpawned.TryGetComponent(out UnitHealth unitHealth);
            
            if (unitHealth is null) yield break;
            
            unitHealth.MaxHealth = unitStatSo.MaxHealth;
            unitHealth.GoldGiven = unitStatSo.GoldGiven;
            unitHealth.XpGiven = unitStatSo.ExperienceGiven;
            unitHealth.UnitType = unitStatSo.Type;
            
            unitSpawned.TryGetComponent(out Animator animator);

            if (animator is null) yield break;

            animator.runtimeAnimatorController = unitStatSo.Controllers;

            onSpawnQueueStatusChange.Raise(this, _unitStatSos.Count < 4);

            _isSpawning = false;
        }

        /// <summary>
        /// Update the unit's queue (UI) with the units that are currently in the queue (_unitsStatSos variable).
        /// </summary>
        private void UpdateQueueUI()
        {
            // disable all sprites
            foreach (var image in _images)
            {
                image.enabled = false;
            }
            // enable the one that need to show up
            for (int i = 0; i < _unitStatSos.Count; i++)
            {
                _images[i].enabled = true;
                _images[i].sprite = _unitStatSos.ElementAt(i).Sprite;
            }
        }

        /// <summary>
        /// Game event listener function called when the event onUnitSpawn is triggered (linked to a GameEventListener component).
        /// </summary>
        /// <param name="sender">The sender of the game event.</param>
        /// <param name="data">The data being transferred.</param>
        public void SpawnUnit(Component sender, object data)
        {
            if (data is not UnitStatSo unitStatSo) return;
            
            _unitStatSos.Enqueue(unitStatSo);
            if (baseId == BaseIdentifier.BaseAllies)
            {
                UpdateQueueUI();
            }
            // send event to notify buttons to be disable 
            onSpawnQueueStatusChange.Raise(this, _unitStatSos.Count < 4);
        }
    }
}
