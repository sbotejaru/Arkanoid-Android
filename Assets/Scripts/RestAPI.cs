using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RestAPI : MonoBehaviour
{
    private string baseURL = "https://localhost:7288";
    public Text HighscoreText;
    public InputField UsernameField;
    public int Highscore { get; set; }
    private string username;

    void Start()
    {
        //StartCoroutine(GetHighestScore());
    }

    IEnumerator GetHighscoreByUsername()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(baseURL + $"/get-highscore-by-username/{this.username}"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError(request.error);
            else
            {
                string json = request.downloadHandler.text;

                SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);

                if (stats["points"] != null)
                {
                    string scoreString = stats["points"].ToString().PadLeft(5, '0');
                    this.Highscore = int.Parse(stats["points"]);

                    HighscoreText.text = $"SCOR MAX\n{scoreString}";
                }
            }
        }
    }

    IEnumerator GetHighestScore()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(baseURL + "/get-highest-score"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
                Debug.LogError(request.error);
            else
            {
                string json = request.downloadHandler.text;
                SimpleJSON.JSONNode stats = SimpleJSON.JSON.Parse(json);

                string scoreString = stats["points"].ToString().PadLeft(5, '0');

                HighscoreText.text = $"SCOR MAX\n{scoreString}";
            }
        }
    }

    public void SetUsername()
    {
        this.username = UsernameField.text;
    }
}
