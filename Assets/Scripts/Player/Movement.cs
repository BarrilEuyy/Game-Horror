using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float sensitivity = 0.2f;
    public Transform playerBody;
    public Transform camera;
    public CharacterController controller;
    public float speed;

    public Transform groundCheck;
    public float groundDistance;
    public RaycastHit hitGround;

    public float jumpHeight;

    float gravity = -9.81f;
    Vector3 velocity;

    private float xRotation = 0f;
    private bool isDragging = false;
    private bool isGrounded;

    void Update()
    {
        GetTouch();

        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance);


        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float horizontalInput = SimpleInput.GetAxis("Horizontal");
        float verticalInput = SimpleInput.GetAxis("Vertical");

        Vector3 moveDir = transform.forward * verticalInput + transform.right * horizontalInput;

        controller.Move(moveDir * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    void GetTouch()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began && touch.position.x > Screen.width / 2)
                {
                    isDragging = true;
                }
                else if (touch.phase == TouchPhase.Moved && isDragging && touch.position.x > Screen.width / 2)
                {
                    Vector2 delta = touch.deltaPosition * sensitivity;

                    xRotation -= delta.y;
                    xRotation = Mathf.Clamp(xRotation, -90f, 90f);
                    camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

                    playerBody.Rotate(Vector3.up * delta.x);
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    isDragging = false;
                }
            }
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
