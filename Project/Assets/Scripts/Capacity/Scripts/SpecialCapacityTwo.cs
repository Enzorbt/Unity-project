using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Capacity.Scripts
{
    public class SpecialCapacityTwo : MonoBehaviour
    {
        public float p_damage = 100f;
        public float damageProbability = 0.5f;

        public void CapacityMakeDammageRand(Component sender, object data)
        {
            GameObject[] allUnits = GameObject.FindGameObjectsWithTag("Unit");

            foreach (GameObject unit in allUnits)
            {
                if (unit.TryGetComponent(out IDamageable damageable))
                {
                    if (Random.value < damageProbability)
                    {
                        damageable.TakeDamage(p_damage);
                        Debug.Log("GameObject: " + unit.name + " has taken damage.");
                    }
                }
            }
        }
    }
}

// Ajouter la detection d'ID pour envoyer au joeur adverse 