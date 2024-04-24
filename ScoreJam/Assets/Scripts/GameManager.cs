using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public HighScoreManager HighScoreManager;
    public ScoreManager ScoreManager;
    public PlayerManager PlayerManager;
    public AudioManager AudioManager;

    public CinemachineVirtualCamera loseCam;
    public CinemachineVirtualCamera winCam;

    public GameObject canvasLose;
    public GameObject canvasWin;
    public GameObject fadeBlack;

    public bool endGame;

    private void Awake()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        PlayerManager.playerCamera.gameObject.SetActive(true);
        loseCam.gameObject.SetActive(false);
        winCam.gameObject.SetActive(false);
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
        float duration = .75f;
        float currentTime = 0f;
        
        PlayerManager.playerAnimator.SetBool("Die", true);
        PlayerManager.playerAudioSource.clip = PlayerManager.DieClip;
        PlayerManager.playerAudioSource.Play();
        fadeBlack.SetActive(true);

        yield return new WaitForSeconds(2);

        /*
        //Diminuer le temps de 1 à 0 progressivement
        while (currentTime < duration)
        {
            Time.timeScale = Mathf.Lerp(1f, 0.5f, currentTime / duration);
            currentTime += 0.001f;
            yield return null;
        }

        //Augmenter le temps de 0 à 1 progressivement
        while (currentTime < duration)
        {
            Time.timeScale = Mathf.Lerp(0.5f, 1f, currentTime / duration);
            currentTime += 0.1f;
            yield return null;
        }
        */

        // Si le joueur bat le high score actuel
        if (ScoreManager.score >= HighScoreManager.highScore)
        {
            HighScoreManager.DisplayEnterName();
            winCam.gameObject.SetActive(true);
            canvasWin.SetActive(true);
            PlayerManager.playerCamera.gameObject.SetActive(false);
            //Debug.Log("Le joueur a battu le high score actuel.");
            // Activer le champ de texte pour que le joueur puisse entrer son nom
        
            // Attendre que le champ de texte soit activé
            yield return new WaitWhile(() => HighScoreManager.enterNameField.gameObject.activeSelf);
            
            // Met à jour le high score
            HighScoreManager.UpdateHighScore(ScoreManager.score);
            yield return HighScoreManager.SubmitScoreRoutine(ScoreManager.score);
        }
        else
        {
            loseCam.gameObject.SetActive(true);
            canvasLose.SetActive(true);
            PlayerManager.playerCamera.gameObject.SetActive(false);
            //Debug.Log("Le joueur n'a pas battu le high score actuel : " + ScoreManager.score + " < " +  HighScoreManager.highScore);
        }
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        yield return new WaitForSeconds(15);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
