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

    public HurtPlayer enemyDmg;

    // Start is called before the first frame update
    void Start()
    {
        atk = 5;
        enemy = FindObjectOfType<EnemyHealthManager>();
        enemyDmg = FindObjectOfType<HurtPlayer>();
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
            enemyDmg.setDamage *= 2;
            enemy.enemyHpMax += 10;
            enemy.XP += 5;
        }
    }

    public void AddXP(int XPtoGive)
    {
        currentXP += XPtoGive;
    }
}
