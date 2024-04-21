using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public HighScoreManager HighScoreManager;
    public ScoreManager ScoreManager;
    public PlayerManager PlayerManager;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    public void EndGame()
    {
        StartCoroutine(DieRoutine());
    }
    
    IEnumerator DieRoutine()
    {
        // Met à jour le high score
        HighScoreManager.UpdateHighScore(ScoreManager.score);
    
        float duration = 1f;
        float currentTime = 0f;

        // Diminuer le temps de 1 à 0 progressivement
        while (currentTime < duration)
        {
            Time.timeScale = Mathf.Lerp(1f, 0f, currentTime / duration);
            currentTime += 0.001f;
            yield return null;
        }

        // Si le joueur bat le high score actuel
        if (ScoreManager.score > HighScoreManager.highScore)
        {
            yield return HighScoreManager.SubmitScoreRoutine(ScoreManager.score);

            HighScoreManager.DisplayEnterName(); // Activer le champ de texte pour que le joueur puisse entrer son nom
        
            // Attendre que le champ de texte soit désactivé
            yield return new WaitUntil(() => !HighScoreManager.enterNameField.gameObject.activeSelf);
        
            HighScoreManager.SetPlayerName();
        }

        currentTime = 0f;

        // Augmenter le temps de 0 à 1 progressivement
        while (currentTime < duration)
        {
            Time.timeScale = Mathf.Lerp(0f, 1f, currentTime / duration);
            currentTime += 0.01f;
            yield return null;
        }

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
