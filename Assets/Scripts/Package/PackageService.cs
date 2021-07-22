using System.Collections.Generic;
using UnityEngine;

public class PackageService : MonoBehaviour
{

    public static PackageService instance;
    public GameObject package1;
    public GameObject package2;
    public List<GameObject> packageList = new List<GameObject>();
    public GameObject referencePackage;
    private const float limit = 2;

    PackageService()
    {
        instance = this;
    }

    public void Start()
    {
        referencePackage.SetActive(false);
        SpawnPackage();
    }

    public void SpawnPackage()
    {
        DeliveryPackage newPackage = Instantiate(referencePackage.GetComponent<DeliveryPackage>(), referencePackage.transform.parent);
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
