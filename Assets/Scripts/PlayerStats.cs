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

    public EnemyHealthManager enemy;

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
            currentXP = 0;
            healthManager.hpMax += 10;
            healthManager.hpCurrent = healthManager.hpMax;
            atk += 5;
        }
    }

    public void AddXP(int XPtoGive)
    {
        currentXP += XPtoGive;
    }
}
