using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    Transform cameraTransform;
    float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 forward = Vector3.zero;
        Vector3 horizontal = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            forward += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            forward -= Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            forward += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            forward -= Vector3.left;
        }
        Vector3 direction = (forward + horizontal).normalized;
        cameraTransform.Translate(direction * (speed * Time.deltaTime), Space.World);
    }

}
