using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiDetection : MonoBehaviour
{
    private bool secur = false;

    void Update()
    {
        if(PlayerInColl&&secur==false) { PlayerInColl = false;secur = true; }
    }

    public bool PlayerInColl = false;
    void OnTriggerEnter(Collider PlayerObj)
    {
        Debug.Log("1");
        PlayerInColl = true;
    }
    void OnTriggerExit(Collider PlayerObj)
    {
        Debug.Log("0");
        PlayerInColl = false;
    }
}
