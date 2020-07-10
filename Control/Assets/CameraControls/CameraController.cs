using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Vector3 cameraVelocity;
    Camera mainCamera;
    Vector3 targetPosition;
    Vector3 height;
    Vector3 cameraOffset;
    Transform middleMouseHit;
    public Transform playerTransform;
    [Range(20, 200)]
    public int scrollSpeed = 50;
    public float rotationSpeed = 0f;

    [Range(0.1f, 1.0f)]
    public float dampingIntensity = 0.5f;
    private Vector3 GetWorldPosAtViewportPoint(float vx, float vy)
    {
        Ray worldRay = mainCamera.ViewportPointToRay(new Vector3(vx, vy, 0));
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float distanceToGround;
        groundPlane.Raycast(worldRay, out distanceToGround);
        Debug.Log("distance to ground:" + distanceToGround);
        return worldRay.GetPoint(distanceToGround);
    }

    void Start()
    {

        mainCamera = GetComponent<Camera>();

        height = new Vector3(0f, mainCamera.transform.position.y, 0f);
        cameraOffset = mainCamera.transform.position - playerTransform.position;
        mainCamera.transform.position += cameraOffset;
        targetPosition = mainCamera.transform.position;
    }

    void Update()
    {
        RaycastHit hit;

        if (Input.GetMouseButtonDown(2))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                middleMouseHit = hit.transform;
            }
        }

        if (Input.GetMouseButtonUp(2))
        {
            // Center whatever position is clicked
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                targetPosition = middleMouseHit.position + height + cameraOffset;
                // mainCamera.transform.LookAt(Vector3.SmoothDamp(mainCamera.transform.position, hit.point, ref cameraVelocity, dampingIntensity));
            }
        }
        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {

            targetPosition = playerTransform.position + height + cameraOffset;
            // mainCamera.transform.LookAt(playerTransform);
        }
        float scrollWheelChange = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelChange != 0)
        {

            targetPosition = mainCamera.transform.position + mainCamera.transform.forward * scrollWheelChange * scrollSpeed;

            Debug.Log(scrollWheelChange);
            // mainCamera.transform.position = mainCamera.transform.forward * scrollWheelChange; 
        }
    }

    void LateUpdate()
    {

        if (Input.GetMouseButton(2))
        {

            // targetPosition = mainCamera.transform.position + cameraAngle;
            // mainCamera.transform.LookAt(middleMouseHit.position);
            // mainCamera.transform.Rotate(new Vector3(0, -Input.GetAxis("Mouse X"), 0), Space.World);
            // mainCamera.transform.LookAt(hit.transform.position);

            // targetPosition = mainCamera.transform.position + new Vector3(-Input.GetAxis("Mouse X") * 100 * Time.deltaTime, 0, 0);
            mainCamera.transform.RotateAround(middleMouseHit.transform.position, Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed);
            targetPosition = mainCamera.transform.position;
        }
        else
        {
            // Move the camera smoothly to the target position
            mainCamera.transform.position = Vector3.SmoothDamp(
                mainCamera.transform.position, targetPosition, ref cameraVelocity, dampingIntensity);
        }
    }
}
