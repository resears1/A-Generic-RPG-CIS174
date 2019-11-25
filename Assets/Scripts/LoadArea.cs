using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadArea : MonoBehaviour
{
    public string level;

    void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.name == "Player")
        {
            SceneManager.LoadScene(level);
        }
    }
}
