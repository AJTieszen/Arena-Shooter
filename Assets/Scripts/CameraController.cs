using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float cameraSpeed;
    public bool fillHeight;

    private uint player;

    private GameInput gameInput;
    // Start is called before the first frame update
    void Start()
    {
        player = target.GetComponent<PlayerController>().playerId;
        Camera camera = GetComponent<Camera>();
        gameInput = new GameInput(player);

        float x = 0.5f * (player % 2);
        float y = 0;
        float w = 0.5f;
        float h = 1;
        if (!fillHeight)
        {
            h = 0.5f;
            y = 0.5f - 0.5f * (int)(player / 2);
        }

        camera.rect = new Rect(x, y, w, h);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = gameInput.getCameraMovement();

        rotate(movement);
    }

    void rotate(Vector2 movement)
    {
        Transform tgt = target.transform;
        float speed = cameraSpeed * Time.deltaTime;
        float x = transform.eulerAngles.x;
        bool allowRotation = true;

        transform.RotateAround(tgt.position, Vector3.up, movement.x * speed);

        if (x > 89 && x < 180 && movement.y < 0) 
            allowRotation = false;
        if (x > 180 && x < 271 && movement.y > 0)
            allowRotation = false;
        if (allowRotation)
            transform.RotateAround(tgt.position, transform.right, movement.y * -speed);
    }
}
