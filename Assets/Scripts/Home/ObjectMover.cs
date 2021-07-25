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

            if (IsMovementAllowed(hitInfo.collider.gameObject))
            {

                Collider collider = movable.GetComponent<Collider>();

                if (hitInfo.collider.gameObject.name == "wall_right")
                {
                    Vector3 offset = hitInfo.normal * collider.bounds.size.x;
                    Vector3 transform = hitInfo.point + offset;
                    movable.transform.position = transform;
                }
                else
                {
                    movable.transform.position = hitInfo.point;
                }
            }
        }
    }

    public bool IsDragging()
    {
        return movable != null;
    }

    private bool IsMovementAllowed(GameObject collidedObject)
    {
        CustomTag customTag = movable.GetComponent<CustomTag>();
        
        if (customTag.placements.Count == 0)
        {
            return true;
        } else if (IsWall(collidedObject) && customTag.placements.Contains(CustomTag.placementWall))
        {
            return true;
        } else if (IsFloor(collidedObject) && customTag.placements.Contains(CustomTag.placementFloor))
        {
            return true;
        }

        return false;
    }

    private bool IsWall(GameObject gameObject)
    {
        return gameObject.name.ToLower().StartsWith("wall");
    }

    private bool IsFloor(GameObject gameObject)
    {
        return gameObject.name.ToLower().StartsWith("floor");
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
