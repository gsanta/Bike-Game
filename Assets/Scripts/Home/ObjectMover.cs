using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover
{

    private GameObject movable;

    public bool StartDrag(Vector3 position)
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out hitInfo, 100, LayerMask.GetMask("Furniture")))
        {
            Debug.Log(hitInfo.collider.gameObject.name);
            movable = hitInfo.collider.gameObject;
            return true;
        }

        return false;
    }

    public void EndDrag()
    {
        movable = null;
    }

    public void Drag(Vector3 position)
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out hitInfo, 100, LayerMask.GetMask("Walls")))
        {
            Collider collider = movable.GetComponent<Collider>();

            Debug.Log(hitInfo.collider.gameObject.name);
            if (hitInfo.collider.gameObject.name == "wall_right")
            {

                Vector3 offset = hitInfo.normal * collider.bounds.size.x;
                Vector3 transform = hitInfo.point + offset;
                movable.transform.position = transform;
            } else
            {
                movable.transform.position = hitInfo.point;
            }
        }
    }

    public bool IsDragging()
    {
        return movable != null;
    }

    //public void TestMovement(Vector3 position)
    //{
    //    RaycastHit hitInfo;
    //    Ray ray = Camera.main.ScreenPointToRay(position);
    //    if (Physics.Raycast(ray, out hitInfo, 100))
    //    {
    //        Debug.Log(hitInfo.collider.gameObject.name);
    //    }
    //}
}
