using UnityEngine;

public class Map1DependencyResolver : MonoBehaviour
{

    public GameObject canvasControllerObject;
    public GameObject playerSpawnerObject;
    public GameObject packageServiceObject;
    public GameObject homeControllerObject;

    private UIController canvasController;
    private PlayerSpawner playerSpawner;
    private PackageService packageService;
    private HomeController homeController;

    // Start is called before the first frame update
    void Start()
    {
        canvasController = canvasControllerObject.GetComponent<UIController>();
        playerSpawner = playerSpawnerObject.GetComponent<PlayerSpawner>();
        packageService = packageServiceObject.GetComponent<PackageService>();
        homeController = homeControllerObject.GetComponent<HomeController>();

        playerSpawner.canvasController = canvasController;
        homeController.packageService = packageService;
    }
}
