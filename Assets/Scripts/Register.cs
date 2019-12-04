using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

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
    public static string UserId;
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
        StartCoroutine(RegisterUser());
    }

    public IEnumerator RegisterUser()
    {
        passConfirm = passConfirmIn.text;

        // Placeholder to confirm password
        if (passConfirm != pass)
        {
            Debug.LogError("Passwords do not match.");
            Error("Passwords do not match.");
        }
        else
        {
            string send = "{ \"Email\": \"" + user + "\", \"Password\": \"" + pass + "\", \"ConfirmPassword\": \"" + passConfirm + "\" }";
            Debug.Log(send);
            byte[] myData = System.Text.Encoding.UTF8.GetBytes(send);
            UnityWebRequest www = UnityWebRequest.Put("https://cis174gamewebsite.azurewebsites.net/api/user/reg", myData);
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
                Error("An error occurred during registration.\nPlease try again");
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

}
