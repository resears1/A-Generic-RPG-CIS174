using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int enemyAtk;
    public int enemyCurrentHp;
    public int enemyMaxHp;
    public int enemyXp;
    public int enemyScore;

    public float timeBeforeLevel;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeBeforeLevel;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            enemyAtk += 3;
            enemyMaxHp += 5;
            enemyCurrentHp = enemyMaxHp;
            enemyXp += 5;
            enemyScore += 3;
            timer = timeBeforeLevel;
        }
    }
}
