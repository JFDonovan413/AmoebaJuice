using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    KeyCode leftMouse = KeyCode.Mouse0, rightMouse = KeyCode.Mouse1, middleMouse = KeyCode.Mouse2;

    public float camheight = 2.5f, camMaxDist = 25f, scrollSpeed = 6f;
    private float camMaxTilt = 90;

    [Range(0, 4)]
    public float camSpeed = 2.0f;

    private float currPan, currTilt = 10f, currDist = 5f;

    PlayerMovement player;
    public Transform tilt;
    Camera mainCam;
    public CamState cameraState = CamState.camNone;

    void Start()
    {

        player = FindObjectOfType<PlayerMovement>();
        // Getting the player movement script and main camera in the scene
        player.mainCam = this;
        mainCam = Camera.main;

        // Setting the position to the current player position plus the height the camera should be at and the rotation to the player's current rotation
        transform.position = player.transform.position + Vector3.up * camheight;
        transform.rotation = player.transform.rotation;

        // Setting the tilt angle to the current tilt for x angle and the current camera angles for y and z
        tilt.eulerAngles = new Vector3(currTilt, transform.eulerAngles.y, transform.eulerAngles.z);
        mainCam.transform.position += tilt.forward * -currDist;
    }

    void Update()
    {

        if (!Input.GetKey(leftMouse) && !Input.GetKey(middleMouse) && !Input.GetKey(rightMouse)) //No mouse buttns
        {
            cameraState = CamState.camNone;
        }
        else if (Input.GetKey(leftMouse) && !Input.GetKey(middleMouse) && !Input.GetKey(rightMouse)) //Left mouse
        {
            cameraState = CamState.camRotate;
        }
        else if (!Input.GetKey(leftMouse) && !Input.GetKey(middleMouse) && Input.GetKey(rightMouse)) //Right mouse
        {
            cameraState = CamState.camSteer;
        }
        caminputs();
    }

    void LateUpdate()
    {

        CamTransforms();
    }

    void caminputs()
    {
        if (cameraState != CamState.camNone)
        {
            if (cameraState == CamState.camRotate)
            {
                if (player.steer)
                    player.steer = false;

                currPan += Input.GetAxis("Mouse X") * camSpeed;
            }
            else if (cameraState == CamState.camSteer || cameraState == CamState.camFree)
                player.steer = true;
            currTilt += Input.GetAxis("Mouse Y") * camSpeed;
            currTilt = Mathf.Clamp(currTilt, 0, camMaxTilt);
        }
        else
        {
            if (player.steer)
                player.steer = false;
        }

        currDist -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        currDist = Mathf.Clamp(currDist, 0, camMaxDist);
    }
    void CamTransforms()
    {
        switch (cameraState)
        {
            case CamState.camNone:
            case CamState.camFree:
            case CamState.camSteer:

                // currPan = currPan - player.transform.eulerAngles.y * camSpeed * Time.deltaTime;
                currPan = Mathf.Lerp(currPan, Mathf.Abs(player.transform.eulerAngles.y), .2f);
                Debug.Log(currPan);
                break;
        }

        if (cameraState == CamState.camNone)
            Mathf.Lerp(currTilt, 10, camSpeed);

        transform.position = player.transform.position + Vector3.up * camheight;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, currPan, transform.eulerAngles.z);
        tilt.eulerAngles = new Vector3(currTilt, tilt.eulerAngles.y, tilt.eulerAngles.z);
        mainCam.transform.position = transform.position + tilt.forward * -currDist;
    }

    public enum CamState { camNone, camRotate, camSteer, camFree }

}
