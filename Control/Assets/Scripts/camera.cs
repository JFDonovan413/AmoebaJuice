using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    Vector3 offset;
    public Transform player;

    public float camPosX;
    public float camPosY;
    public float camPosZ;

    public float camRotationX;
    public float camRotationY;
    public float camRotationZ;

    public float camMoveSpeed = 10;
    public float scrollSpeed = 10f;
    private bool leftClick;
    private bool playermoved;
    Vector3 lastPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        lastPlayerPos = player.transform.position;
        camPosX = player.position.x;
        camPosY = player.position.y + 12;
        camPosZ = player.position.z - 12;

        offset = new Vector3(player.position.x + camPosX, player.position.y + camPosY, player.position.z + camPosZ);
        // transform.rotation = Quaternion.Euler(camRotationX, camRotationY, camRotationZ);
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }

    void Update()
    {

        leftClick = Input.GetMouseButton(0);

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


    // Update is called once per frame
    void LateUpdate()
    {

        Vector3 camPos;
        if (leftClick)
        {
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * camMoveSpeed * camPosZ, Vector3.up) * Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * camMoveSpeed * camPosY, Vector3.right) * offset;

            camPos = player.position + offset;
            if (camPos.y < 2)
                camPos.y = 2;
            transform.position = camPos;
        }
        if (playermoved)
        {
            Debug.Log(offset);
            camPos = player.position + offset;
            if (camPos.y < 2)
                camPos.y = 2;
            transform.position = camPos;
        }

        transform.LookAt(player.position);
        lastPlayerPos = player.position;
    }
}
