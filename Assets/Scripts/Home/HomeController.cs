using UnityEngine;

public class HomeController : MonoBehaviour
{
    public GameObject homeObject;
    public GameObject environmentObject;

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

        environmentObject.SetActive(false);
        homeObject.SetActive(true);
        Camera.main.transform.position = new Vector3(0, 6, 7);
        Camera.main.transform.rotation = Quaternion.Euler(46f, 180f, 0);
    }

    private void CloseHome()
    {
        homeObject.SetActive(false);
        environmentObject.SetActive(true);

        foreach (GameObject player in PlayerSpawner.instance.GetPlayers())
        {
            player.SetActive(true);
        }
    }
}
