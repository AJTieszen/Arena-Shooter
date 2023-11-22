using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Character Settings
    public float speed;
    public float gravity;
    public float terminalVelocity;
    public float jumpVelocity;

    // Character status
    private float vSpeed = 0;
    private Vector3 velocity = Vector3.zero;
    private Vector3 startPosition;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        startPosition = controller.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Reset velocity
        velocity = Vector3.zero;

        // Apply Gravity
        if (controller.isGrounded) vSpeed = 0;
        else if (vSpeed > terminalVelocity * -1) vSpeed -= gravity * Time.deltaTime;

        // Control movement
        if (Input.GetKey(KeyCode.W)) velocity += Vector3.forward * speed;
        if (Input.GetKey(KeyCode.A)) velocity += Vector3.left * speed;
        if (Input.GetKey(KeyCode.S)) velocity += Vector3.back * speed;
        if (Input.GetKey(KeyCode.D)) velocity += Vector3.right * speed;
        if (Input.GetKey(KeyCode.Space) && controller.isGrounded) vSpeed = jumpVelocity;

        // Respawn
        if (Input.GetKeyDown(KeyCode.R))
        {
            controller.transform.position = startPosition;
            vSpeed = 0;
            Physics.SyncTransforms();
        }

        // Apply velocity
        velocity.y = vSpeed;
        controller.Move(velocity * Time.deltaTime);
    }
}
