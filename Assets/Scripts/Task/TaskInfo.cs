using UnityEngine;

public class TaskInfo
{
    public enum TaskState { UNASSIGNED, ASSIGNED, PICKEDUP, DELIVERED }
    public readonly string description;
    public readonly int money;
    public TaskState taskState;
    public readonly DeliveryPackage deliveryPackage;
    public PlayerController player;

    public TaskInfo(string _description, int _money, DeliveryPackage _deliveryPackage)
    {
        description = _description;
        money = _money;
        deliveryPackage = _deliveryPackage;
        taskState = TaskState.UNASSIGNED;
    }

    public void AssignTo(PlayerController _player)
    {
        player = _player;
        Debug.Log("Assigned to: ");
        Debug.Log(this);
        player.TaskInfo = this;
        taskState = TaskState.ASSIGNED;
    }

    public void Pickup()
    {
        taskState = TaskState.PICKEDUP;
        deliveryPackage.Pickup();
    }

    public void Delivered()
    {
        taskState = TaskState.DELIVERED;
        deliveryPackage.Throw();
    }
}
