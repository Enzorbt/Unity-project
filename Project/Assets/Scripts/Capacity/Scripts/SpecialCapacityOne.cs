using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Capacity.Scripts
{
    public class SpecialCapacityOne : MonoBehaviour
    {
        public float p_damage = 70f;
        public void CapacityMakeDammage(Component sender, object data)
        {
            GameObject[] allUnits = GameObject.FindGameObjectsWithTag("Unit,Enemies");

            foreach (GameObject unit in allUnits)
            {
                if (unit.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(p_damage);
                    Debug.Log("GameObject: " + unit.name + " has taken damage.");
                }
            }
        }
    }
}


// Ajouter la detection d'ID pour envoyer au joeur adverse 