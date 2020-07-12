using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliders : MonoBehaviour
{

    public GameObject toMake;
    private float juiceSpawnTime = 0;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private GameObject hitobject;

    void OnTriggerEnter(Collider hit)
    {
        hitobject = hit.gameObject;

        if (hitobject.name == "Cylinder")
        {

            float shrinkPercent = Random.Range(0.2f, 0.3f);

            transform.localScale -= transform.localScale * shrinkPercent;

            spawnObj(transform.localScale * (shrinkPercent + .2f));

        }
        else if (hitobject.name == "Sphere")
        {
            if (Time.time - juiceSpawnTime > 4)
            {
                transform.localScale += hitobject.transform.localScale;
                GameObject.Destroy(hitobject);
            }
        }
    }
    void spawnObj(Vector3 newObjectAmount)
    {
        juiceSpawnTime = Time.time;

        GameObject newBox = Instantiate(toMake, new Vector3(transform.position.x + Random.Range(-1, 1), 0, transform.position.z + Random.Range(-1, 1)), transform.rotation);
        newBox.transform.localScale = newObjectAmount;
    }
}
