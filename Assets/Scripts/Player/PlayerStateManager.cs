/**using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStateManager : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        LoadPlayerData();
    }

    public void LoadPlayerData()
    {
        PlayerData playerData = PlayerSaveSystem.Load();

        if(SceneManager.GetActiveScene().name == "World")
        {
            print("wolrd");
            player.transform.position = new Vector3(playerData.positionWorld[0], playerData.positionWorld[1], playerData.positionWorld[2]);
        }
        else
        {
            print("here");
            player.transform.position = new Vector3(playerData.positionPuzzle[0], playerData.positionPuzzle[1], playerData.positionPuzzle[2]);
        }

        print(player.transform.position.x + " " + player.transform.position.y + " " + player.transform.position.z);


        if (player.GetComponent<Oxygen>() != null)
        {
            player.GetComponent<Oxygen>().SetOxygenValue(playerData.oxygenLevel);
        }
    }

}
**/