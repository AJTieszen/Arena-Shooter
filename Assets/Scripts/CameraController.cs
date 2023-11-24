using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float cameraSpeed;


    public uint player;
    public bool fillHeight;

    private GameInput gameInput;
    // Start is called before the first frame update
    void Start()
    {
        gameInput = new GameInput(player);
        Camera camera = GetComponent<Camera>();

        float x = 0.5f * (player % 2);
        float y = 0;
        float w = 0.5f;
        float h = 1;

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
        transform.RotateAround(tgt.position, Vector3.up, movement.x * speed);
        transform.RotateAround(tgt.position, transform.right, movement.y * -speed);
    }
}
