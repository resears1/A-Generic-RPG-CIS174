using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadArea : MonoBehaviour
{
    public string level;

    public string exitPoint;

    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.name == "Player")
        {
            SceneManager.LoadScene("main");
            player.startPoint = exitPoint;
        }
    }
}
