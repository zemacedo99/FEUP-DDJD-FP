using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerCamera;
    [SerializeField] bool cursorLock = true;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] float mouseSensitivity;
    [SerializeField] float moveSpeed = 6.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    public float gravity = -30f;
    [SerializeField] Transform groundCheck; 
    [SerializeField] LayerMask ground;

    public float jumpHeight = 6f;
    float velocityY;
    bool isGrounded;

    [SerializeField] float cameraCap;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;

    Vector2 targetDir;
    Vector2 targetDirVelocity;

    CharacterController controller;
    Cloning cloningScript;
    Recorder recorder;

    Vector2 cameraInputValue;
    Vector2 moveInputValue;

    float cameraRotY;
    float cameraRotX;

    public InputActionAsset actions;
    public InputAction cameraInput, jumpButton, moveInput;

    void Start()
    {
        Application.targetFrameRate = 200;

        cameraInput = actions.FindActionMap("movement", true).FindAction("camera", true);
        jumpButton = actions.FindActionMap("movement", true).FindAction("jump", true);
        moveInput = actions.FindActionMap("movement", true).FindAction("move", true);
        actions.FindActionMap("movement").Enable();

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
        cameraInputValue = cameraInput.ReadValue<Vector2>();

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, cameraInputValue, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraRotY += cameraInputValue.y * mouseSensitivity;
        cameraRotX += cameraInputValue.x * mouseSensitivity;
        cameraRotX = Mathf.Clamp(cameraRotX, -cameraCap, cameraCap);

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
        float cappedRotY = Mathf.Clamp(currentMouseDelta.y * mouseSensitivity, -cameraCap, cameraCap);
        playerCamera.Rotate(-Vector3.right * cappedRotY);
    }

    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.35f, ground);

        Vector2 newMoveInputValue;

        newMoveInputValue = moveInput.ReadValue<Vector2>();
        newMoveInputValue.Normalize();

        moveInputValue = newMoveInputValue;

        targetDir = Vector2.SmoothDamp(targetDir, moveInputValue, ref targetDirVelocity, moveSmoothTime);
        if (!isGrounded)
            velocityY += gravity * 2f * Time.deltaTime;
        Vector3 velocity = (transform.forward * targetDir.y + transform.right * targetDir.x) * moveSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);

        // JUMP
        if (isGrounded)
        {
            if (jumpButton.WasPressedThisFrame())
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