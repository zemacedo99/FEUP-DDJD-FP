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
		// for (int i=0; i < colors.Length; i++) {
		// 	colors[i] = Color.black;
		// }

		LoadFogOfWarData(); // Load the fog of war data from file
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

	void OnDestroy()
    {
        SaveFogOfWarData(); // Save the fog of war data to file when the game is closed or the scene is switched
    }


    void SaveFogOfWarData()
    {
        SaveSystem.SaveFogOfWarData(mesh);
    }

    void LoadFogOfWarData()
    {
        FogOfWarData data = SaveSystem.LoadFogOfWarData();

		for (int i = 0; i < mesh.colors.Length; i++)
        {
            int index = i * 4;
			mesh.colors[i].r = data.colorData[index];
			mesh.colors[i].g = data.colorData[index + 1];
			mesh.colors[i].b = data.colorData[index + 2];
			mesh.colors[i].a = data.colorData[index + 3];
        }

    }

}



