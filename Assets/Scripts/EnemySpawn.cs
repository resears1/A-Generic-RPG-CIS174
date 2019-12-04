using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public GameObject SpawnObject;

    public Vector3 randomSpawn;


    public float timeBeforeSpawn;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = timeBeforeSpawn;
        randomSpawn = new Vector3(transform.position.x + Random.Range(0, 4), transform.position.y + Random.Range(0,4), 1);
        GameObject slime = GameObject.Instantiate(SpawnObject, randomSpawn, Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < 30)
            {
                randomSpawn = new Vector3(transform.position.x + Random.Range(0, 4), transform.position.y + Random.Range(0, 4), 1);
                GameObject slime = GameObject.Instantiate(SpawnObject, randomSpawn, Quaternion.identity) as GameObject;
                timer = timeBeforeSpawn;
            }
        }
    }
}
