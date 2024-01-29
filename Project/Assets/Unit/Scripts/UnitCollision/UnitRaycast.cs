using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts.UnitCollision
{
    public class UnitRaycast : MonoBehaviour
    {
        private float _rayLength = 2;
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
            PerformRaycast(_rayLength);
        }

        private void PerformRaycast(float length)
        {
            _hit = Physics2D.Raycast(transform.position, _direction, length, ~LayerMask.GetMask("Unit"));

            if (_hit.collider is not null)
            {
                if (_hit.collider.gameObject != gameObject && _hit.collider.CompareTag("Unit"))
                {
                    Debug.Log("Unit met: " + _hit.transform.name);
                    _unitMovementScript.Stop();
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (_unitMovementScript != null)
            {
                Debug.DrawRay(transform.position, _direction * _rayLength, Color.green);
            }
        }
    }
}