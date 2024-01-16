using System.Collections;
using System.Collections.Generic;
using Supinfo.Project;
using UnityEngine;

public class UnitCollision : MonoBehaviour
{
    private Unit unitScript; 
    private void Start()
    {
        unitScript = GetComponent<Unit>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Unit"))
        {
            Debug.Log("Unit find");
            unitScript.SetIsMoving(false);
        }

        if (collision.CompareTag("Castle"))
        {
            Debug.Log("Castle find");
            //unitScript.SetIsMoving(false);
        }
    }
}