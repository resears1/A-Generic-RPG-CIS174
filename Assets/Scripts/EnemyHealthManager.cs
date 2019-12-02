using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int enemyHpMax;
    public int enemyHpCurrent;

    private PlayerStats playerStats;

    public int XP;
    public int ScoreReward;

    void Start()
    {
        enemyHpCurrent = enemyHpMax;

        playerStats = FindObjectOfType<PlayerStats>();
    }


    void Update()
    {
        if (enemyHpCurrent <= 0)
        {
            Destroy(gameObject);
            Score.score += ScoreReward;
            playerStats.AddXP(XP);
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
