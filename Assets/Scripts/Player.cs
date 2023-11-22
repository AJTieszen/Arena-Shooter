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
        float forward = 0;
        float strafe = 0;

        // Control movement
        if (Input.GetKey(KeyCode.W)) forward = speed;
        if (Input.GetKey(KeyCode.A)) strafe = speed * -1;
        if (Input.GetKey(KeyCode.S)) forward = speed * -1;
        if (Input.GetKey(KeyCode.D)) strafe = speed;
        
        // Jump
        if (Input.GetKey(KeyCode.Space))
            jump();

        // Respawn
        if (Input.GetKeyDown(KeyCode.R))
            respawn();

        // Apply velocity
        move(forward, strafe);
    }

    void respawn()
    {
        controller.transform.position = startPosition;
        vSpeed = 0;
        Physics.SyncTransforms();
    }

    void jump()
    {
        if (controller.isGrounded)
        {
            vSpeed = jumpVelocity;
        }
    }

    void move (float forward, float strafe)
    {
        // Movement
        velocity = Vector3.zero;

        velocity += Vector3.forward * forward;
        velocity += Vector3.right * strafe;

        // Gravity
        if (controller.isGrounded && vSpeed < 0) vSpeed = 0;
        else if (vSpeed > terminalVelocity * -1) vSpeed -= gravity * Time.deltaTime;

        // move character
        velocity.y = vSpeed;
        controller.Move(velocity * Time.deltaTime);
    }
}
