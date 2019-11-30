using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int enemyHpMax;
    public int enemyHpCurrent;

    void Start()
    {
        enemyHpCurrent = enemyHpMax;
    }


    void Update()
    {
        if (enemyHpCurrent <= 0)
        {
            Destroy(gameObject);
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
