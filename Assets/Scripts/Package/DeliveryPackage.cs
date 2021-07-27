using UnityEngine;

public class DeliveryPackage : MonoBehaviour
{
    public float throwForce = 600;
    public bool canHold = true;
    public GameObject item;
    public bool isHolding = false;
    public TaskController taskController;
    public TaskInfo taskInfo;
    private const float limit = 2;

    Vector3 objectPos;
    float distance;

    void Update()
    {

        if (distance >= 1f)
        {
            isHolding = false;
        }

        if (isHolding)
        {
            PlayerController player = taskInfo.player;

            item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            item.transform.SetParent(player.transform);
        } else
        {
            objectPos = item.transform.position;
            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.transform.position = objectPos;
        }
    }

    public bool IsWithinPickupDistance()
    {
        float distance = Vector3.Distance(transform.position, taskInfo.player.transform.position);
        if (distance <= limit)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void Throw()
    {
        PlayerController player = taskInfo.player;
        Vector3 throwDir = new Vector3(player.transform.forward.x, 1, player.transform.forward.z);
        item.GetComponent<Rigidbody>().AddForce(throwDir * throwForce);
        isHolding = false;
    }

    public void Pickup()
    {
        isHolding = true;
        item.GetComponent<Rigidbody>().useGravity = false;
        item.GetComponent<Rigidbody>().detectCollisions = true;
    }
}
