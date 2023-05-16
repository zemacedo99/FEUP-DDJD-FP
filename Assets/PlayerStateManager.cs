using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Awake()
    {
        LoadPlayerData();
    }

    public void LoadPlayerData()
    {
        PlayerData playerData = PlayerSaveSystem.Load();

        print(playerData.position[0] + " " + playerData.position[1] + " " + playerData.position[2]);

        player.transform.position = new Vector3(playerData.position[0], playerData.position[1], playerData.position[2]);

        if (player.GetComponent<Oxygen>() != null)
        {
            player.GetComponent<Oxygen>().SetOxygenValue(playerData.oxygenLevel);
        }
    }

}
