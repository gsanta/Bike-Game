using UnityEngine;

public class Map1DependencyResolver : MonoBehaviour
{

    public GameObject canvasControllerObject;
    public GameObject playerSpawnerObject;
    public GameObject packageServiceObject;
    public GameObject homeControllerObject;
    public GameObject deliveryDestinationControllerObject;
    public GameObject taskControllerObject;

    private UIController canvasController;
    private PlayerSpawner playerSpawner;
    private PackageService packageService;
    private HomeController homeController;
    private DeliveryDestinationController deliveryDestinationController;
    private TaskController taskController;

    void Awake()
    {
        canvasController = canvasControllerObject.GetComponent<UIController>();
        playerSpawner = playerSpawnerObject.GetComponent<PlayerSpawner>();
        packageService = packageServiceObject.GetComponent<PackageService>();
        homeController = homeControllerObject.GetComponent<HomeController>();
        deliveryDestinationController = deliveryDestinationControllerObject.GetComponent<DeliveryDestinationController>();
        taskController = taskControllerObject.GetComponent<TaskController>();

        playerSpawner.canvasController = canvasController;
        homeController.packageService = packageService;
        taskController.packageService = packageService;
        deliveryDestinationController.taskController = taskController;
        packageService.taskController = taskController;
        homeController.taskController = taskController;
    }
}
