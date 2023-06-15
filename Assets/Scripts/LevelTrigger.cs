using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTrigger : SceneDict
{
    public string scene;
    public string requiredItem = "";
    private Image fade;
    private PlayerInventory pInv;

    private void Start()
    {
        if (scene != "World" && !holeDict.ContainsKey(scene))
            holeDict.Add(scene, this.gameObject.transform.parent.gameObject.name);
        pInv = FindObjectOfType<PlayerInventory>();
    }

    private bool HasItem()
    {
        if (requiredItem != null && requiredItem.Length > 0)
            return pInv.HasItem(requiredItem);
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !HasItem())
            return;
        if (scene != "World")
        {
            print("joining");
            EnterPuzzle();
        }
        else
            LeavePuzzle();
    }

    private void EnterPuzzle()
    {
        SceneManager.LoadScene(scene);
    }

    private void LeavePuzzle()
    {
        var pl = GameObject.FindGameObjectWithTag("Player");
        fade = pl.GetComponentInChildren<Camera>().gameObject.GetComponentInChildren<Canvas>().gameObject.GetComponentsInChildren<Image>()[1];
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
        print(SceneManager.GetActiveScene().name);
        InvokeRepeating(nameof(FadeOut), 0f, 0.05f);
        pl.GetComponent<PlayerInventory>().inventory.Save(); // Saves Player Current 
    }

    private void FadeOut()
    {
        fade.color = new Color(1, 1, 1, 1.5f*fade.color.a + 0.01f);
        if(fade.color.a >= 1)
            SceneManager.LoadScene("World");
    }
}
