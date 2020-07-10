using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // CharacterController characterController;
    public float speed = 0.0f;

    // private Vector3 movement = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 v = new Vector3( horizontal, 0.0f, vertical) * speed * Time.deltaTime;
        // transform.LookAt(v);
        // transform.Translate(v);

        transform.position = transform.position + v;
        transform.LookAt(transform.position + v);
    }
}
