using UnityEngine;

public class TaskInfo : MonoBehaviour
{
    public enum TaskState { UNASSIGNED, ASSIGNED, FINISHED }
    public string description;
    public int money;
    public TaskState taskState;
}
