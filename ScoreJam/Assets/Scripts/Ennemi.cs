using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class Ennemi : MonoBehaviour
{
    public GameManager GameManager;
    
    public Animator GarbageAnimControler;
    public GameObject cubePrefab;
    public float jumpHeight = 10;
    public float cubeLifetime = 5;

    public NavMeshAgent ennemi;
    public Transform PLayer;
    public VisualEffect vfxEyes;
    public VisualEffect vfxSmoke;

    private bool isCollided;
    public bool Incombat;

    private GameObject spawnedCube;
    public int InitTry;
    public int trys;

    private AudioSource ennemiAudioSource;
    public AudioClip hitClip;
    public AudioClip monsterClip;

    void Start()
    {
        trys = Random.Range(0, 8);
        InitTry = trys;
        ennemiAudioSource = GetComponent<AudioSource>();
        ennemiAudioSource.clip = hitClip;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isCollided && other.CompareTag("Player"))
        {
            isCollided = true;
            trys--;
            GarbageAnimControler.SetBool("Shake", true);
            StartCoroutine(ResetAnimationBoolAfterDelay(GarbageAnimControler.GetCurrentAnimatorStateInfo(0).length));

            if (trys >= 0)
            {
                ennemiAudioSource.clip = hitClip;
                ennemiAudioSource.pitch = Random.Range(0.9f, 1.1f); // Modifiez le pitch de façon aléatoire
                ennemiAudioSource.Play();
                
                Vector3 spawnPosition = transform.position + Vector3.up * 3; // Ajouter un décalage vers le haut
                spawnedCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
                Rigidbody rb = spawnedCube.GetComponent<Rigidbody>();
                rb.AddForce(Vector3.up * Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight), ForceMode.VelocityChange);
                rb.AddForce(Vector3.forward * Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight), ForceMode.VelocityChange);
                StartCoroutine(DestroyCubeAfterDelay(spawnedCube, cubeLifetime));
            }
            else if (trys <= 0 && !Incombat)
            {
                Incombat = true;
                StartCoroutine(SwitchSound());
                vfxEyes.gameObject.SetActive(true);
                vfxSmoke.gameObject.SetActive(true);
            } 
            else if(trys < 0 && Incombat)
            {
                GameManager.PlayerManager.HP--;
            }
            StartCoroutine(ResetColledJunior());
        }
    }

    void Update()
    {
        if (Incombat)
            ennemi.SetDestination(PLayer.position);
    }

    IEnumerator SwitchSound()
    {
        ennemiAudioSource.Play();
        yield return new WaitForSeconds(hitClip.length);
        ennemiAudioSource.clip = monsterClip;
        ennemiAudioSource.loop = true;
        ennemiAudioSource.Play();
    }
    IEnumerator ResetColledJunior()
    {
        yield return new WaitForSeconds(1);
        isCollided = false;
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
