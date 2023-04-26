using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// These videos take long to make so I hope this helps you out and if you want to help me out you can by leaving a like and subscribe, thanks!

public class PlayerMovement : MonoBehaviour
{
    Transform playerCamera;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] bool cursorLock = true;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float moveSpeed = 6.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    public float gravity = -30f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    public float jumpHeight = 6f;
    float velocityY;
    bool isGrounded;

    float cameraCap;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;

    Vector2 currentDir;
    Vector2 currentDirVelocity;

    CharacterController controller;
    Recorder recorder;
    Cloning cloningScript;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        recorder = GetComponent<Recorder>();
        cloningScript = GetComponent<Cloning>();

        if (cloningScript.isClone)
        {
            playerCamera = cloningScript.startingCamera.transform;
        }
        else
        {
            // Find Main Camera
            for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
            {
                GameObject child = gameObject.transform.GetChild(i).gameObject;
                if (child.CompareTag("MainCamera"))
                {
                    playerCamera = child.transform;
                }
            }
        }

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        UpdateMouse();
        UpdateMove();
    }

    void UpdateMouse()
    {
        if (!cloningScript.isClone)
        {
            Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

            cameraCap -= currentMouseDelta.y * mouseSensitivity;

            cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);

            playerCamera.localEulerAngles = Vector3.right * cameraCap;

            var rotationValue = Vector3.up * currentMouseDelta.x * mouseSensitivity;
            transform.Rotate(rotationValue);

            if (recorder.isRecording)
            {
                recorder.Push(Recorder.ActionType.MoveCamera, Time.time, Vector3.up * currentMouseDelta.x * mouseSensitivity);
            }
        }
    }

    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);

        Vector2 targetDir;
        if (!cloningScript.isClone)
        {

            targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            targetDir.Normalize();

            currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

            velocityY += gravity * 2f * Time.deltaTime;

            Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * moveSpeed + Vector3.up * velocityY;

            controller.Move(velocity * Time.deltaTime);

            if (recorder.isRecording)
            {
                recorder.Push(Recorder.ActionType.Move, Time.time, velocity * Time.deltaTime);
            }

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                velocityY = Jump(jumpHeight, gravity);

                if (recorder.isRecording)
                {
                    recorder.Push(Recorder.ActionType.Jump, Time.time);
                }
            }
        }

        if (isGrounded! && controller.velocity.y < -1f)
        {
            velocityY = -8f;
        }
    }

    public float Jump(float jumpHeight, float gravity)
    {
        float velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);

        return velocityY;
    }
    public void SetVelocityY(float newValue)
    {
        velocityY = newValue;
    }
}