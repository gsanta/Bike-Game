using System.Collections.Generic;
using UnityEngine;

public class PackageService : MonoBehaviour
{

    public static PackageService instance;
    public GameObject package1;
    public GameObject package2;
    public List<GameObject> packageList = new List<GameObject>();
    public GameObject referencePackage;
    public GameObject[] spawnPoints;
    private const float limit = 2;

    PackageService()
    {
        instance = this;
    }

    public void Start()
    {
        referencePackage.SetActive(false);
        SpawnPackage();

        foreach (GameObject spawnPoint in spawnPoints)
        {
            spawnPoint.SetActive(false);
        }
    }

    public void SpawnPackage()
    {
        Transform transform = spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
        DeliveryPackage newPackage = Instantiate(referencePackage.GetComponent<DeliveryPackage>(), referencePackage.transform.parent);
        newPackage.transform.position = transform.position;
        newPackage.gameObject.SetActive(true);
        packageList.Add(newPackage.gameObject);
    }

    public GameObject GetNearestPackage(GameObject player)
    {
        float minDistance = float.MaxValue;
        GameObject minDistancePackage = null;

        foreach (GameObject package in packageList)
        {
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
