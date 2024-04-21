using System.Collections;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public TMP_Text highScoreText;
    public int highScore;
    private string leaderboardID = "21701";

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "High Score: " + highScore;
        StartCoroutine(LoginRoutine());
    }

    public void UpdateHighScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = "High Score: " + highScore;
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
    
}