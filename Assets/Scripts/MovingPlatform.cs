using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 targetA, targetB;
    [SerializeField] private float speed;
    [SerializeField] private bool changedDirection = false;
    public bool moving = false;
    private Vector3 initialPosition;

    public FMODUnity.EventReference movingPlatformEvent;
    FMOD.Studio.EventInstance movingPlatformEventInstance;
    public FMODUnity.EventReference movingPlatformStopEvent;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    void FixedUpdate()
    {
        if (moving)
        {
            if (!changedDirection)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, initialPosition + targetB, speed * Time.deltaTime);
            }
            else if (changedDirection)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, initialPosition + targetA, speed * Time.deltaTime);
            }
            if (transform.localPosition == initialPosition + targetB)
            {
                changedDirection = true;
            }
            else if (transform.localPosition == initialPosition + targetA)
            {
                changedDirection = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }

    public void SetMoving(bool newMoving)
    {
        if (!moving && newMoving)
        {
            // Start Moving
            movingPlatformEventInstance = FMODUnity.RuntimeManager.CreateInstance(movingPlatformEvent);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(movingPlatformEventInstance, gameObject.transform);
            movingPlatformEventInstance.start();
        }
        else if (moving && !newMoving)
        {
            // Stop Moving
            FMODUnity.RuntimeManager.PlayOneShotAttached(movingPlatformStopEvent, gameObject);
        }
        moving = newMoving;
    }

    private void OnDestroy()
    {
        movingPlatformEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
