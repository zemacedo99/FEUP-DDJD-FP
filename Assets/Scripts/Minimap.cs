using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;

    void LateUpdate ()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y,0f);
    }

    public GameObject fogOfWarPlane;
	public LayerMask fogLayer;
	public float radius = 7.5f;
	private float radiusSqr { get { return radius*radius; }}
	
	private Mesh mesh;
	private Vector3[] vertices;
	private Color[] colors;
	
	void Start () {
		mesh = fogOfWarPlane.GetComponent<MeshFilter>().mesh;
		vertices = mesh.vertices;
		colors = new Color[vertices.Length];
		for (int i=0; i < colors.Length; i++) {
			colors[i] = Color.black;
		}
		UpdateColor();
	}
	
	void Update () {
		Ray r = new Ray(transform.position, player.position - transform.position);
		RaycastHit hit;
		if (Physics.Raycast(r, out hit, 1000, fogLayer, QueryTriggerInteraction.Collide)) {
			for (int i=0; i< vertices.Length; i++) {
				Vector3 v = fogOfWarPlane.transform.TransformPoint(vertices[i]);
				float dist = Vector3.SqrMagnitude(v - hit.point);
				if (dist < radiusSqr) {
					float alpha = Mathf.Min(colors[i].a, dist/radiusSqr);
					colors[i].a = alpha;
				}
			}
			UpdateColor();
		}
	}
	
	void UpdateColor() {
		mesh.colors = colors;
	}

}



