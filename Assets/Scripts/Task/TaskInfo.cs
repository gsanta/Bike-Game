using UnityEngine;

public class TaskInfo : MonoBehaviour
{
    public enum TaskState { UNASSIGNED, ASSIGNED, FINISHED }
    public readonly string description;
    public readonly int money;
    public TaskState taskState;
    public readonly DeliveryPackage deliveryPackage;

    public TaskInfo(string _description, int _money, DeliveryPackage _deliveryPackage)
    {
        description = _description;
        money = _money;
        deliveryPackage = _deliveryPackage;
        taskState = TaskState.UNASSIGNED;

    }
}
