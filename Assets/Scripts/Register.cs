using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    // Variables to hold input field
    public InputField userIn;
    public InputField passIn;
    public InputField passConfirmIn;
    public Text errorText;

    // public static strings to hold text that can be accessed by other scripts
    public static string user;
    public static string pass;
    public static string passConfirm;

    // To set an error, you can set errorText.text
    // Maybe potentially pass in a parameter and concatonate the error for all errors
    // Like I am doing here. This is being called on line 40 as a placeholder.
    public void Error(string err)
    {
        errorText.text = "Error: " + err;
    }


    // set static strings to input field text
    // triggered by button onclick event
    public void setUser()
    {
        user = userIn.text;
        pass = passIn.text;
        passConfirm = passConfirmIn.text;

        // Placeholder to confirm password
        if (passConfirm != pass)
        {
            Error("Passwords do not match.");
        }

        UnityWebRequest www = UnityWebRequest.Post("https://cis174gamewebsite.azurewebsites.net/api/user/reg", "{ \"Email\": \"" + user + "\" \"Password\": \"" + pass + "\" \"ConfirmPassword\": \"" + pass + "\"}");
        while (!www.isDone && (www.error == null || www.error.Equals("")))
        {
            Debug.Log(www.downloadProgress + " - " + www.downloadedBytes);
        }
        if(www.error != null && !www.error.Equals(""))
        {
            Debug.LogError("Request error: " + www.error);
        }
        Debug.Log(www.responseCode + " User Successfully Registered");
    }
}
