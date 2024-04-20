using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemi : MonoBehaviour
{
    public bool colledJunior = false;
    public GameObject CubeColl;
    public bool colled = false;

    //public GameObject sphereColl;
    public int trys;
    public NavMeshAgent ennemi;
    public Transform PLayer;
    public bool Incombat;

    public GameObject enemiDetection;
    public bool agroed = false;

    // Start is called before the first frame update
    void Start()
    {
        trys = Random.Range(0, 8);
    }

    // Update is called once per frame
    void Update()
    {
        // Vérifie si CubeColl est en collision avec quelque chose
        colled = CubeColl.GetComponent<GroundCheck>().PUNI;

        agroed = enemiDetection.GetComponent<EnnemiDetection>().PlayerInColl;

        // Si CubeColl n'est pas en collision, réduit le nombre de trys et attend 1 seconde avant de réinitialiser colledJunior
        if (!colled && !colledJunior)
        {
            colledJunior = true;
            trys--;
            StartCoroutine(ResetColledJunior());
        }

        // Vérifie si le joueur est détecté ou si l'ennemi est déjà en combat, et le cas échéant, déplace l'ennemi vers la position du joueur
        if (trys <= 0)
        {
            if (agroed || Incombat)
            {
                // Vérifie si ennemi n'est pas null avant d'accéder à ses propriétés
                if (ennemi != null && PLayer != null)
                {
                    ennemi.SetDestination(PLayer.position);
                    Incombat = true;
                }
            }
        }
    }

    // Coroutine pour réinitialiser colledJunior après 1 seconde
    IEnumerator ResetColledJunior()
    {
        yield return new WaitForSeconds(1);
        colledJunior = false;
    }
}
