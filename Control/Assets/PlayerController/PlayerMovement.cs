using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 4f, moveSpeed = 80f, mouseTurnMultiplier = 1f;
    private float rotation;
    CharacterController controller;
    private float x;
    private float z;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        // Getting movement inputs
        this.getKeys();

        //Setting rotation based on input to rotation
        Vector3 charRotation = transform.eulerAngles + new Vector3(0, rotation * turnSpeed, 0);
        transform.eulerAngles = charRotation;

        // Setting xy movement based on xy inputs and movement speed
        Vector3 velocity = (transform.forward * z + transform.right * x) * Time.deltaTime * moveSpeed;
        controller.Move(velocity);
    }

    void getKeys()
    {

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        // Using Q and E keys to rotate
        if (Input.GetKey(KeyCode.Q))
        {
            rotation = -1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (Input.GetKey(KeyCode.Q))
            {
                rotation = 0;
            }
            else
            {
                rotation = 1;
            }
        }
        if(!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E)){
            rotation = 0;
        }
    }
}
