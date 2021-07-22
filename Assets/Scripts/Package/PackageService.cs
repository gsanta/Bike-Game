using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageService : MonoBehaviour
{

    public static PackageService instance;
    public GameObject package1;
    public GameObject package2;
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
        DeliveryPackage newPackage = Instantiate(referencePackage.GetComponent<DeliveryPackage>(), referencePackage.transform);
        newPackage.gameObject.SetActive(true);
    }

    public GameObject GetNearestPackage(GameObject player)
    {
        float minDistance = float.MaxValue;
        GameObject minDistancePackage = null;
        GameObject[] packages = GetPackages();

        foreach (GameObject package in packages)
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

    private GameObject[] GetPackages()
    {
        return new GameObject[] { package1, package2 };
    }
}
