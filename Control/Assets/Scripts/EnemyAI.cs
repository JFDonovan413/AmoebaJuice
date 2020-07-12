using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    GameObject player;
    Vector3 target;
    float movementSpeed = 8f;
    float lastHit;
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Cube");
    }

    // Update is called once per frame
    void Update()
    {
        if (target == transform.position)
        {
            lastHit = Time.time;
            target = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
        }
        else if (Time.time - lastHit > 2)
            target = player.transform.position;

        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);

    }
}
