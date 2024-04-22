using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public int sceneIndex; // Index de la sc�ne � charger

    public void RestartGame()
    {
        // Charger la sc�ne en utilisant son index
        SceneManager.LoadScene(1);
        Debug.Log("niga");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
