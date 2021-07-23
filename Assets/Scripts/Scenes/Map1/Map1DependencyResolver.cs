using UnityEngine;

public class Map1DependencyResolver : MonoBehaviour
{

    public GameObject canvasControllerObject;
    public GameObject playerSpawnerObject;

    private UIController canvasController;
    private PlayerSpawner playerSpawner;

    // Start is called before the first frame update
    void Start()
    {
        canvasController = canvasControllerObject.GetComponent<UIController>();
        playerSpawner = playerSpawnerObject.GetComponent<PlayerSpawner>();

        playerSpawner.canvasController = canvasController;
    }
}
