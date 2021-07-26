using System;
using UnityEngine;

public class DeliveryDestinationController : MonoBehaviour
{
    public TaskController taskController;

    public void TriggerPackageDelivered(DeliveryPackage package)
    {
        taskController.FinishTask(package.taskInfo);
        //PackageDeliveredEventArgs packageAddedEventArgs = new PackageDeliveredEventArgs();
        //packageAddedEventArgs.Package = package;

        //EventHandler<PackageDeliveredEventArgs> handler = PackageDelivered;
        //if (handler != null)
        //{
        //    handler(this, packageAddedEventArgs);
        //}
    }

    public event EventHandler<PackageDeliveredEventArgs> PackageDelivered;

    public class PackageDeliveredEventArgs : EventArgs
    {
        public DeliveryPackage Package { get; set; }
    }
}
