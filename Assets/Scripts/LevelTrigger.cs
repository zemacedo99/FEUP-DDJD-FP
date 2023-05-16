using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : SceneDict
{
    public string scene;

    private void Start()
    {
        if (scene != "World" && !holeDict.ContainsKey(scene))
            holeDict.Add(scene, this.gameObject.transform.parent.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (scene != "World")
        {
            print("joining");
            EnterPuzzle();
        }
        else
            LeavePuzzle();
    }

    private void PlayerCurrentStatus()
    {
        GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerInventory>().inventory.Save(); // Saves Player Current 

        // TODO: Get player's current spawning point
        int currentSpawnPoint = 0;
        PlayerPrefs.SetInt("SPAWN_POINT", currentSpawnPoint);
    }

    private void EnterPuzzle()
    {
        SceneManager.LoadScene(scene);
    }

    private void LeavePuzzle()
    {
        print("leave puzzle");
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
        SceneManager.LoadScene("World");
    }
}
