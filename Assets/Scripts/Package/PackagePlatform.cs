using System;
using System.Collections.Generic;
using UnityEngine;

public class PackagePlatform : MonoBehaviour
{

    private List<GameObject> packageList = new List<GameObject>();
    public GameObject packageServiceObject;
    public GameObject destinationControllerObject;

    void OnCollisionEnter(Collision collision)
    {
        int x = packageList.Count % 3 - 1;
        int z = packageList.Count / 3 - 1;

        if (packageList.Contains(collision.gameObject))
        {
            return;
        }

        Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();
        DeliveryPackage package = collision.gameObject.GetComponent<DeliveryPackage>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.rotation = Quaternion.identity;
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        collision.gameObject.transform.position = GetPackageTransform(x, z, collision.gameObject);
        packageList.Add(collision.gameObject);

        collision.transform.rotation = Quaternion.identity;

        DeliveryDestinationController destinationController = destinationControllerObject.GetComponent<DeliveryDestinationController>();
        destinationController.TriggerPackageDelivered(package);
            
        //packageServiceObject.GetComponent<PackageService>().SpawnPackage();
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

    protected virtual void OnPackageAdded(PackageAddedEventArgs e)
    {
        EventHandler<PackageAddedEventArgs> handler = PackageAdded;
        if (handler != null)
        {
            handler(this, e);
        }
    }

    public event EventHandler<PackageAddedEventArgs> PackageAdded;

    public class PackageAddedEventArgs : EventArgs
    {
        public DeliveryPackage Package { get; set; }
    }
}
