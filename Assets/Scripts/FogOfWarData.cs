using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class FogOfWarData
{
    public float[] colorData;

    public FogOfWarData(Mesh mesh)
    {
        colorData = new float[mesh.colors.Length * 4];

        for (int i = 0; i < mesh.colors.Length; i++)
        {
            int index = i * 4;
            colorData[index] = mesh.colors[i].r;
            colorData[index + 1] = mesh.colors[i].g;
            colorData[index + 2] = mesh.colors[i].b;
            colorData[index + 3] = mesh.colors[i].a;
        }
    }
}
