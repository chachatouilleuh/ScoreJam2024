using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int HP;
    public GameManager GameManager;
    public Animator playerAnimator;
    public AudioSource playerAudioSource;
    public AudioClip biteClip;
    public AudioClip DieClip;

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
