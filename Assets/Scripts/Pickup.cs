using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float throwForce = 600;
    public bool canHold = true;
    public GameObject item;
    public GameObject tempParent;
    public bool isHolding = false;

    Vector3 objectPos;
    float distance;

    // Update is called once per frame
    void Update()
    {

        if (distance >= 1f)
        {
            isHolding = false;
        }

        if (isHolding)
        {
            item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            item.transform.SetParent(tempParent.transform);

            if (Input.GetMouseButtonDown(1))
            {
                Vector3 throwDir = new Vector3(tempParent.transform.forward.x, 1, tempParent.transform.forward.z);
                item.GetComponent<Rigidbody>().AddForce(throwDir * throwForce);
                isHolding = false;
            }
        } else
        {
            objectPos = item.transform.position;
            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.transform.position = objectPos;
        }
        // Check if isholding
    }

    public void PickupItem(GameObject player)
    {
        Debug.Log(player.name);
        tempParent = player;
        isHolding = true;
        item.GetComponent<Rigidbody>().useGravity = false;
        item.GetComponent<Rigidbody>().detectCollisions = true;
    }

    public float GetDistanceTo(GameObject player)
    {
        return Vector3.Distance(item.transform.position, player.transform.position);
    }

    void OnMouseDown()
    {
        isHolding = true;
        item.GetComponent<Rigidbody>().useGravity = false;
        item.GetComponent<Rigidbody>().detectCollisions = true;
    }

    private void OnMouseUp()
    {
        isHolding = false;
    }
}
