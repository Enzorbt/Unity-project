using UnityEngine;

namespace Supinfo.Project.Unit.Scripts.UnitCollision
{
    public class UnitRaycast : MonoBehaviour
    {
        private float rayLength = 100f;
        private Unit unitScript;
        private RaycastHit hit;
        //private Rigidbody2D _rigidbody;
        
        private void Start()
        {
            unitScript = GetComponent<Unit>();
            //_rigidbody = GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            // Clean Unity / componant 
            // Comment 
            Debug.DrawRay(transform.position, unitScript.GetDirection() * rayLength, Color.green);
            if (Physics.Raycast(transform.position, unitScript.GetDirection(), rayLength))
            {
                // Faire quelque chose avec l'objet frappé
                Debug.Log("Objet frappé : " + hit.collider.gameObject.name);
            }
            
        }
    }
}