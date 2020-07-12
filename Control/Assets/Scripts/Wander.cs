using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public float movementSpeed = 10f;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        target = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));

    }

    // Update is called once per frame
    void LateUpdate()
    {

        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);

        if (transform.position == target)
        {
            target = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
        }
    }
}
