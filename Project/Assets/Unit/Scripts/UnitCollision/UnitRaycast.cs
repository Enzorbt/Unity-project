using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts.UnitCollision
{
    public class UnitRaycast : MonoBehaviour
    {
        private float _rayMoovementLength = 2; // Need to be find in the unit data 
        // private float _rayAttackLength = 1; // Need to be find in the unit data
        private IMovement _unitMovementScript;
        private RaycastHit2D _hit;
        private Vector3 _direction;

        private void Start()
        {
            _unitMovementScript = GetComponent<IMovement>();
        }
    
        private void Awake()
        {
            if (transform.position.x > 0)
            {
                _direction = Vector3.left;
            }
            else
            {
                _direction = Vector3.right;
            }
        }

        private void FixedUpdate()
        {
            PerformRaycastMoovement(_rayMoovementLength);
            //PerformRaycastAttack(_rayAttackLength);
        }

        private void PerformRaycastMoovement(float length)
        {
            _hit = Physics2D.Raycast(transform.position, _direction, length, ~LayerMask.GetMask("Unit"));

            if (_hit.collider is not null)
            {
                string[] hitTagParts = _hit.collider.tag.Split(',');
                string[] myTagParts = gameObject.tag.Split(',');
                if (hitTagParts[0] == "Unit")
                {
                    if (hitTagParts[1] != myTagParts[1])
                    {
                        // Rencontre avec une unité enemy 
                        //_unitMovementScript.StopMovement();
                        Debug.Log("Enemy find !");
                    }
                    _unitMovementScript.StopMovement();
                }
            }
            else
            {
                _unitMovementScript.StartMovement();
            }
        }

        private void PerformRaycastAttack(float length)
        {
            // Vérfier si une unité et dans le range de l'unité pour l'attaquer (Avec le range d'attack récupèrer dans la data des unit)
        }

        private void OnDrawGizmos()
        {
            if (_unitMovementScript != null)
            {
                Debug.DrawRay(transform.position, _direction * _rayMoovementLength, Color.green);
            }
        }
    }
}
