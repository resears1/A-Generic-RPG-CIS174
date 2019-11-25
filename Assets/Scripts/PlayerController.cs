using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Animator animate;
    private Rigidbody2D myRigidbody;

    private bool isPlayerMoving;
    private Vector2 lastMove;

    private static bool doesPlayerExist;

    void Start()
    {
        animate = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        if (!doesPlayerExist)
        {
            doesPlayerExist = true;
            DontDestroyOnLoad(transform.gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        isPlayerMoving = false;

        if (Input.GetAxisRaw("Vertical") > 0.5 || Input.GetAxisRaw("Vertical") < -0.5)
        {
            // transform.Translate(new Vector2(0f, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime));
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, Input.GetAxisRaw("Vertical") * speed);
            isPlayerMoving = true;
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }

        if (Input.GetAxisRaw("Horizontal") > 0.5 || Input.GetAxisRaw("Horizontal") < -0.5)
        {
            // transform.Translate (new Vector2(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0f));
            myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, myRigidbody.velocity.y);
            isPlayerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }

        if(Input.GetAxisRaw("Horizontal") < 0.5 && Input.GetAxisRaw("Horizontal") > -0.5)
        {
            myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
        }

        if (Input.GetAxisRaw("Vertical") < 0.5 && Input.GetAxisRaw("Vertical") > -0.5)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0f);
        }

        animate.SetFloat("MovementY", Input.GetAxisRaw("Vertical"));
        animate.SetFloat("MovementX", Input.GetAxisRaw("Horizontal"));
        animate.SetBool("IsPlayerMoving", isPlayerMoving);
        animate.SetFloat("LastMoveX", lastMove.x);
        animate.SetFloat("LastMoveY", lastMove.y);
    }
}
