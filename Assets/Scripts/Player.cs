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
    public uint playerId = 0;

    // Character status
    private float vSpeed = 0;
    private Vector3 velocity = Vector3.zero;
    private Vector3 startPosition;

    private CharacterController controller;
    private GameInput gameInput;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gameInput = new GameInput(playerId);
        startPosition = controller.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GameInput.update();

        Vector2 movement = gameInput.getMovement();
        if (gameInput.isJumpPressed())
            jump();

        if (gameInput.isMenuPressed())
            Application.Quit();
        if (Input.GetKeyDown(KeyCode.R))
            respawn();

        move(movement);
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
    void move (Vector2 direction)
    {
        // Movement
        velocity = Vector3.zero;

        velocity.z += direction.y * speed;
        velocity.x += direction.x * speed;

        // Gravity
        if (controller.isGrounded && vSpeed < 0) vSpeed = 0;
        else if (vSpeed > terminalVelocity * -1) vSpeed -= gravity * Time.deltaTime;

        // move character
        velocity.y = vSpeed;
        controller.Move(velocity * Time.deltaTime);
    }
}
