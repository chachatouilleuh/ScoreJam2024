using UnityEngine;
using UnityEngine.SceneManagement;

public class menuprincipal : MonoBehaviour
{
    // M�thode appel�e lorsque le bouton est cliqu� pour changer de sc�ne
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }//atteyer de pt les cuilles stp
}