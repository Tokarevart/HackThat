using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20f;
    public float fireRate = 10f;
    public GameObject bolt;

    private Vector3 movement;
    private Rigidbody playerRb;
    private AudioSource shotSound;
    private int floorMask;
    private float camRayLength = 200f;
    private float fireDTime;
    private float nextFireTime = 0f;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        playerRb = GetComponent<Rigidbody>();
        shotSound = GameObject.FindGameObjectWithTag("ShotSound").GetComponent<AudioSource>();
        fireDTime = 1f / fireRate;
    }

    void Update()
    {
        if (Time.time > nextFireTime && Input.GetKey(KeyCode.Mouse0))
        {
            Fire();

            nextFireTime = Time.time + fireDTime;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
    }

    void Move(float h, float v)
    {
        Vector3 inputMovement = new Vector3(h, 0f, v);
        Quaternion quat = new Quaternion(inputMovement.x, inputMovement.y, inputMovement.z, 0);

        quat = Camera.main.transform.rotation * quat * Quaternion.Inverse(Camera.main.transform.rotation);
        movement.x = quat.x;
        movement.z = quat.z;
        movement.Normalize();

        playerRb.position += movement * speed * Time.fixedDeltaTime;
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            playerRb.rotation = Quaternion.LookRotation(playerToMouse);
        }
    }

    void Fire()
    {
        //Quaternion quat = Camera.main.transform.rotation * new Quaternion(0f, 0f, 1.5f, 0f) * Quaternion.Inverse(Camera.main.transform.rotation);
        //Vector3 playerToBoltStartPos = new Vector3(quat.x, 0.7f, quat.z);

        Instantiate(bolt, transform.position, transform.rotation);
        shotSound.Play();
    }
}