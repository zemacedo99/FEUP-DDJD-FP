using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("World", LoadSceneMode.Single);
    }
}
