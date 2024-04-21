using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool PUNI = true;

    void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet entrant est le joueur (Player)
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("1");
            PUNI = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // R�initialise PUNI lorsque le joueur quitte la zone de d�tection
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("0");
            PUNI = true;
        }
    }
}