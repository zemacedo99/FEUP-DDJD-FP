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
    public InputAction cameraInput, jumpButton, moveInput, gravButton;

    public bool canGravJump;
    bool isGravityInverted;

    public bool canJetpack;
    private Oxygen oxy;
    public float jetCost;
    public GameObject waterParticle;

    void Start()
    {
        Application.targetFrameRate = 200;

        oxy = GetComponent<Oxygen>();
        cameraInput = actions.FindActionMap("movement", true).FindAction("camera", true);
        jumpButton = actions.FindActionMap("movement", true).FindAction("jump", true);
        gravButton = actions.FindActionMap("movement", true).FindAction("gravity", true);
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
        if (isGravityInverted) cameraInputValue.y *= -1;

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, cameraInputValue, ref currentMouseDeltaVelocity, mouseSmoothTime);

        //cameraRotX = Mathf.Clamp(cameraRotX, -cameraCap, cameraCap);

        cameraRotX += currentMouseDelta.x * mouseSensitivity;
        transform.rotation = Quaternion.Euler(Vector3.up * cameraRotX); 

        cameraRotY += currentMouseDelta.y * mouseSensitivity;
        cameraRotY = Mathf.Clamp(cameraRotY, -cameraCap, cameraCap);
        playerCamera.localEulerAngles = (-Vector3.right * cameraRotY);
    }

    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.35f, ground);

        Vector2 newMoveInputValue;

        newMoveInputValue = moveInput.ReadValue<Vector2>();
        newMoveInputValue.Normalize();

        moveInputValue = newMoveInputValue;

        targetDir = Vector2.SmoothDamp(targetDir, moveInputValue, ref targetDirVelocity, moveSmoothTime);

        velocityY += gravity * 2f * Time.deltaTime;
        Vector3 velocity = (transform.forward * targetDir.y + transform.right * targetDir.x) * moveSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);

        // JUMP
        if (jumpButton.WasPressedThisFrame() && (isGrounded || (!isGrounded && canJetpack)))
        {
            int mul = 1;
            if (!isGrounded)
            {
                mul = 3;
                oxy.oxygenValue -= jetCost;
                JetPackParticles();
            }
            velocityY = Jump(jumpHeight * mul);
        }
        // GRAVITY Switch
        if (gravButton.WasPressedThisFrame() && canGravJump && isGrounded)
        {
            gravity *= -1;
            if (isGravityInverted) isGravityInverted = false;
            else isGravityInverted = true;

            //transform.localScale = new Vector3(1, transform.localScale.y * -1, 1);
            transform.Rotate(Vector3.right, 180f);
            velocityY = 0f;
        }
    }

    public float Jump(float height)
    {
        float velocityY = Mathf.Sqrt(height * -2f * gravity);

        return velocityY;
    }

    private void JetPackParticles()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(waterParticle, this.transform.position, Quaternion.identity);
        }
    }
}