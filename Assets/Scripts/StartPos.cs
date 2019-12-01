using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPos : MonoBehaviour
{
    private PlayerController player;
    private CameraController cam;

    public Vector2 startDirection;

    public string pointName;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (player.startPoint == pointName)
        {
            player.transform.position = transform.position;
            player.lastMove = startDirection;

            cam = FindObjectOfType<CameraController>();
            cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
        }
    }
}
