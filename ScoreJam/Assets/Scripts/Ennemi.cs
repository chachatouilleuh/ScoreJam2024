using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class Ennemi : MonoBehaviour
{
    public Animator GarbageAnimControler;
    public GameObject cubePrefab;
    public float jumpHeight = 1f;
    public float cubeLifetime = 5f;
    //public GameObject poubelleHitBox;
    public NavMeshAgent ennemi;
    public Transform PLayer;
    public VisualEffect vfxEyes;
    public VisualEffect vfxSmoke;

    private bool colledJunior = false;
    private bool agroed = false;
    private bool animationPlaying = false;
    public bool Incombat = false;

    private GameObject spawnedCube;
    public int InitTry;
    public int trys;

    void Start()
    {
        trys = Random.Range(0, 8);
        InitTry = trys;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!colledJunior)
        {
            colledJunior = true;
            trys--;
            GarbageAnimControler.SetBool("Shake", true);

            if (trys >= 0)
            {
                StartCoroutine(ResetAnimationBoolAfterDelay(GarbageAnimControler.GetCurrentAnimatorStateInfo(0).length));
                spawnedCube = Instantiate(cubePrefab, transform.position, Quaternion.identity);
                Rigidbody rb = spawnedCube.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddForce(Vector3.up * Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight), ForceMode.VelocityChange);
                StartCoroutine(DestroyCubeAfterDelay(spawnedCube, cubeLifetime));
            }
            
            if (trys <= 0 && !Incombat)
            {
                Incombat = true;
                vfxEyes.gameObject.SetActive(true);
                vfxSmoke.gameObject.SetActive(true);
            }
            StartCoroutine(ResetColledJunior());
        }
    }

    void Update()
    {
        if (Incombat)
            ennemi.SetDestination(PLayer.position);
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
    }

    IEnumerator DestroyCubeAfterDelay(GameObject cube, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (cube != null)
            Destroy(cube);
    }
}
