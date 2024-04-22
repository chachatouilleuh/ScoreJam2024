using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public int sceneIndex; // Index de la scène à charger

    public void RestartGame()
    {
        // Charger la scène en utilisant son index
        SceneManager.LoadScene(0);
        Debug.Log("niga");
    }
}
