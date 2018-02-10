using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public GameObject bolt;

    private Transform camTrans;
    private Vector3 movement;
    private Rigidbody playerRb;
    private AudioSource shotSound;
    private ParticleSystem particleSys;
    private int floorMask;
    private float camRayLength = 200f;
    private float fireDeltaTime;
    private float nextFireTime = 0f;

    void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
        camTrans = Camera.main.transform;
        playerRb = GetComponent<Rigidbody>();
        particleSys = GetComponent<ParticleSystem>();
        shotSound = GameObject.FindGameObjectWithTag("ShotSound").GetComponent<AudioSource>();

        fireDeltaTime = 1f / fireRate;
        AnimateDust(0f, 0f);
    }

    void Update()
    {
        if (Time.time < nextFireTime)
            return;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Fire();
            nextFireTime = Time.time + fireDeltaTime;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        AnimateDust(h, v);
    }

    void Move(float h, float v)
    {
        Vector3 inputMovement;

        //
        // Checking if the camera is perpendicular to the floor.
        Vector3 buf = camTrans.rotation.eulerAngles;
        int bufx = Mathf.RoundToInt(buf.x), bufy = Mathf.RoundToInt(buf.y), bufz = Mathf.RoundToInt(buf.z);
        if (bufx % 90 == 0 && bufy % 90 == 0 && bufz % 90 == 0)
            inputMovement = new Vector3(h, v, 0f);
        else
            inputMovement = new Vector3(h, 0f, v);

        movement = TAUnityLib.RotateVector3(inputMovement, camTrans.rotation);
        movement.y = 0f;
        movement.Normalize();

        playerRb.position += movement * speed * Time.fixedDeltaTime;
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - playerRb.position;
            playerToMouse.y = 0f;

            playerRb.rotation = Quaternion.LookRotation(playerToMouse);
        }
    }

    void Fire()
    {
        Instantiate(bolt, playerRb.position, playerRb.rotation);
        shotSound.Play();
    }

    void AnimateDust(float h, float v)
    {
        if (!Mathf.Approximately(h, 0f) || !Mathf.Approximately(v, 0f))
        {
            if (particleSys.isStopped)
                particleSys.Play();
        }
        else
        {
            if (particleSys.isPlaying)
                particleSys.Stop();
        }
    }
}