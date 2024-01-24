// using UnityEngine;
//
// namespace Supinfo.Project.Unit.Scripts.UnitCollision
// {
//     public class UnitRaycast : MonoBehaviour
//     {
//         private float _rayLength = 2;
//         private Unit _unitScript;
//         private RaycastHit2D _hit;
//         
//         private void Start()
//         {
//             _unitScript = GetComponent<Unit>();
//         }
//
//         private void FixedUpdate()
//         {
//             PerformRaycast(_rayLength);
//         }
//         
//         private void PerformRaycast(float length)
//         {
//             _hit = Physics2D.Raycast(transform.position, _unitScript.GetDirection(), length);
//             
//             if (_hit.collider != null && _hit.collider != GetComponent<Collider2D>())
//             {
//                 Debug.Log("Hit object: " + _hit.transform.name);
//                 _unitScript.SetIsMoving(false); 
//                 // if (_hit.collider.gameObject.CompareTag("Unit"))
//                 // {
//                 //     Debug.Log("Unit met");
//                 //     _unitScript.SetIsMoving(false);
//                 // }
//                 // else if (_hit.collider.gameObject.CompareTag("Castle"))
//                 // {
//                 //     Debug.Log("Castle met");
//                 //     _unitScript.SetIsMoving(false); 
//                 // }
//             }
//         }
//         
//         private void OnDrawGizmos()
//         {
//             Debug.DrawRay(transform.position, _unitScript.GetDirection() * _rayLength, Color.green);
//         }
//     }
// }
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts.UnitCollision
{
    public class UnitRaycast : MonoBehaviour
    {
        private float _rayLength = 2;
        private Unit _unitScript;
        private RaycastHit2D _hit;

        private void Start()
        {
            _unitScript = GetComponent<Unit>();
        }

        private void FixedUpdate()
        {
            PerformRaycast(_rayLength);
        }

        private void PerformRaycast(float length)
        {
            _hit = Physics2D.Raycast(transform.position, _unitScript.GetDirection(), length, ~LayerMask.GetMask("Unit"));

            if (_hit.collider != null)
            {
                if (_hit.collider.gameObject != gameObject && _hit.collider.CompareTag("Unit"))
                {
                    Debug.Log("Unit met: " + _hit.transform.name);
                    _unitScript.SetIsMoving(false);
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (_unitScript != null)
            {
                Debug.DrawRay(transform.position, _unitScript.GetDirection() * _rayLength, Color.green);
            }
        }
    }
}