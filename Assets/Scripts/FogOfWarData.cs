using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class FogOfWarData
{
    public List<int>  updatedIndices;
    public List<float>  updatedAlphas;

    public FogOfWarData(List<int> indices, List<float>  alphas)
    {
        updatedIndices = indices;
        updatedAlphas = alphas;
    }
}
