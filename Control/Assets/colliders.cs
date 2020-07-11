using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliders : MonoBehaviour
{
    CharacterController controller;
    BoxCollider thisBox;
    public GameObject toMake;
    private float timeLastHit = 0;
    // Start is called before the first frame update
    void Start()
    {
        // Finding the current box
        BoxCollider[] boxes = FindObjectsOfType<BoxCollider>();
        foreach (BoxCollider box in FindObjectsOfType<BoxCollider>())
        {
            if (box.gameObject.name == this.gameObject.name)
            {
                thisBox = box;
            }
        }

        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    private string hitobject;

    void OnTriggerEnter(Collider hit)
    {
        hitobject = hit.gameObject.name;
        if (hitobject == "Cylinder")
        {
            // transform.position = transform.position - transform.forward * 120;

            // transform.Translate(-transform.forward.x, transform.forward.y, 100 * Time.deltaTime);
            controller.Move(-transform.forward * 10);
            spawnObj();
        }
        // GameObject newBox = Instantiate(toMake);
        // transform.Move(-transform.forward * 30 * Time.deltaTime);

    }

    void spawnObj()
    {
        Debug.Log(Time.time - timeLastHit);

        if (Time.time - timeLastHit > .5)
        {
            Debug.Log("Been at least .5 seconds");
            GameObject newBox = Instantiate(toMake, new Vector3(Random.Range(-25,25), 0, Random.Range(-25,25)) + transform.position, transform.rotation);
            // newBox.transform.position = transform.position ;
            thisBox.enabled = true;
        }
        timeLastHit = Time.time;
        Debug.Log(timeLastHit);
    }
}
