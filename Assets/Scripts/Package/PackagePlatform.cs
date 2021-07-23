using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackagePlatform : MonoBehaviour
{

    private List<GameObject> packageList = new List<GameObject>();
    public GameObject packageServiceObject;

    void OnCollisionEnter(Collision collision)
    {
        int x = packageList.Count % 3 - 1;
        int z = packageList.Count / 3 - 1;

        if (packageList.Contains(collision.gameObject))
        {
            return;
        }

        Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.rotation = Quaternion.identity;
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        collision.gameObject.transform.position = GetPackageTransform(x, z, collision.gameObject);
        packageList.Add(collision.gameObject);
        //RemovePackageCollider(collision.gameObject);

        collision.transform.rotation = Quaternion.identity;

        packageServiceObject.GetComponent<PackageService>().SpawnPackage();
        //Debug.Log("Collision detected with: " + collision.gameObject.name);
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "MyGameObjectName")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Do something here");
        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "MyGameObjectTag")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something else here");
        }
    }

    private Vector3 GetPackageTransform(int posX, int posZ, GameObject package)
    {
        Vector3 size = GetComponent<Collider>().bounds.size;
        Vector3 packageSize = package.GetComponent<Collider>().bounds.size;
        Transform packageTransform = package.transform;
        Vector3 up = transform.up * (transform.localScale.y / 2f) + packageTransform.up * (packageTransform.localScale.y / 2f);
        float x = (size.x / 2f - packageSize.x / 2f) * posX;
        float z = (size.z / 2f - packageSize.z / 2f) * posZ;

        Debug.Log("x: " + x + " z" + z);

        Vector3 pos = (transform.position + up);
        pos.x += x;
        pos.z += z;

        return pos;
    }
}
