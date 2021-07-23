using UnityEngine;

public class HomeController : MonoBehaviour
{
    public GameObject homeObject;
    public GameObject environmentObject;
    
    [HideInInspector] public PackageService packageService;

    private bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Home controller started");
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
    }

    private void DisplayHome()
    {
        foreach (GameObject player in PlayerSpawner.instance.GetPlayers())
        {
            player.SetActive(false);
        }

        foreach (GameObject package in packageService.GetPackages())
        {
            package.SetActive(false);
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

        foreach(GameObject package in packageService.GetPackages())
        {
            package.SetActive(true);
        }
    }
}
