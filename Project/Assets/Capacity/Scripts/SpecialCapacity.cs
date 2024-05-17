using System.Collections;
using Supinfo.Project.Interfaces.Capacity;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using UnityEngine;

namespace Supinfo.Project.Capacity.Scripts
{
    public class SpecialCapacity : MonoBehaviour, ICapacity
    {
        private bool _running;
        public void LaunchCapacity(Component sender, object data)
        {
            Debug.Log("Launching");
            if (data is not CapacitySo capacitySo) return;

            if (!_running)
            {
                StartCoroutine(CapacityDelayed(capacitySo));
            }
        }

        private IEnumerator CapacityDelayed(CapacitySo capacitySo)
        {
            _running = true;
            var allUnits = GameObject.FindGameObjectsWithTag("Unit," + capacitySo.Tag);

            foreach (var unit in allUnits)
            {
                if (!unit.TryGetComponent(out IDamageable damageable)) break;
                if (Random.value < capacitySo.HitProbability)
                {
                    damageable.TakeDamage(capacitySo.Value);
                }
            }
            
            yield return new WaitForSeconds(capacitySo.Cooldown);
            
            _running = false;
        }
    }
}