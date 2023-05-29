using UnityEngine;

public class FootSteps : MonoBehaviour
{
    private TerrainDetector terrainDetector;
    public FMODUnity.EventReference footstepsEvent;
    public FMODUnity.EventReference footstepsEvent1;
    public FMODUnity.EventReference footstepsEvent2;
    public FMODUnity.EventReference footstepsEvent3;
    public FMODUnity.EventReference footstepsEvent4;
    public FMODUnity.EventReference footstepsEvent5;
    public FMODUnity.EventReference footstepsEvent6;


    private void Awake()
    {
        terrainDetector = new TerrainDetector();
    }

    public void Step()
    {
        int terrainTextureIndex = terrainDetector.GetActiveTerrainTextureIdx(transform.position);
        Debug.Log(terrainTextureIndex);

        switch(terrainTextureIndex)
        {
            case 0:
                FMODUnity.RuntimeManager.PlayOneShotAttached(footstepsEvent, gameObject);
                break;
            case 1:
                FMODUnity.RuntimeManager.PlayOneShotAttached(footstepsEvent1, gameObject);
                break;
            case 2:
                FMODUnity.RuntimeManager.PlayOneShotAttached(footstepsEvent2, gameObject);
                break;
            case 3:
                FMODUnity.RuntimeManager.PlayOneShotAttached(footstepsEvent3, gameObject);
                break;
            case 4:
                FMODUnity.RuntimeManager.PlayOneShotAttached(footstepsEvent4, gameObject);
                break;
            case 5:
                FMODUnity.RuntimeManager.PlayOneShotAttached(footstepsEvent5, gameObject);
                break;
            case 6:
                FMODUnity.RuntimeManager.PlayOneShotAttached(footstepsEvent6, gameObject);
                break;
            default:
                FMODUnity.RuntimeManager.PlayOneShotAttached(footstepsEvent, gameObject);
                break;
        }
    }
}