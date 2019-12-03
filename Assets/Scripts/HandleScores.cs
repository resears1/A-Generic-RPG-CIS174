using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    class HandleScores
    {
        public static ArrayList scoreList;

        public static void InsertNewScore()
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
            string send = "{ \"Score\": \"" + Score.score + "\", \"UserId\": \"" + UserId + "\" }";
            Debug.Log(send);
            byte[] myData = System.Text.Encoding.UTF8.GetBytes(send);
            UnityWebRequest www = UnityWebRequest.Put("https://cis174gamewebsite.azurewebsites.net/api/HighScore/addscore", myData);
            www.SetRequestHeader("Content-Type", "application/json");
            www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Could not add new score - " + www.error);
            }
            else
            {
                while (!www.isDone)
                {
                    Debug.Log("Waiting... Download progress: " + www.downloadProgress);
                }
                Debug.Log("New Score Inserted!");
            }
        }

        public static ArrayList GetUserScores()
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
                    while (!request.isDone)
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
                return scoreList;
            }
        }
    }
}
