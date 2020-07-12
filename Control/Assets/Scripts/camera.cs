using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    Vector3 offset;
    Vector3 cameraVelocity;
    public Transform player;

    public float camPosX;
    public float camPosY;
    public float camPosZ;

    public float camRotationX;
    public float camRotationY;
    public float camRotationZ;

    public float camMoveSpeed = .5f;
    public float scrollSpeed = 10f;
    private bool leftClick;
    private bool rightCLick;

    private bool playermoved;
    Vector3 lastPlayerPos;

    void Start()
    {
        lastPlayerPos = player.transform.position;
        camPosX = player.position.x;
        camPosY = player.position.y + 12;
        camPosZ = player.position.z - 12;

        offset = new Vector3(player.position.x + camPosX, player.position.y + camPosY, player.position.z + camPosZ);

        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }

    void Update()
    {

        leftClick = Input.GetMouseButton(0);
        rightCLick = Input.GetMouseButton(1);

        float scrollWheelChange = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelChange != 0)
        {

            Vector3 camPos = transform.position + transform.forward * scrollWheelChange * scrollSpeed;
            if (camPos.y < 2)
                camPos.y = 2;
            transform.position = camPos;
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Q))
        {
            playermoved = true;
        }

    }

    void LateUpdate()
    {

        Vector3 camPos;

        if (playermoved)
        {
            Debug.Log(offset);
            camPos = player.position + offset;
            if (camPos.y < 2)
                camPos.y = 2;
            transform.position = camPos;
        }

        if (leftClick)
        {
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * camMoveSpeed * camPosZ, Vector3.up) * Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * camMoveSpeed * camPosY, Vector3.right) * offset;

            camPos = player.position + offset;
            if (camPos.y < 2)
                camPos.y = 2;
            transform.position = camPos;
        }

        else if (rightCLick)
        {
            offset = Quaternion.AngleAxis(-Input.GetAxis("Mouse X") * camMoveSpeed * camPosZ, Vector3.up) * Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * camMoveSpeed * camPosY, Vector3.right) * offset;

            camPos = player.position + offset;
            if (camPos.y < 2)
                camPos.y = 2;
            transform.position = camPos;
            Debug.Log(player.transform.eulerAngles);
            // player.LookAt(new Vector3(player.forward.x, -camPos.y, player.forward.z).normalized);
            player.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            Debug.Log(player.transform.eulerAngles);

            // player.transform.eulerAngles = Vector3.SmoothDamp(player.transform.eulerAngles, new Vector3(0, transform.eulerAngles.y, 0), ref cameraVelocity, .2f);
        }

        transform.LookAt(player.position);
    }

    float PlusMinus180(float angle, float target)
    {
        while (angle > target + 180f)
        {
            angle -= 360f;
        }
        while (angle <= target - 180f)
        {
            angle += 360f;
        }
        return angle;
    }
}
