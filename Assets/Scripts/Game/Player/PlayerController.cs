using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public int playerId;
    public Transform viewPoint;
    public float mouseSensitivity = 1f;
    public float moveSpeed = 5f, runSpeed = 8f;
    private float activeMoveSpeed;
    private Vector3 moveDir, movement;
    public float jumpForce = 12f, gravityMod = 2.5f;
    public GameObject package;
    public CharacterController charController;
    private Camera cam;
    public Transform groundCheckPoint;
    private bool isGrounded;
    public LayerMask groundLayers;
    public GameObject bulletImpact;
    public float muzzleDisplayTime;
    public float maxHeat = 10f, coolRate = 4f, overheatCoolRate = 5f;
    private float heatCounter;
    public GameObject playerHitImpact;
    public int maxHealth = 100;
    public GameObject playerModel;
    public PlayerData playerData;

    public TaskInfo taskInfo;

    [HideInInspector] public UIController canvasController;
    [HideInInspector] public GameObject homeControllerObject;


    public TaskInfo TaskInfo
    {
        set
        {
            Debug.Log("Setting taskInfo");
            Debug.Log(value);
            taskInfo = value;
        }
    }

    private void Awake()
    {
        playerData = new PlayerData(photonView);
    }

    void Start()
    {
        Debug.Log("playercontroller start");
        //Cursor.lockState = CursorLockMode.Locked;

        cam = Camera.main;
    }

    void Update()
    {
        if (playerData.IsMine)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + horizontal, transform.rotation.eulerAngles.z);

            moveDir = new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"));

            if (Input.GetKey(KeyCode.LeftShift))
            {
                activeMoveSpeed = runSpeed;
            }
            else
            {
                activeMoveSpeed = moveSpeed;
            }

            float yVal = movement.y;
            movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized * activeMoveSpeed;
            movement.y = yVal;

            isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, .25f, groundLayers);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                movement.y = jumpForce;
            }
            else if (charController.isGrounded)
            {
                movement.y = 0;
            }
            else
            {
                movement.y += Physics.gravity.y * Time.deltaTime * gravityMod;
            }

            charController.Move(movement * Time.deltaTime);

            if (heatCounter < 0)
            {
                heatCounter = 0;
            }


            if (Input.GetMouseButtonDown(0) && taskInfo != null)
            {
                if (!IsPointerOverUIComponent.GetInstance().Test())
                {
                    if (taskInfo.taskState == TaskInfo.TaskState.PICKEDUP)
                    {
                        ThrowPackage();
                    } else
                    {
                        PickupPackage();
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        } else if (Cursor.lockState == CursorLockMode.None)
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    Cursor.lockState = CursorLockMode.Locked;
            //}
        }
    }

    private void ThrowPackage()
    {
        if (taskInfo != null)
        {
            taskInfo.Delivered();
        }
    }

    private void PickupPackage()
    {
        if (taskInfo != null && taskInfo.taskState == TaskInfo.TaskState.ASSIGNED)
        {
            if (taskInfo.deliveryPackage.IsWithinPickupDistance())
            {
                taskInfo.Pickup();
            }
        }
    }

    private void LateUpdate()
    {
        if (playerData.IsMine)
        {
            cam.transform.position = viewPoint.position;
            cam.transform.rotation = viewPoint.rotation;
        }
    }
}
