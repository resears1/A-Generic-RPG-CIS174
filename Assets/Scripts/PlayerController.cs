using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Animator animate;
    private Rigidbody2D myRigidbody;

    private bool isPlayerMoving;
    public Vector2 lastMove;

    private static bool doesPlayerExist;

    private bool attacking;
    public float attackTime;
    private float attackTimeCounter;

    public string startPoint;

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

        if(!attacking)
        {
            // These two if statements gather input and set the player to moving
            if (Input.GetAxisRaw("Vertical") > 0.5 || Input.GetAxisRaw("Vertical") < -0.5)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, Input.GetAxisRaw("Vertical") * speed);
                isPlayerMoving = true;
                lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            }
            if (Input.GetAxisRaw("Horizontal") > 0.5 || Input.GetAxisRaw("Horizontal") < -0.5)
            {
                myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, myRigidbody.velocity.y);
                isPlayerMoving = true;
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            }

            // These two if statements gather input (or lack of) and set player to stop
            if (Input.GetAxisRaw("Horizontal") < 0.5 && Input.GetAxisRaw("Horizontal") > -0.5)
            {
                myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
            }
            if (Input.GetAxisRaw("Vertical") < 0.5 && Input.GetAxisRaw("Vertical") > -0.5)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0f);
            }

            // Gather input for attack button
            if (Input.GetKeyDown(KeyCode.J))
            {
                attackTimeCounter = attackTime;
                attacking = true;
                myRigidbody.velocity = Vector2.zero;
                animate.SetBool("Attack", true);
            }
        }

        if(attackTimeCounter > 0)
        {
            attackTimeCounter -= Time.deltaTime;
        }

        if(attackTimeCounter <= 0)
        {
            attacking = false;
            animate.SetBool("Attack", false);
        }


        animate.SetFloat("MovementY", Input.GetAxisRaw("Vertical"));
        animate.SetFloat("MovementX", Input.GetAxisRaw("Horizontal"));
        animate.SetBool("IsPlayerMoving", isPlayerMoving);
        animate.SetFloat("LastMoveX", lastMove.x);
        animate.SetFloat("LastMoveY", lastMove.y);
    }
}
