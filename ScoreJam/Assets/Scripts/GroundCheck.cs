using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool PUNI = true;

    void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant est le joueur (Player)
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("1");
            PUNI = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Réinitialise PUNI lorsque le joueur quitte la zone de détection
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("0");
            PUNI = true;
        }
    }
}