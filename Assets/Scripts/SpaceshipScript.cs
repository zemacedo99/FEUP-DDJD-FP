using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SpaceshipScript : MonoBehaviour
{
    public Animator spaceshipAnim;
    public GameObject cam;
    public GameObject player;

    public GameObject pickup;
    public RawImage itemImage;
    public Texture2D rocketSprite;
    public TextMeshProUGUI pickupText;
    public InputActionAsset actions;
    public InputAction pickupInput;
    private bool inCol;
    public Image fade;

    public PauseMenuScript pms;
    public Light dirLight;
    public GameObject notification;

    public FMODUnity.EventReference spaceshipEvent;

    private void Start()
    {
        actions.FindActionMap("interactions").Enable();
        pickupInput = actions.FindActionMap("interactions", true).FindAction("pickup", true);
        if(pms.UpdateCollectedTape() == 6)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            if(PlayerPrefs.GetInt("MissionNotification") == 0)
            {
                PlayerPrefs.SetInt("MissionNotification", 1);
                notification.GetComponent<NotificationFlash>().Enable();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            pickup.SetActive(true);
            pickupText.text = "Exit!";
            itemImage.texture = rocketSprite;
            inCol = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickup.SetActive(false);
            inCol = false;
        }
    }

    void PlayAnimation()
    {
        player.SetActive(false);
        cam.SetActive(true);
        spaceshipAnim.SetTrigger("Play");
        InvokeRepeating(nameof(ReduceLight), 3.3f, 0.1f);
    }

    private void Update()
    {
        if(pickupInput.WasPressedThisFrame() && inCol && Time.timeScale != 0)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(spaceshipEvent, gameObject);
            InvokeRepeating(nameof(FadeOut), 0f, 0.05f);
            pickup.SetActive(false);
            inCol = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void ReduceLight()
    {
        dirLight.intensity -= 0.05f;
        if(dirLight.intensity <= 0.01f)
        {
            SceneManager.LoadScene("Credits");
        }
    }

    private void FadeOut()
    {
        fade.color = new Color(0, 0, 0, 1.5f * fade.color.a + 0.01f);
        if (fade.color.a >= 1)
            PlayAnimation();
    }
}
