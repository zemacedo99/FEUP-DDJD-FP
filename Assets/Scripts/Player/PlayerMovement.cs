using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerCamera;
    [SerializeField] bool cursorLock = true;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] float mouseSensitivity = 100;
    [SerializeField] float moveSpeed = 6.0f;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    public float gravity = -30f;
    [SerializeField] Transform groundCheck; 
    [SerializeField] LayerMask ground;

    public float jumpHeight = 6f;
    float velocityY;
    bool isGrounded;

    [SerializeField] float cameraCap;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;

    Vector2 currentDir;
    Vector2 currentDirVelocity;

    CharacterController controller;
    Cloning cloningScript;
    Recorder recorder;

    Vector2 cameraInputValue;
    Vector2 moveInputValue;

    float cameraRotY;
    float cameraRotX;

    void Start()
    {
        Application.targetFrameRate = 200;

        controller = GetComponent<CharacterController>();
        recorder = GetComponent<Recorder>();
        cloningScript = GetComponent<Cloning>();

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }

        moveInputValue = new();

        Vector3 rot = transform.rotation.eulerAngles;
        cameraRotY = rot.y;
        cameraRotX = rot.x;
    }

    void Update()
    {
        UpdateMouse();
        UpdateMove();
    }

    void UpdateMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");
        cameraInputValue = new Vector2(mouseX, mouseY);

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, cameraInputValue, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraRotY += mouseX * mouseSensitivity;
        cameraRotX += mouseY * mouseSensitivity;
        cameraRotX = Mathf.Clamp(cameraRotX, -cameraCap, cameraCap);

        // Don't rotate player pitch
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
        float cappedRotX = Mathf.Clamp(currentMouseDelta.y * mouseSensitivity, -cameraCap, cameraCap);
        playerCamera.Rotate(Vector3.right * cappedRotX);
    }

    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.35f, ground);

        Vector2 newMoveInputValue;

        newMoveInputValue = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        newMoveInputValue.Normalize();

        moveInputValue = newMoveInputValue;

        currentDir = Vector2.SmoothDamp(currentDir, moveInputValue, ref currentDirVelocity, moveSmoothTime);
        if (!isGrounded)
            velocityY += gravity * 2f * Time.deltaTime;
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * moveSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);

        // JUMP
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocityY = Jump();
            }

        }
    }

    public float Jump()
    {
        float velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);

        return velocityY;
    }

}