using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public GameObject SpawnObject;

    public Vector3 randomSpawn;

    // Start is called before the first frame update
    void Start()
    {
        randomSpawn = new Vector3(transform.position.x -2, transform.position.y + 1, 1);
        GameObject slime = GameObject.Instantiate(SpawnObject, randomSpawn, Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
