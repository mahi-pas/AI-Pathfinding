using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private const float SPEED = 1f, SCALE = 2;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-SPEED * cam.orthographicSize * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(SPEED * cam.orthographicSize * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -SPEED * cam.orthographicSize * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, SPEED * cam.orthographicSize * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0))
        {
            cam.orthographicSize -= SCALE;
        }
        if (Input.GetMouseButtonDown(1))
        {
            cam.orthographicSize += SCALE;
        }
    }
}
