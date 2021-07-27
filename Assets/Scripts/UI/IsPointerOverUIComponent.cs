
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IsPointerOverUIComponent
{

    private static IsPointerOverUIComponent instance;

    private IsPointerOverUIComponent() { }

    public static IsPointerOverUIComponent GetInstance()
    {
        if (instance == null)
        {
            instance = new IsPointerOverUIComponent();
        }

        return instance;
    }

    public bool Test()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        return results.Count > 0;
    }
}
