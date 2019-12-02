using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    // Variables to hold input field
    public InputField username;
    public InputField password;

    // public static strings to hold text that can be accessed by other scripts
    public static string user;
    public static string pass;

    void Start()
    {
        StartCoroutine(getText());
    }

    // set static strings to input field text
    // compare strings to Register script's public static strings
    // display "Success" or "Failure"
    // triggered by button onclick event
    public void getLogin()
    {
        user = username.text;
        pass = password.text;

        if (user == Register.user && pass == Register.pass)
        {
            Debug.Log("Success");
        } else
        {
            Debug.Log("Failure");
        }

        // Debug information
    }

    public IEnumerator getText()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://cis174gamewebsite.azurewebsites.net/api/register/5");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }
}
