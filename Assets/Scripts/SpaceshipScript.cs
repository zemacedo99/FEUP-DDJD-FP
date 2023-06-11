using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipScript : MonoBehaviour
{
    public Animator spaceshipAnim;
    public GameObject cam;
    public GameObject player;

    private void Start()
    {
        Invoke(nameof(PlayAnimation), 1f);

    }
    void PlayAnimation()
    {
        player.SetActive(false);
        cam.SetActive(true);
        spaceshipAnim.SetTrigger("Play");
    }
}
