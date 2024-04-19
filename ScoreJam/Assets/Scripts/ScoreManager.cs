using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;

    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }
}