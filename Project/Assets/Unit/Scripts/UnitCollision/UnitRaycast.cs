using UnityEngine;

namespace Supinfo.Project.Unit.Scripts.UnitCollision
{
    public class UnitRaycast : MonoBehaviour
    {
        //Private fields
        private float rayLength = 10f;
        private Unit unitScript;
        private RaycastHit hit;
        private Ray _ray ;
        
        private void Start()
        {
            unitScript = GetComponent<Unit>();
        }
        private void Update()
        {
            if (Physics.Raycast(transform.position, unitScript.GetDirection(), out hit, rayLength))
            {
                Debug.Log("Raycast en avant : " + hit.collider.gameObject.name);
            }
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(transform.position, unitScript.GetDirection() * rayLength, Color.green);
        }
    }
}