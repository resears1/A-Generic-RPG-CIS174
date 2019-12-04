using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D r;

    private bool isMoving;

    public float timeBetweenMove;
    private float timeBetweenMoveCounter;
    public float timeToMove;
    private float timeToMoveCounter;

    private Vector3 moveDirection;

    public float waitForReload;
    private bool isReloading;

    private GameObject player;

    void Start()
    {
        r = GetComponent<Rigidbody2D>();

        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
    }

    void Update()
    {
        if (isMoving)
        {
            timeToMoveCounter -= Time.deltaTime;
            r.velocity = moveDirection;

            if (timeToMoveCounter < 0f)
            {
                isMoving = false;
                timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
            }
        }
        else
        {
            timeBetweenMoveCounter -= Time.deltaTime;
            r.velocity = Vector2.zero;

            if (timeBetweenMoveCounter < 0f)
            {
                isMoving = true;
                timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

                moveDirection = new Vector3(Random.Range(-1f, 1f) * speed, Random.Range(-1f,1f) * speed, 0f);
            }
        }

        if (isReloading)
        {
            waitForReload -= Time.deltaTime;
            if (waitForReload < 0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                player.SetActive(true);
            }
        }


    }
}
