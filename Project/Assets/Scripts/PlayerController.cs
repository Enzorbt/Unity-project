using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera camera_;

    private Vector3 origin;
    private Vector3 difference;

    private bool drag = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            difference = camera_.ScreenToWorldPoint(Input.mousePosition) - camera_.transform.position;

            if (drag == false)
            {
                drag = true;
                origin = camera_.ScreenToWorldPoint(Input.mousePosition);
            }
            
        }
        else
        {
            drag = false;
        }

        if (drag)
        {
            // camera_.transform.position = origin - difference;
            camera_.transform.position = new Vector3(origin.x - difference.x,0, origin.z);
        }
        
        
    }
}
