using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticle : MonoBehaviour
{
    public Rigidbody rb;
    public float spawnOffset = 0.1f;
    public float speedHor = 1f;
    public float speedVer = 0.1f;
    public float minScale = 0.3f;
    public float maxScale = 0.6f;

    private void Start()
    {
        this.transform.position = new Vector3(this.transform.position.x + Random.Range(-spawnOffset, spawnOffset),
                                            this.transform.position.y + Random.Range(-spawnOffset, spawnOffset),
                                            this.transform.position.z + Random.Range(-spawnOffset, spawnOffset));

        this.rb.velocity = new Vector3(Random.Range(-speedHor, speedHor), Random.Range(speedVer/2, speedVer), Random.Range(-speedHor, speedHor));

        this.transform.localScale = new Vector3(Random.Range(minScale, maxScale), Random.Range(minScale, maxScale), Random.Range(minScale, maxScale));

        Invoke(nameof(Kill), 1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    private void Kill()
    {
        Destroy(this.gameObject);
    }
}
