using UnityEngine;

public class HomeController : MonoBehaviour
{
    public GameObject homeObject;
    public GameObject environmentObject;
    
    [HideInInspector] public PackageService packageService;
    [HideInInspector] public TaskController taskController;

    private ObjectMover objectMover;
    private bool isActive = false;
    // Start is called before the first frame update

    public HomeController()
    {
        objectMover = new ObjectMover();
    }

    void Start()
    {
        homeObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            isActive = !isActive;

            if (isActive)
            {
                DisplayHome();
            } else
            {
                CloseHome();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            objectMover.StartDrag(Input.mousePosition);
        }

        if (objectMover.IsDragging() && Input.GetMouseButtonUp(0))
        {
            objectMover.EndDrag();
        }

        if (objectMover.IsDragging() && (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)) {
            objectMover.Drag(Input.mousePosition);
        }
    }

    private void DisplayHome()
    {
        foreach (GameObject player in PlayerSpawner.instance.GetPlayers())
        {
            player.SetActive(false);
        }

        foreach (TaskButtonController taskButton in taskController.tasks)
        {
            taskButton.taskInfo.deliveryPackage.gameObject.SetActive(false);
        }

        environmentObject.SetActive(false);
        homeObject.SetActive(true);
        Camera.main.transform.position = new Vector3(1.43f, 5.14f, 5.49f);
        Camera.main.transform.rotation = Quaternion.Euler(33.723f, 193.305f, 0.129f);
    }

    private void CloseHome()
    {
        homeObject.SetActive(false);
        environmentObject.SetActive(true);

        foreach (GameObject player in PlayerSpawner.instance.GetPlayers())
        {
            player.SetActive(true);
        }

        foreach (TaskButtonController taskButton in taskController.tasks)
        {
            taskButton.taskInfo.deliveryPackage.gameObject.SetActive(false);
        }
    }
}
