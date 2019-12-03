using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    // Variables to hold input field
    public InputField username;
    public InputField password;
    public Text errorText;

    // public static strings to hold text that can be accessed by other scripts
    public static string user;
    public static string pass;
    public static string UserId;

    void Start()
    {
    }

    // To set an error, you can set errorText.text
    // Maybe potentially pass in a parameter and use string concatonation for each error
    // This one is currently not being called, unlike Register's version
    public void Error(string err)
    {
        errorText.text = "Error: " + err;
    }


    // set static strings to input field text
    // compare strings to Register script's public static strings
    // display "Success" or "Failure"
    // triggered by button onclick event
    public void getLogin()
    {
        user = username.text;
        pass = password.text;
        StartCoroutine(login());
    }

    public IEnumerator login()
    {
        string send = "{ \"Email\": \"" + user + "\", \"Password\": \"" + pass + "\", \"RememberMe\": \"false\" }";
        Debug.Log(send);
        byte[] myData = System.Text.Encoding.UTF8.GetBytes(send);
        UnityWebRequest www = UnityWebRequest.Put("https://cis174gamewebsite.azurewebsites.net/api/user/login", myData);
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
            Error("Login Failed. Please try again.");
        }
        else
        {
            Debug.Log("Upload complete!");
            errorText.text = "";

            string json = www.downloadHandler.text;
            JArray parsedArray = JArray.Parse(json);
            foreach (JObject parsedObject in parsedArray.Children<JObject>())
            {
                foreach (JProperty parsedProperty in parsedObject.Properties())
                {
                    string propertyName = parsedProperty.Name;
                    if (propertyName.Equals("id"))
                    {
                        string propertyValue = (string)parsedProperty.Value;
                        UserId = propertyValue;
                        Debug.Log(UserId);
                        break;
                    }
                }
            }
            SceneManager.LoadScene("main");
        }
    }
}
