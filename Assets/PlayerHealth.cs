using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health = 1;
    [SerializeField] private Image img;
    private CharacterController charc;
    private Vector3 impact = Vector3.zero;

    private void Start()
    {
        charc = GetComponent<CharacterController>();
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Hurt"))
            DoDamage();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("Hurt"))
        {
            impact = (this.gameObject.transform.position - hit.point)*30;

            //impact = new Vector3(-hit.moveDirection.x, -hit.moveDirection.y+1, -hit.moveDirection.z)*10;
            //Debug.Log(impact);
            DoDamage();
        }
    }

    void DoDamage()
    {
        health -= 75 * Time.deltaTime;
        //Debug.Log(Time.deltaTime);
        if (health <= 0)
        {
            //Debug.Log("ded");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Update()
    {
        health = Mathf.Clamp(health + Time.deltaTime, 0, 1);
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1 - health);

        // apply the impact force:
        if (impact.magnitude > 0.2) charc.Move(impact * Time.deltaTime);
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }
}
