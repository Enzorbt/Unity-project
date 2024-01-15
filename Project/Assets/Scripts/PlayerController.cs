using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Supinfo.Project.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private Camera _camera;

        private Vector3 _cameraOriginalPos;
        private Vector3 _origin;
        private Vector3 _difference;

        private bool _drag = false;
        
        // Start is called before the first frame update
        void Awake()
        {
            _camera = Camera.main;
            _cameraOriginalPos= _camera.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0)) // Clicking in left mouse button
            {
                _difference = _camera.ScreenToWorldPoint(Input.mousePosition) - _camera.transform.position;

                if (_drag == false)
                {
                    _drag = true;
                    _origin = _camera.ScreenToWorldPoint(Input.mousePosition);
                }
                
            }
            else // If not clicking in left mouse button drag = false
            {
                _drag = false;
            }

            if (_drag)
            {
                MoveCameraHorizontally(_difference.x);
            }
        }

        private void MoveCameraHorizontally(float offset)
        {
            // move camera horizontally
            _camera.transform.position = new Vector3(_origin.x - offset,_cameraOriginalPos.y, _origin.z);
        }
    }
}
