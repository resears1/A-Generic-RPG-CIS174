using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


