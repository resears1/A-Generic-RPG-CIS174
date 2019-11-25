using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPos : MonoBehaviour
{
    private PlayerController player;
    private CameraController cam;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        player.transform.position = transform.position;

        cam = FindObjectOfType<CameraController>();
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
    }
}
