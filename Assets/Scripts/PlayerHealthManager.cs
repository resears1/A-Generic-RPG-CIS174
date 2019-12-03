using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealthManager : MonoBehaviour
{
    public int hpMax;
    public int hpCurrent;
    public ArrayList scoreList;
    void Start()
    {
        hpCurrent = hpMax;
    }


    void Update()
    {
        if (hpCurrent <= 0)
        {
            StartCoroutine(InsertNewScore());
            GetUserScores();
            gameObject.SetActive(false);
        }
    }

    public void HurtPlayer(int damage)
    {
        hpCurrent -= damage;
    }

    public void SetFullHP()
    {
        hpCurrent = hpMax;
    }

    IEnumerator InsertNewScore()
    {
        string UserId;
        if(Login.UserId != null)
        {
            UserId = Login.UserId;
        } 
        else
        {
            UserId = Register.UserId;
        }
        string send = "{ \"Score\": \"" + Score.score + "\", \"UserId\": \"" + UserId + "\" }";
        Debug.Log(send);
        byte[] myData = System.Text.Encoding.UTF8.GetBytes(send);
        UnityWebRequest www = UnityWebRequest.Put("https://cis174gamewebsite.azurewebsites.net/api/HighScore/addscore", myData);
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Could not add new score - " + www.error);
        }
        else
        {
            Debug.Log("New Score Inserted!");
        }
    }

    void GetUserScores()
    {
        string UserId;
        if (Login.UserId != null)
        {
            UserId = Login.UserId;
        }
        else
        {
            UserId = Register.UserId;
        }

        string url = "http://cis174gamewebsite.azurewebsites.net/api/highscore/" + UserId;

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.SendWebRequest();
            scoreList = new ArrayList();
            if (request.error != null)
            {
                Debug.Log("Could not get scores - " + request.error);
            }
            else
            {
                while(!request.isDone)
                {
                    Debug.Log("Waiting... Download progress: " + request.downloadProgress);
                }
                Debug.Log("Scores Retrieved!");
                JArray parsedArray = JArray.Parse(request.downloadHandler.text);
                foreach (JObject parsedObject in parsedArray.Children<JObject>())
                {
                    foreach (JProperty parsedProperty in parsedObject.Properties())
                    {
                        string propertyName = parsedProperty.Name;
                        if (propertyName.Equals("score"))
                        {
                            scoreList.Add((string)parsedProperty.Value);
                        }
                    }
                }
            }
        }
    }

}


