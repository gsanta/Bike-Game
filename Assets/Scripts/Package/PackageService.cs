using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageService : MonoBehaviour
{

    public static PackageService instance;
    public GameObject package1;
    private const float limit = 2;

    PackageService()
    {
        instance = this;
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
        return new GameObject[] { package1 };
    }
}
