using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    // Variables to hold input field
    public InputField userIn;
    public InputField passIn;

    // public static strings to hold text that can be accessed by other scripts
    public static string user;
    public static string pass;

    // set static strings to input field text
    // triggered by button onclick event
    public void setUser()
    {
        user = userIn.text;
        pass = passIn.text;
    }
}
