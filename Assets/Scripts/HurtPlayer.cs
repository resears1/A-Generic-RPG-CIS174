using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int enemyDamage;

    public EnemyStats slime;

    void Start()
    {
        slime = FindObjectOfType<EnemyStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
         {
            collision.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(enemyDamage);
         }
    }

    void Update()
    {
        enemyDamage = slime.enemyAtk;
    }


}
