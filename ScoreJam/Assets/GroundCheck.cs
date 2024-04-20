using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool PUNI = true ;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("1");
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("0");
    }
}
