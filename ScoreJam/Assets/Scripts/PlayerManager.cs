using System.Collections;
using Cinemachine;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int HP;
    public GameManager GameManager;
    public Animator playerAnimator;
    public AudioSource playerAudioSource;
    public AudioClip biteClip;
    public AudioClip DieClip;
    public CinemachineFreeLook playerCamera;

    private void Start()
    {
        HP = 1;
        playerAudioSource.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (HP <= 0)
        {
            GameManager.EndGame();
        };
    }
}
