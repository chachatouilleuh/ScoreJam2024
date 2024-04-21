using System.Collections;
using UnityEngine;

public class Collectible : MonoBehaviour
{ 
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>(); // Trouver l'instance du ScoreManager
            if (scoreManager != null)
            {
                StartCoroutine(UpdateScore(scoreManager)); // Passer l'instance trouvée au coroutine
            }
        }
    }

    IEnumerator UpdateScore(ScoreManager scoreManager)
    {
        yield return scoreManager.score++;
        Destroy(gameObject);
    }
}