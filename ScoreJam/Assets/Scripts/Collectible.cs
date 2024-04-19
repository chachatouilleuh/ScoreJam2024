using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int scoreValue = 0;
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            scoreManager.IncreaseScore(scoreValue);
            Destroy(gameObject);
        }
    }
}