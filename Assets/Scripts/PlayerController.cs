using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Animator animate;

    private bool isPlayerMoving;
    private Vector2 lastMove;

    void Start()
    {
        animate = GetComponent<Animator>();
    }

    void Update()
    {
        isPlayerMoving = false;

        if (Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0)
        {
            transform.Translate(new Vector2(0f, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime));
            isPlayerMoving = true;
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }

        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.Translate (new Vector2(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0f));
            isPlayerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }

        animate.SetFloat("MovementY", Input.GetAxisRaw("Vertical"));
        animate.SetFloat("MovementX", Input.GetAxisRaw("Horizontal"));
        animate.SetBool("IsPlayerMoving", isPlayerMoving);
        animate.SetFloat("LastMoveX", lastMove.x);
        animate.SetFloat("LastMoveY", lastMove.y);
    }
}
