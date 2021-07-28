using UnityEngine;

public class PackageService : MonoBehaviour
{

    public static PackageService instance;
    public GameObject referencePackage;
    public GameObject[] spawnPoints;
    [HideInInspector] public TaskController taskController;
    private const float limit = 2;

    PackageService()
    {
        instance = this;
    }

    public void Start()
    {
        referencePackage.SetActive(false);
        //SpawnPackage();

        foreach (GameObject spawnPoint in spawnPoints)
        {
            spawnPoint.SetActive(false);
        }
    }

    public DeliveryPackage SpawnPackage()
    {
        Debug.Log(taskController);
        Transform transform = spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
        DeliveryPackage newPackage = Instantiate(referencePackage.GetComponent<DeliveryPackage>(), referencePackage.transform.parent);
        newPackage.taskController = taskController;
        newPackage.transform.position = transform.position;
        newPackage.gameObject.SetActive(true);
        newPackage.transform.SetParent(gameObject.transform);

        return newPackage;
    }

    public GameObject GetNearestPackage(GameObject player)
    {
        float minDistance = float.MaxValue;
        GameObject minDistancePackage = null;

        foreach (TaskButtonController task in taskController.tasks)
        {
            GameObject package = task.taskInfo.deliveryPackage.gameObject;
            float distance = Vector3.Distance(package.transform.position, player.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                minDistancePackage = package;
            }
        }

        return minDistance < limit ? minDistancePackage : null;
    }
}
