using UnityEngine;
using UnityEngine.UI;

public class TaskButtonController : MonoBehaviour
{
    [HideInInspector] public TaskInfo taskInfo;
    [HideInInspector] public TaskController taskController;
    [HideInInspector] public PlayerSpawner playerSpawner;
    [HideInInspector] public Toggle toggle;

    private void Start()
    {
        Toggle toggle = gameObject.GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate
        {
            ToggleValueChanged(toggle);
        });
    }

    public void ToggleValueChanged(Toggle toggle)
    {
        Debug.Log("Toggle value changed");
        taskController.AssignTask(playerSpawner.GetPlayer(), taskInfo);
    }
}
