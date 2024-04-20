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
        // V�rifie si CubeColl est en collision avec quelque chose
        colled = CubeColl.GetComponent<GroundCheck>().PUNI;

        agroed = enemiDetection.GetComponent<EnnemiDetection>().PlayerInColl;

        // Si CubeColl n'est pas en collision, r�duit le nombre de trys et attend 1 seconde avant de r�initialiser colledJunior
        if (!colled && !colledJunior)
        {
            colledJunior = true;
            trys--;
            StartCoroutine(ResetColledJunior());
        }

        // V�rifie si le joueur est d�tect� ou si l'ennemi est d�j� en combat, et le cas �ch�ant, d�place l'ennemi vers la position du joueur
        if (trys <= 0)
        {
            if (agroed || Incombat)
            {
                // V�rifie si ennemi n'est pas null avant d'acc�der � ses propri�t�s
                if (ennemi != null && PLayer != null)
                {
                    ennemi.SetDestination(PLayer.position);
                    Incombat = true;
                }
            }
        }
    }

    // Coroutine pour r�initialiser colledJunior apr�s 1 seconde
    IEnumerator ResetColledJunior()
    {
        yield return new WaitForSeconds(1);
        colledJunior = false;
    }
}
