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
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = highScore.ToString();
        StartCoroutine(SetUpRoutine());
    }

    public void DisplayEnterName()
    {
        enterNameField.gameObject.SetActive(true);
        enterNameField.Select(); // Met le focus sur le champ de texte
        enterNameField.ActivateInputField();
    }
    
    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(enterNameField.text , (response) =>
        {
            if (response.success)
            {
                Debug.Log("Name successfully set");
                enterNameField.gameObject.SetActive(false); // Désactiver le champ de texte après avoir envoyé le nom
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
        if (score > highScore)
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
        LootLockerSDKManager.GetScoreList(leaderboardID, count,0, (response) =>
        {
            if (response.success)
            {
                string tempPlayerNames = "Name\n";
                string tempPlayerScores = "High Score\n";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; ++i)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if (members[i].player.name != "")
                        tempPlayerNames += members[i].player.name;
                    else
                        tempPlayerNames += members[i].player.id;
                    
                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                    
                    playerName.text = tempPlayerNames;
                    highScoreText.text = tempPlayerScores;
                }
                Debug.Log("score fetched");
                done = true;
            }
            else
            {
                done = true;
                Debug.Log("score not fetched");
            }
        });
        yield return new WaitWhile(() => !done);
    }
}