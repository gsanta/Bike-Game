using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{

    public List<TaskButtonController> tasks = new List<TaskButtonController>();
    public TaskButtonController templateTaskButton;
    
    [HideInInspector] public PackageService packageService;

    void Start()
    {
        templateTaskButton.gameObject.SetActive(false);

        CreateTask("First task", 100);
    }

    public void CreateTask(string description, int money)
    {
        TaskButtonController newTaskButton = Instantiate(templateTaskButton, templateTaskButton.transform.parent);
        newTaskButton.gameObject.SetActive(true);
        newTaskButton.taskController = this;

        DeliveryPackage deliveryPackage = packageService.SpawnPackage();

        TaskInfo taskInfo = new TaskInfo(description, money, deliveryPackage);
        
        deliveryPackage.taskController = this;
        deliveryPackage.taskInfo = taskInfo;

        tasks.Add(newTaskButton);
    }

    public void AssignTask(PlayerController player, TaskInfo taskInfo)
    {
        taskInfo.deliveryPackage.AssignTo(player);
        //taskObject.taskInfo.taskState = TaskInfo.TaskState.ASSIGNED;
    }

    public void FinishTask(TaskInfo taskInfo)
    {
        taskInfo.taskState = TaskInfo.TaskState.FINISHED;
    }

    public event EventHandler TaskFinished;
}
