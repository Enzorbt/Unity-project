using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera _camera;

    private Vector3 origin;
    private Vector3 difference;

    private bool drag = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) // Clicking in left mouse button
        {
            difference = _camera.ScreenToWorldPoint(Input.mousePosition) - _camera.transform.position;

            if (drag == false)
            {
                drag = true;
                origin = _camera.ScreenToWorldPoint(Input.mousePosition);
            }
            
        }
        else // If not clicking in left mouse button drag = false
        {
            drag = false;
        }

        if (drag)
        {
            MoveCameraHorizontally(difference.x);
        }
    }

    public void MoveCameraHorizontally(float offset)
    {
        // move camera horizontally
        _camera.transform.position = new Vector3(origin.x - offset,origin.y, origin.z);
    }
}
