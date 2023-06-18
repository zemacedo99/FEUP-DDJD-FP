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

	private List<int> updatedIndices  = new List<int>();
	private List<float> updatedAlphas = new List<float>();


	
	void Start () {
		mesh = fogOfWarPlane.GetComponent<MeshFilter>().mesh;
		vertices = mesh.vertices;
		colors = new Color[vertices.Length];
		for (int i=0; i < colors.Length; i++) {
			colors[i] = Color.black;
		}

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
					if (colors[i].a != alpha)
					{
						colors[i].a = alpha;
						updatedIndices.Add(i);
						updatedAlphas.Add(alpha);
					}
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
		//Debug.Log("SaveFogOfWarData: " + updatedIndices.Count + " " + updatedAlphas.Count);
        SaveSystem.SaveFogOfWarData(updatedIndices,updatedAlphas);

    }

    void LoadFogOfWarData()
    {
        FogOfWarData data = SaveSystem.LoadFogOfWarData();
		if (data == null)
			return;

		//Debug.Log("LoadFogOfWarData: " + data.updatedIndices.Count + " " + data.updatedAlphas.Count);

		updatedIndices = data.updatedIndices;
		updatedAlphas = data.updatedAlphas;

		for(int indice = 0; indice < data.updatedIndices.Count; indice++)
		{
			colors[data.updatedIndices[indice]].a = data.updatedAlphas[indice];
		}
    }

}



