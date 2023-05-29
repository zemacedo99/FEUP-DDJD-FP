using UnityEngine;

public class FootSteps : MonoBehaviour
{
    private TerrainDetector terrainDetector;


    private void Awake()
    {
        terrainDetector = new TerrainDetector();
    }

    public int Step()
    {
        int terrainTextureIndex = terrainDetector.GetActiveTerrainTextureIdx(transform.position);
        int surfaceType = 1;
        
        switch(terrainTextureIndex)
        {
            case 0:
                surfaceType = 1;
                break;
            case 2:
                surfaceType = 3;
                break;
            case 4:
                surfaceType = 2;
                break;
        }
        return surfaceType;
    }
}