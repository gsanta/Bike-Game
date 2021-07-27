using UnityEngine;
using UnityEngine.UI;

public class TaskButtonController : MonoBehaviour
{
    [HideInInspector] public TaskInfo taskInfo;
    [HideInInspector] public PlayerSpawner playerSpawner;
    [HideInInspector] public Toggle toggle;
    public string abcd;

    private void Start()
    {
        //Toggle toggle = gameObject.GetComponent<Toggle>();
        //toggle.onValueChanged.AddListener(delegate
        //{
        //    ToggleValueChanged(toggle);
        //});
    }

    public void OnClick()
    {
        Image img = GetComponent<Image>();
        img.color = Color.yellow;
        Debug.Log("Toggle click");
        Debug.Log(abcd);
        Debug.Log(taskInfo);
        taskInfo.AssignTo(playerSpawner.GetPlayer());
    }
}
