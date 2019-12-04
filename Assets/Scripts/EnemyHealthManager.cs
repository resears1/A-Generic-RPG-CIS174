using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public PlayerStats playerStats;

    public EnemyStats slime;

    public int enemyHpMax;
    public int enemyHpCurrent;

    public int XP;
    public int ScoreReward;

    public HurtPlayer enemyDmg;

    public float timeBeforeLevel;
    private float timer;

    void Start()
    {
        enemyDmg = FindObjectOfType<HurtPlayer>();
        slime = FindObjectOfType<EnemyStats>();
        playerStats = FindObjectOfType<PlayerStats>();
        enemyHpMax = slime.enemyMaxHp;
        enemyHpCurrent = enemyHpMax;
        timer = timeBeforeLevel;
    }


    void Update()
    {
        if (enemyHpCurrent <= 0)
        {
            Destroy(gameObject);
            Score.score += slime.enemyScore;
            playerStats.AddXP(slime.enemyXp);
        }

        if (enemyHpMax < slime.enemyMaxHp)
        {
            enemyHpMax = slime.enemyMaxHp;
            enemyHpCurrent = enemyHpMax;
        }
    }

    public void HurtEnemy(int damage)
    {
        enemyHpCurrent -= damage;
    }

    public void SetFullHP()
    {
        enemyHpCurrent = enemyHpMax;
    }
}
