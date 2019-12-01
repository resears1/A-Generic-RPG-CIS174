using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    // Variables to hold input field
    public InputField username;
    public InputField password;

    // public static strings to hold text that can be accessed by other scripts
    public static string user;
    public static string pass;

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
        Debug.Log(username.text);
        Debug.Log(password.text);

        Debug.Log(Register.user);
        Debug.Log(Register.pass);
    }
}
