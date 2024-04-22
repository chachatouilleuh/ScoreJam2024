using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Collectible : MonoBehaviour
{
    private GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             // Trouver l'instance du ScoreManager
            if (gameManager != null)
            {
                gameManager.ScoreManager.StartCoroutine(UpdateScore(gameManager)); // Passer l'instance trouvée au coroutine
            }
        }
    }

    IEnumerator UpdateScore(GameManager gameManager)
    {
        gameManager.PlayerManager.playerAudioSource.clip = gameManager.PlayerManager.biteClip;
        gameManager.PlayerManager.playerAudioSource.pitch = Random.Range(0.9f, 1.1f); ;
        gameManager.PlayerManager.playerAudioSource.Play();
        yield return gameManager.ScoreManager.score++;
        Destroy(gameObject);
    }
}