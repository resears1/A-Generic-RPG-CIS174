using Assets.Scripts;
using Newtonsoft.Json.Linq;
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
            HandleScores.InsertNewScore();
            ArrayList scores = HandleScores.GetUserScores();
            gameObject.SetActive(false);
            UIManager.CreateLeaderboard(scores);
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

}


