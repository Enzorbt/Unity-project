using UnityEngine;

namespace Supinfo.Project.Scripts
{
    /// <summary>
    /// Controls the player camera movements.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// Main camera reference.
        /// </summary>
        private Camera _camera;

        /// <summary>
        /// Original position of the camera.
        /// </summary>
        private Vector3 _cameraOriginalPos; 
        
        /// <summary>
        /// Origin position when dragging starts.
        /// </summary>
        private Vector3 _origin; 
        
        /// <summary>
        /// Difference between the current and origin positions.
        /// </summary>
        private Vector3 _difference;

        /// <summary>
        /// Flag to check if the camera is being dragged.
        /// </summary>
        private bool _drag = false;

        // New variables for the left and right boundaries
        
        /// <summary>
        /// Left boundary for camera movement.
        /// </summary>
        [SerializeField] private float leftBoundary = 0f;
        
        /// <summary>
        /// Right boundary for camera movement.
        /// </summary>
        [SerializeField] private float rightBoundary = 34.55f;

        /// <summary>
        /// State of the Drag possibility (true = can, false = cannot)
        /// </summary>
        private bool _canDrag = true;
        
        void Awake()
        {
            _camera = Camera.main;  // Get the main camera.
            _cameraOriginalPos = _camera.transform.position;  // Store the original camera position.
        }
        
        void Update()
        {
            if (!_canDrag) return;
            if (Input.GetMouseButton(0)) // Clicking in left mouse button
            {
                _difference = _camera.ScreenToWorldPoint(Input.mousePosition) - _camera.transform.position;

                if (_drag == false)
                {
                    _drag = true;  // Set drag flag to true.
                    _origin = _camera.ScreenToWorldPoint(Input.mousePosition);  // Set the origin position.
                }
            }
            else // If not clicking in left mouse button drag = false
            {
                _drag = false;  // Set drag flag to false.
            }

            if (_drag)
            {
                MoveCameraHorizontally(_difference.x);  // Move the camera horizontally.
            }
        }

        /// <summary>
        /// Moves the camera horizontally within the defined boundaries.
        /// </summary>
        /// <param name="offset">Offset value for camera movement.</param>
        private void MoveCameraHorizontally(float offset)
        {
            // Calculate the new x position
            float newX = _origin.x - offset;

            // Clamp the new x position between the left and right boundaries
            newX = Mathf.Clamp(newX, leftBoundary, rightBoundary);

            // Move camera horizontally
            _camera.transform.position = new Vector3(newX, _cameraOriginalPos.y, _origin.z);
        }

        /// <summary>
        /// Game event listener function to change the state of the drag possibility.
        /// </summary>
        /// <param name="sender">The sender of the triggered event.</param>
        /// <param name="data">The data send by the sender.</param>
        public void OnDragStateChange(Component sender, object data)
        {
            if (data is not bool state) return;
            _canDrag = state;
        }
    }
}
