using System;
using System.Collections;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public TMP_Text highScoreText;
    public TMP_Text playerName;
    public TMP_InputField enterNameField;
    
    public int highScore;
    private string leaderboardID = "21701";

    public int playerListCount = 1;

    void Start()
    {
        StartCoroutine(SetUpRoutine());
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = highScore.ToString();
    }

    public void DisplayEnterName()
    {
        Debug.Log("je display");
        enterNameField.gameObject.SetActive(true);
        enterNameField.ActivateInputField();
    }
    
    public void SetPlayerName()
    {
        
        LootLockerSDKManager.SetPlayerName(enterNameField.text , (response) =>
        {
            if (response.success)
            {
                Debug.Log("Name successfully set");
                enterNameField.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Name not set " + response.errorData);
            }
        });
        
    }

    
    IEnumerator SetUpRoutine()
    {
        yield return LoginRoutine();
        yield return FetchTopHighScoreRoutine(playerListCount);
    }

    public void UpdateHighScore(int score)
    {
        if (score >= highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = highScore.ToString();
        }
    }
    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                PlayerPrefs.SetString("PlayerID",response.player_id.ToString());
                Debug.Log("logged in");
                done = true;
            }
            else
            {
                done = true;
                Debug.Log("not logged");
            }
                
        });
        yield return new WaitWhile(() => !done);
    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;

        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload,leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("score uploaded");
                done = true;
            }
            else
            {
                done = true;
                Debug.Log("score not uploaded");
            }
        });
        yield return new WaitWhile(() => !done);
    }

    public IEnumerator FetchTopHighScoreRoutine(int count)
    {
        bool done = false;
        string tempPlayerNames = "";
        string tempPlayerScores = "";

        LootLockerSDKManager.GetScoreList(leaderboardID, count, 0, (response) =>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; ++i)
                {
                    //tempPlayerNames += members[i].rank + ". ";
                    if (members[i].player.name != "")
                        tempPlayerNames += members[i].player.name;
                    else
                        tempPlayerNames += members[i].player.id;

                    //tempPlayerNames += "\n";

                    // Ajoutez également les scores dans la chaîne temporaire des scores
                    tempPlayerScores += members[i].score + "\n";

                    // Mettez à jour le highScore ici pour qu'il corresponde au dernier score de la liste
                    highScore = members[i].score;

                    // Sortez de la boucle dès que vous avez ajouté un nom
                    break;
                }
                Debug.Log("score fetched");
            }
            else
            {
                Debug.Log("score not fetched");
            }

            // Assurez-vous que le drapeau done est défini pour indiquer que la requête est terminée
            done = true;
        });

        // Attendre que la requête soit terminée avant de continuer
        yield return new WaitWhile(() => !done);

        // Assurez-vous d'attribuer les chaînes de noms et de scores après que la requête soit terminée
        playerName.text = tempPlayerNames;
        highScoreText.text = tempPlayerScores;
    }

}