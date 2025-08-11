using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator playerAnim;

    [SerializeField] private float walkSpeed = 15;
    [SerializeField] private float runSpeed = 30;
    [SerializeField] private float mouseSensitivity = 5f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController my_controller;
    private Vector3 moveDirection;
    private float rotationY;
    private bool isRunning;
    private float verticalVelocity;

    private void Awake()
    {
        my_controller = GetComponent<CharacterController>();
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
        HandleAnimations();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;
        isRunning = Input.GetKey(KeyCode.LeftShift) && inputDirection.magnitude > 0.1f;
        Vector3 move = transform.rotation * inputDirection * (isRunning ? runSpeed : walkSpeed);

        if (my_controller.isGrounded)
        {
            if (verticalVelocity < 0)
            {
                verticalVelocity = -2f;
            }
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        moveDirection = new Vector3(move.x, verticalVelocity, move.z);

        my_controller.Move(moveDirection * Time.deltaTime);
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        rotationY += mouseX * mouseSensitivity;
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }

    private void HandleAnimations()
    {
        bool isMoving = new Vector3(moveDirection.x, 0, moveDirection.z).magnitude > 0.1f;

        playerAnim.SetBool("Walking", isMoving);
        playerAnim.SetBool("Idle", !isMoving);
        playerAnim.SetBool("Running", isRunning && isMoving);
    }
}
