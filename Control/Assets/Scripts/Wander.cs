using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public float movementSpeed = 6f;
    public Material[] mats;
    public Material theNewMaterial;
    private GameObject player;
    Vector3 target;
    float birthday;
    float initialSpeed;
    // Start is called before the first frame update
    void Start()
    {
        birthday = Time.time;
        target = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
        initialSpeed = movementSpeed;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        float age = Time.time - birthday;
        player = GameObject.Find("Cube");


        // Juices will gravitated toward player if they are close
        if (Vector3.Distance(transform.position, player.transform.position) < 10 && age > 2)
        {

            target = player.transform.position;
        }

        transform.LookAt(target);
        // For the first 2 seconds, speed will be increased to place space between juice and player
        if (age > 0 && age < 2)
        {
            movementSpeed = initialSpeed + 1;
        }
        else
        {
            movementSpeed = initialSpeed;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);

        if (transform.position == target)
        {
            target = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
        }


    }
}
