using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Test Collision");
            
        if (collision.CompareTag("Unit"))
        {
            Debug.Log("Unité rencontrée");
            //_isMoving = false;
        }

        if (collision.CompareTag("Castle"))
        {
            Debug.Log("Chateau rencontré");
            //_isMoving = false;
        }
    }
}