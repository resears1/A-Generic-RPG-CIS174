using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealthManager : MonoBehaviour
{
    public int hpMax;
    public int hpCurrent;

    void Start()
    {
        hpCurrent = hpMax;
    }


    void Update()
    {
        if (hpCurrent <= 0)
        {
            gameObject.SetActive(false);
            Upload();
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

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("myField", "myData");

        UnityWebRequest www = UnityWebRequest.Post("https://cis174gamewebsite.azurewebsites.net/api/HighScore/addscore", "{ \"Score\": \"" + Score.score + "\" \"UserId\": \"" + Login.user + "\" }");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
}


