using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 50f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal")*5;
        float vertical = Input.GetAxis("Vertical")*5;

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.Self);

        if (Input.GetMouseButton(1)) 
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            transform.Rotate(Vector3.up, mouseX * turnSpeed * Time.deltaTime*20, Space.World);
            transform.Rotate(Vector3.right, -mouseY * turnSpeed * Time.deltaTime*20, Space.Self);
        }
    }
}

