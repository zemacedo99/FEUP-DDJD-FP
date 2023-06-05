using UnityEngine;
using UnityEngine.SceneManagement;


public class FootSteps : MonoBehaviour
{
    private TerrainDetector terrainDetector;
    private bool onWater = false;
    public enum SurfaceType
    {
        Concrete,
        Grass,
        Water,
        Rock
    }
    PlayerMovement playerMovementScript;

    private void Awake()
    {
        terrainDetector = new TerrainDetector();

        playerMovementScript = GetComponent<PlayerMovement>();
    }

    public int GetSurfaceType(string currentSceneName)
    {
        if (currentSceneName != "World")
            return (int)SurfaceType.Concrete;

        if (onWater) return (int)SurfaceType.Water;

        int terrainTextureIndex = terrainDetector.GetActiveTerrainTextureIdx(transform.position);
        int surfaceType;
        
        switch(terrainTextureIndex)
        {
            case 2:
                surfaceType = (int)SurfaceType.Rock;
                break;
            case 4:
                surfaceType = (int)SurfaceType.Rock;
                break;
            default:
                surfaceType = (int)SurfaceType.Grass;
                break;
        }
        return surfaceType;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "water")
        {
            onWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "water")
        {
            onWater = false;
        }
    }
}