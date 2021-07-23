using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviourPunCallbacks
{
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
    private float muzzleCounter;
    public float maxHeat = 10f, coolRate = 4f, overheatCoolRate = 5f;
    private float heatCounter;
    public Gun[] allGuns;
    private int selectedGun;
    public GameObject playerHitImpact;
    public int maxHealth = 100;
    private int currentHealth;
    public Animator animator;
    public GameObject playerModel;
    public Transform modelGunPoint, gunHolder;
    public GameObject gun;
    public GameObject gunPointer;
    public PlayerData playerData;

    [HideInInspector] public UIController canvasController;
    [HideInInspector] public GameObject homeControllerObject;

    private GameObject deliveryPackage;

    private void Awake()
    {
        playerData = new PlayerData(photonView);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        cam = Camera.main;

        UIController.instance.weaponTempSlider.maxValue = maxHeat;

        currentHealth = maxHealth;

        if (playerData.IsMine)
        {
            UIController.instance.healthSlider.maxValue = maxHealth;
            UIController.instance.healthSlider.value = currentHealth;
        }
    }

    void Update()
    {
        if (playerData.IsMine)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + horizontal, transform.rotation.eulerAngles.z);

            gun.transform.rotation = Quaternion.Euler(gun.transform.rotation.eulerAngles.x, gun.transform.rotation.eulerAngles.y + Input.GetAxisRaw("Mouse X"), gun.transform.rotation.eulerAngles.z);

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

            if (allGuns[selectedGun].muzzleFlash.activeInHierarchy)
            {
                muzzleCounter -= Time.deltaTime;

                if (muzzleCounter <= 0)
                {
                    allGuns[selectedGun].muzzleFlash.SetActive(false);
                }
            }

            if (heatCounter < 0)
            {
                heatCounter = 0;
            }
            UIController.instance.weaponTempSlider.value = heatCounter;

            if (Input.GetMouseButtonDown(0))
            {
                if (deliveryPackage)
                {
                    deliveryPackage.GetComponent<DeliveryPackage>().ThrowPackage();
                    deliveryPackage = null;
                } else
                {
                    PickupPackage();
                }
            }
        }

        animator.SetBool("grounded", isGrounded);
        animator.SetFloat("speed", moveDir.magnitude);

        //if (Input.GetKeyDown(KeyCode.H))
        //{
            //Debug.Log("Home button clicked");
            //Debug.Log(GameObject.Find("Environment"));
            //GameObject.Find("Environment").SetActive(false);
            //homeControllerObject.SetActive(true);
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        } else if (Cursor.lockState == CursorLockMode.None)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    private void PickupPackage()
    {
        if (!deliveryPackage)
        {
            GameObject package = PackageService.instance.GetNearestPackage(gameObject);

            if (package)
            {
                deliveryPackage = package;
                deliveryPackage.GetComponent<DeliveryPackage>().PickupPackage(gameObject);
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
