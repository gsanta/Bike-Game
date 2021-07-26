using UnityEngine;

public class DeliveryPackage : MonoBehaviour
{
    public float throwForce = 600;
    public bool canHold = true;
    public GameObject item;
    public bool isHolding = false;
    public TaskController taskController;
    public TaskInfo taskInfo;
    private PlayerController owner;

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
            item.transform.SetParent(owner.transform);
        } else
        {
            objectPos = item.transform.position;
            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.transform.position = objectPos;
        }
    }

    public void FinishDelivery()
    {
        Vector3 throwDir = new Vector3(owner.transform.forward.x, 1, owner.transform.forward.z);
        item.GetComponent<Rigidbody>().AddForce(throwDir * throwForce);
        isHolding = false;
    }

    public void AssignTo(PlayerController player)
    {
        owner = player;
    }

    public void Pickup(PlayerController player)
    {
        if (owner == player)
        {
            isHolding = true;
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().detectCollisions = true;
        }
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
