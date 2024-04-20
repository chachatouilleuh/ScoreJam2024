using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemi : MonoBehaviour
{
    public bool DoOnce = false;

    public Animator GarbageAnimControler;
    public GameObject cubePrefab; // Le prefab du cube que vous voulez instancier
    public GameObject poubelle; // La poubelle à côté de laquelle vous voulez instancier le cube
    public float maxSpawnDistance = 5f; // La distance maximale à laquelle le cube peut être instancié de la poubelle
    public float jumpHeight = 1f; // La hauteur du saut du cube
    public float cubeLifetime = 5f; // La durée de vie du cube avant qu'il ne soit détruit

    private GameObject spawnedCube; // Référence au cube instancié

    public bool colledJunior = false;
    public GameObject CubeColl;
    public bool colled = false;

    public int InitTry;

    public int trys;
    public NavMeshAgent ennemi;
    public Transform PLayer;
    public bool Incombat;

    public GameObject enemiDetection;
    public bool agroed = false;

    private bool animationPlaying = false;
    private float animationDuration = 2.0f; // Supposons que l'animation dure 2 secondes

    void Start()
    {
        trys = Random.Range(0, 8);
        InitTry = trys;
    }


    void OnTriggerEnter(Collider other)
    {






        if (trys != InitTry)
        {
            colled = CubeColl.GetComponent<GroundCheck>().PUNI;
            agroed = enemiDetection.GetComponent<EnnemiDetection>().PlayerInColl;

        }

        if (!colled && !colledJunior)
        {
            colledJunior = true;


            if (DoOnce == false)
            {
                trys--;
                spawnedCube = Instantiate(cubePrefab, poubelle.transform.position, Quaternion.identity);
                Rigidbody rb = spawnedCube.GetComponent<Rigidbody>();
                GarbageAnimControler.SetBool("Shake", true);
                animationPlaying = true;
                StartCoroutine(ResetAnimationBoolAfterDelay(animationDuration));
                if (rb != null)
                {
                    rb.AddForce(Vector3.up * Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight), ForceMode.VelocityChange);
                }
                StartCoroutine(DestroyCubeAfterDelay(spawnedCube, cubeLifetime));

                if (trys <= 0) 
                {
                    if ( Incombat==false)
                    {

                        ennemi.SetDestination(PLayer.position);
                        Incombat = true;
                        //GarbageAnimControler.SetBool("Shake", true);
                       // animationPlaying = true;
                        //StartCoroutine(ResetAnimationBoolAfterDelay(animationDuration));
                    }
                }
                DoOnce = true;
            }
            
            StartCoroutine(ResetColledJunior());
        }

        




    }



    void OnTriggerExit(Collider other)
    {
        DoOnce = false;
    }



    void Update()
    {
        if (Incombat)
        {
            ennemi.SetDestination(PLayer.position);
        }
    }

   

    IEnumerator ResetColledJunior()
    {
        yield return new WaitForSeconds(1);
        colledJunior = false;
    }

    IEnumerator ResetAnimationBoolAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GarbageAnimControler.SetBool("Shake", false);
        animationPlaying = false;
    }


    IEnumerator DestroyCubeAfterDelay(GameObject cube, float delay)
    {
        yield return new WaitForSeconds(delay);
        // Détruire le cube après le délai spécifié
        if (cube != null)
        {
            Destroy(cube);
        }
    }
}