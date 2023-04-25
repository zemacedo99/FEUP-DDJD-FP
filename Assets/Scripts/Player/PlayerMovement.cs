using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// These videos take long to make so I hope this helps you out and if you want to help me out you can by leaving a like and subscribe, thanks!

public class PlayerMovement : MonoBehaviour
{
    public Transform playerCamera;
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
    Cloning cloningScript;
    Recorder recorder;

    // Clone Playing
    Vector2 moveInputValue;
    Vector2 cameraInputValue;
    int moveInputValueUpdateIndex;
    int cameraInputValueUpdateIndex;
    int jumpIndex;
    // Clone Recording
    bool hasRecordedMoveInputValueUpdate;
    bool hasRecordedCameraInputValueUpdate;
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        recorder = GetComponent<Recorder>();
        cloningScript = GetComponent<Cloning>();

        if (!cloningScript.isClone) {
            // Find Main Camera
            for (var i = gameObject.transform.childCount - 1; i >= 0; i--)
            {
                GameObject child = gameObject.transform.GetChild(i).gameObject;
                if (child.CompareTag("MainCamera"))
                {
                    playerCamera = child.transform;
                }
            }
            if (cursorLock)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;
            }
        }
        else
        {
            // Instantiate clone camera
            playerCamera = Instantiate(recorder.startingCamera, recorder.startingCamera.transform.localPosition, recorder.startingCamera.transform.localRotation).transform;
            playerCamera.localRotation = recorder.startingCamera.transform.localRotation;
            playerCamera.SetParent(gameObject.transform, false);
            // Initialize cameraCap
            cameraCap = playerCamera.localEulerAngles.x;
            if (cameraCap > 90)
                cameraCap -= 360f;

            // Helps with Debugging
            playerCamera.GetComponent<Camera>().enabled = true;
        }

        moveInputValue = new();
        cameraInputValue = new();
        currentMouseDelta = new();

        // Clone Playing
        moveInputValueUpdateIndex = 0;
        cameraInputValueUpdateIndex = 0;
        jumpIndex = 0;
        // Clone Recording
        hasRecordedMoveInputValueUpdate = false;
        hasRecordedCameraInputValueUpdate = false;
    }

    void Update()
    {
        UpdateMouse();
        UpdateMove();
    }

    void UpdateMouse()
    {
        Vector2 newCameraInputValue;

        if (!cloningScript.isClone)
        {
            newCameraInputValue = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            if (recorder.isRecording && (newCameraInputValue != cameraInputValue || !hasRecordedCameraInputValueUpdate))  // is different OR is the first recording
            {
                recorder.Push(Recorder.EventType.CameraInputValueUpdate, Time.time - recorder.GetRecordingStartTime(), newCameraInputValue);
                hasRecordedCameraInputValueUpdate = true;
            }
            else if (!recorder.isRecording) hasRecordedCameraInputValueUpdate = false;

            cameraInputValue = newCameraInputValue;
        }
        else
        {
            // Get Playback Value
            Tuple<Recorder.EventType, float, Vector3> tuple = cloningScript.recorder.GetEvent(cameraInputValueUpdateIndex);
            if (tuple != null && tuple.Item2 <= Time.time - cloningScript.recorder.GetPlayStartTime())
            {
                if (tuple.Item1 == Recorder.EventType.CameraInputValueUpdate)
                {
                    cameraInputValue = tuple.Item3;
                }
                cameraInputValueUpdateIndex++;
                //tuple = cloningScript.recorder.GetEvent(cameraUpdateIndex);
            }

        }
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, cameraInputValue, ref currentMouseDeltaVelocity, mouseSmoothTime);
        cameraCap -= currentMouseDelta.y * mouseSensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);
        playerCamera.localEulerAngles = Vector3.right * cameraCap;

        var rotationValue = Vector3.up * currentMouseDelta.x * mouseSensitivity;
        transform.Rotate(rotationValue);
    }

    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.35f, ground);

        Vector2 newMoveInputValue;

        if (!cloningScript.isClone)
        {
            newMoveInputValue = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            newMoveInputValue.Normalize();

            if (recorder.isRecording && (!newMoveInputValue.Equals(moveInputValue) || !hasRecordedMoveInputValueUpdate)) // is different OR is the first recording
            {
                recorder.Push(Recorder.EventType.MoveInputValueUpdate, Time.time - recorder.GetRecordingStartTime(), newMoveInputValue);
                hasRecordedMoveInputValueUpdate = true;
            }
            else if (!recorder.isRecording) hasRecordedMoveInputValueUpdate = false;

            moveInputValue = newMoveInputValue;
        }
        else
        {
            // Get Playback Value
            Tuple<Recorder.EventType, float, Vector3> tuple = cloningScript.recorder.GetEvent(moveInputValueUpdateIndex);
            if (tuple != null && tuple.Item2 <= Time.time - cloningScript.recorder.GetPlayStartTime())
            {
                if (tuple.Item1 == Recorder.EventType.MoveInputValueUpdate)
                {
                    moveInputValue = tuple.Item3;
                }
                moveInputValueUpdateIndex++;
                //tuple = cloningScript.recorder.GetEvent(moveDirUpdateIndex);
            }
        }
        currentDir = Vector2.SmoothDamp(currentDir, moveInputValue, ref currentDirVelocity, moveSmoothTime);
        if (!isGrounded)
            velocityY += gravity * 2f * Time.deltaTime;
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * moveSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);

        // JUMP
        if (isGrounded)
        {
            if (!cloningScript.isClone && Input.GetButtonDown("Jump"))
            {
                velocityY = Jump();
                if (recorder.isRecording)
                {
                    recorder.Push(Recorder.EventType.Jump, Time.time - recorder.GetRecordingStartTime());
                }
            }
            else if (cloningScript.isClone)
            {
                // Get Playback Value
                Tuple<Recorder.EventType, float, Vector3> tuple2 = cloningScript.recorder.GetEvent(jumpIndex);
                if (tuple2 != null && tuple2.Item2 <= Time.time - cloningScript.recorder.GetPlayStartTime())
                {
                    if (tuple2.Item1 == Recorder.EventType.Jump)
                    {
                        velocityY = Jump();
                    }
                    jumpIndex++;
                    //tuple2 = cloningScript.recorder.GetEvent(jumpIndex);
                }
            }
        }

        //if (isGrounded! && controller.velocity.y < -1f)
        //{
        //    velocityY = -8f;
        //}

        //if (isGrounded && controller.velocity.y < -1f)
        //{
        //    velocityY = 0f;
        //}
    }

    public float Jump()
    {
        float velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);

        return velocityY;
    }

    public void ResetPlayIndexes()
    {
        moveInputValueUpdateIndex = 0;
        cameraInputValueUpdateIndex = 0;
        jumpIndex = 0;
    }
}