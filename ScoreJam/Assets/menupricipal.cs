using UnityEngine;
using UnityEngine.SceneManagement;

public class menuprincipal : MonoBehaviour
{
    // Méthode appelée lorsque le bouton est cliqué pour changer de scène
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }//atteyer de pt les cuilles stp
}