using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : SceneDict
{
    public string scene;

    private void Start()
    {
        if(scene != "World" && !holeDict.ContainsKey(scene))
            holeDict.Add(scene, this.gameObject.transform.parent.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (scene != "World")
            EnterPuzzle();
        else
            LeavePuzzle();
    }

    private void EnterPuzzle()
    {
        SceneManager.LoadScene(scene);
    }

    private void LeavePuzzle()
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
        SceneManager.LoadScene("World");
    }
}
