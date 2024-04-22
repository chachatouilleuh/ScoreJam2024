using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public HighScoreManager HighScoreManager;
    public ScoreManager ScoreManager;
    public PlayerManager PlayerManager;

    private bool endGame;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    public void EndGame()
    {
        if (!endGame)
        {
            StartCoroutine(DieRoutine());
            endGame = true;
        }
    }
    
    IEnumerator DieRoutine()
    {
        float duration = 1f;
        float currentTime = 0f;

        // Diminuer le temps de 1 à 0 progressivement
        while (currentTime < duration)
        {
            Time.timeScale = Mathf.Lerp(1f, 0.25f, currentTime / duration);
            currentTime += 0.001f;
            yield return null;
        }

        // Si le joueur bat le high score actuel
        if (ScoreManager.score >= HighScoreManager.highScore)
        {
            Debug.Log("Le joueur a battu le high score actuel.");
            HighScoreManager.DisplayEnterName(); // Activer le champ de texte pour que le joueur puisse entrer son nom
        
            // Attendre que le champ de texte soit activé
            yield return new WaitWhile(() => HighScoreManager.enterNameField.gameObject.activeSelf);

            // Met à jour le high score
            HighScoreManager.UpdateHighScore(ScoreManager.score);
            yield return HighScoreManager.SubmitScoreRoutine(ScoreManager.score);
        }
        else
        {
            Debug.Log("Le joueur n'a pas battu le high score actuel : " + ScoreManager.score + " < " +  HighScoreManager.highScore);
        }
        
        Debug.Log("Le nom est validé je reprends le temps");
        currentTime = 0.25f;
        
        // Augmenter le temps de 0 à 1 progressivement
        while (currentTime < duration)
        {
            Time.timeScale = Mathf.Lerp(0.25f, 1f, currentTime / duration);
            currentTime += 0.01f;
            yield return null;
        }
        Debug.Log("Le temps redevient à la normale");
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
