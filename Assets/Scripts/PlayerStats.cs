using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int currentLvl;
    public int currentXP;
    public int[] toNextLvl;

    public int atk;

    public PlayerHealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {
        atk = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentXP >= toNextLvl[currentLvl])
        {
            currentLvl++;
            healthManager.hpMax += 10;
            healthManager.hpCurrent = healthManager.hpMax;
            atk += 3;
        }
    }

    public void AddXP(int XPtoGive)
    {
        currentXP += XPtoGive;
    }
}
