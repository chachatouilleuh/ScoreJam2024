using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChek : MonoBehaviour
{
    public bool PUNI = true;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("1");
        PUNI = false;
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("0");
        PUNI = true;
    }
}