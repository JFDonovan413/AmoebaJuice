using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    KeyCode leftMouse = KeyCode.Mouse0, rightMouse = KeyCode.Mouse1, middleMouse = KeyCode.Mouse2;

    public float camheight = 1.75f, camMaxDist = 25f;
    private float camMaxTilt = 90;

    [Range(0, 4)]
    public float camSpeed = 2.0f;

    private float currPan, currTilt = 10f, currDist = 5f;

    PlayerMovement player;
    public Transform tilt;
    Camera mainCam;

    void Start()
    {

        player = FindObjectOfType<PlayerMovement>();
        // Getting the player movement script and main camera in the scene
        mainCam = Camera.main;

        // Setting the position to the current player position plus the height the camera should be at and the rotation to the player's current rotation
        transform.position = player.transform.position + Vector3.up * camheight;
        transform.rotation = player.transform.rotation;

        // Setting the tilt angle to the current tilt for x angle and the current camera angles for y and z
        tilt.eulerAngles = new Vector3(currTilt, transform.eulerAngles.y, transform.eulerAngles.z);
        mainCam.transform.position += tilt.forward * -currDist;
    }

    void Update() { }

    void LateUpdate()
    {

        CamTransforms();
    }
    void CamTransforms()
    {
        currPan = player.transform.eulerAngles.y;

        transform.position = player.transform.position + Vector3.up * camheight;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, currPan, transform.eulerAngles.z);
        tilt.eulerAngles = new Vector3(currTilt, tilt.eulerAngles.y, tilt.eulerAngles.z);
        mainCam.transform.position = transform.position + tilt.forward * -currDist;
    }
}
