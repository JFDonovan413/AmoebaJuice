using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // If right click, set that point as destination for nav mesh agent
        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("Clicked");
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {

                agent.stoppingDistance = 1f;
                agent.destination = hit.point;
            }
        }

        // Resetting path if we're "close enough" to position chosen
        if( Vector3.Distance( agent.transform.position, agent.destination) <= 1 ){

            agent.ResetPath();
        }
    }
}
