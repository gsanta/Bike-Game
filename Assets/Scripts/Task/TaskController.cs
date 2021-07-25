using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{

    public List<TaskObject> tasks = new List<TaskObject>();
    public TaskObject templateTaskObject;
    
    [HideInInspector] public PackageService packageService;

    void Start()
    {
        templateTaskObject.gameObject.SetActive(false);

        TaskInfo taskInfo = new TaskInfo();
        taskInfo.description = "First task";
        taskInfo.money = 100;
        CreateTask(taskInfo);
    }

    public void CreateTask(TaskInfo taskInfo)
    {
        TaskObject newTaskObject = Instantiate(templateTaskObject, templateTaskObject.transform.parent);
        
        DeliveryPackage deliveryPackage = packageService.SpawnPackage();
        newTaskObject.deliveryPackage = deliveryPackage;
        
        deliveryPackage.taskController = this;
        deliveryPackage.taskObject = newTaskObject;

        tasks.Add(newTaskObject);
    }

    public void AssignTask(TaskObject taskObject)
    {
        //taskObject.taskInfo.taskState = TaskInfo.TaskState.ASSIGNED;
    }

    public void FinishTask(TaskObject taskObject)
    {
        taskObject.taskInfo.taskState = TaskInfo.TaskState.FINISHED;
    }

    public event EventHandler TaskFinished;
}
