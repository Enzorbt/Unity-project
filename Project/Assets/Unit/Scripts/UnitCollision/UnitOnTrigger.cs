using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts.UnitCollision
{
    public class UnitOnTrigger : MonoBehaviour
    { 
        private IMovement _unitMovementScript; 
        
        private void Start()
        {
            _unitMovementScript = GetComponent<IMovement>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Unit"))
            {
                Debug.Log("Unit find");
                _unitMovementScript.Stop();
            }

            if (collision.CompareTag("Castle"))
            {
                Debug.Log("Castle find");
                //unitScript.SetIsMoving(false);
            }
        }
    }
}
