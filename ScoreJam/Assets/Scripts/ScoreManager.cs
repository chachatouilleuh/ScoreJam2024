using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public int score;
 
    private void Start()
    {
        score = 0;
    }

    void Update()
    {
        scoreText.text = "Score: " + score;
    }
    
}