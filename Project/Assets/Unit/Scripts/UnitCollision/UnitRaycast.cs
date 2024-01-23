using UnityEngine;

namespace Supinfo.Project.Unit.Scripts.UnitCollision
{
    public class UnitRaycast : MonoBehaviour
    {
        private Unit unitScript;
        private int rayLenght = 20;
        private void Start()
        {
            unitScript = GetComponent<Unit>();
        }
        void Update()
        {
            // Get the direction of unit 
            // Make the if else to catch the unit with a raycast 
            // Clean Unity / componant 
            // Comment 
            Debug.DrawRay(transform.position, Vector3.left * rayLenght);
            
        }
    }
}