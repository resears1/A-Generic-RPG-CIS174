using System.Collections;
using System.Collections.Generic;
using System.Text;
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
            InsertNewScore();
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
        string send = "{ \"Score\": \"" + Score.score + "\" \"UserId\": \"" + UserId + "\" }";
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
}


