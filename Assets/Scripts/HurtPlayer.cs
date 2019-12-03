using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{

    public int setDamage;

    void Start()
    {
        setDamage = 3;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
         {
            collision.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(setDamage);
         }
    }

    void Update()
    {
        
    }


}
