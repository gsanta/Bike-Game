using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{

    public List<TaskButtonController> tasks = new List<TaskButtonController>();
    public GameObject templateTaskButton;
    public GameObject templateTaskButton2;
    
    [HideInInspector] public PackageService packageService;

    void Start()
    {
        templateTaskButton.gameObject.SetActive(false);

        CreateTask("First task", 100);
    }

    public void CreateTask(string description, int money)
    {
        DeliveryPackage deliveryPackage = packageService.SpawnPackage();
        TaskInfo taskInfo = new TaskInfo(description, money, deliveryPackage);
        
        GameObject newTaskButton = Instantiate(templateTaskButton, templateTaskButton.transform.parent);
        newTaskButton.SetActive(true);
        TaskButtonController taskButtonController = newTaskButton.GetComponent<TaskButtonController>();
        taskButtonController.taskInfo = taskInfo;
        taskButtonController.abcd = "efgh";
        taskButtonController.playerSpawner = PlayerSpawner.instance;
        
        deliveryPackage.taskController = this;
        deliveryPackage.taskInfo = taskInfo;

        tasks.Add(taskButtonController);
    }

    public void FinishTask(TaskInfo taskInfo)
    {
        taskInfo.taskState = TaskInfo.TaskState.DELIVERED;
    }

    public event EventHandler TaskFinished;
}
