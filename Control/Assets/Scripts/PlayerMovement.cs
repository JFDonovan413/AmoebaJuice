using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 150f, moveSpeed = 15, mouseTurnSpeed = 5f;
    private float rotation;
    CharacterController controller;
    private float x;
    private float z;
    public CameraController mainCam;
    public bool steer;
    private bool rightCLick;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {

        // Getting movement inputs
        this.getKeys();


    }

    void LateUpdate()
    {
        //Setting rotation based on input to rotation
        Vector3 charRotation = transform.eulerAngles + new Vector3(0, rotation, 0);
        transform.eulerAngles = charRotation;

        // Setting xy movement based on xy inputs and movement speed
        Vector3 velocity = (transform.forward * z + transform.right * x) * Time.deltaTime * moveSpeed;
        controller.Move(velocity);
    }

    void getKeys()
    {

        x = getAxis(Input.GetKey(KeyCode.E), Input.GetKey(KeyCode.Q)); ;
        z = Input.GetAxis("Vertical");

        rightCLick = Input.GetMouseButton(0);

        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            x += getAxis(Input.GetKey(KeyCode.E), Input.GetKey(KeyCode.Q));
            Mathf.Clamp(x, -1, 1);
        }


        if (Input.GetAxis("Horizontal") != 0)
        {
            rotation = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        }
    }

    float getAxis(bool positive, bool negative)
    {
        float axis = 0;

        if (positive)
            axis += 1;
        if (negative)
            axis -= 1;
        return axis;
    }
}
