using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class Register : MonoBehaviour
{
    // Variables to hold input field
    public InputField userIn;
    public InputField passIn;

    // public static strings to hold text that can be accessed by other scripts
    public static string user;
    public static string pass;
    public static string UserId;

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
        string send = "{ \"Email\": \"" + user + "\", \"Password\": \"" + pass + "\", \"ConfirmPassword\": \"" + pass + "\" }";
        Debug.Log(send);
        byte[] myData = System.Text.Encoding.UTF8.GetBytes(send);
        UnityWebRequest www = UnityWebRequest.Put("https://cis174gamewebsite.azurewebsites.net/api/user/reg", myData);
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
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
        }

    }

}
