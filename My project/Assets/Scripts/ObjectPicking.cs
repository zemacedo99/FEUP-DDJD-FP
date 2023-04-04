using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPicking : MonoBehaviour
{
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(gameObject);
            GameObject playerClone = Instantiate(playerPrefab, transform.position, Quaternion.identity);

            playerClone.GetComponent<Cloning>().InitClone();
        }
    }
}

