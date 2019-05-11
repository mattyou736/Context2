using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{

	public float currentSpeed;
    public float normalSpeed;
    public float poweredUpSpeed;

	//rigidbody
	private Rigidbody RigidPlayer;

    private PlayerFlags playerStats;

	//movement check
	private bool Moving, shooting;
	public Vector2 lastMove;

	private static bool playerExists;

    public bool canMove;

    public Camera mainCam;

    public GameObject stunPartic;

    public bool canShoot;
    public GameObject bulletPrefab;
    public Transform firePoint , firePoint2, firePoint3, firePoint4;
    [Header("Time in sec")]
    public float setCooldown;
    [HideInInspector] public bool fire;
    private float cooldown = 0;

    public Animator anim;

    private void Awake()
    {
        playerStats = GetComponent<PlayerFlags>();
        cooldown = setCooldown;
    }

    // Use this for initialization
    void Start () 
	{
		//anim = GetComponent<Animator> ();
		RigidPlayer = GetComponent<Rigidbody>();
        canMove = true;

		if (!playerExists) 
		{
			playerExists = true;
			
		} 
		else 
		{
			Destroy(gameObject);
		}

	}

    // Update is called once per frame
    void Update () 
	{
        
        

        if (!canMove)
        {
            RigidPlayer.velocity = Vector3.zero;
            stunPartic.SetActive(true);
            StartCoroutine(ResetMovement());
            return;
        }

        //aiming
        Ray camRay = mainCam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if(groundPlane.Raycast(camRay, out rayLength))
        {
            Vector3 pointToLook = camRay.GetPoint(rayLength);
            Debug.DrawLine(camRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x,transform.position.y,pointToLook.z));
        }

		//movements

		//left right
		if (Input.GetAxisRaw ("Horizontal") > 0.5f|| Input.GetAxisRaw ("Horizontal") < -0.5f) 
		{
			//transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal")* moveSpeed * Time.deltaTime , 0, 0));
			RigidPlayer.velocity = new Vector3(Input.GetAxisRaw("Horizontal")* currentSpeed, RigidPlayer.velocity.y, RigidPlayer.velocity.z );
			Moving = true;
			//lastMove = new Vector2(Input.GetAxisRaw ("Horizontal"),0f);
		}

		//up down
		if (Input.GetAxisRaw ("Vertical") > 0.5f || Input.GetAxisRaw ("Vertical") < -0.5f) 
		{
			//transform.Translate(new Vector3(0, Input.GetAxisRaw("Vertical")* moveSpeed * Time.deltaTime , 0));
			RigidPlayer.velocity = new Vector3(RigidPlayer.velocity.x, 0, Input.GetAxisRaw("Vertical")* currentSpeed);
			Moving = true;
			//lastMove = new Vector2(0f,Input.GetAxisRaw ("Vertical"));
		}

		//stp moving
		if (Input.GetAxisRaw ("Horizontal") < 0.5f && Input.GetAxisRaw ("Horizontal") > -0.5f) 
		{
			RigidPlayer.velocity = new Vector3(0f, RigidPlayer.velocity.y, RigidPlayer.velocity.z);

        }
		if (Input.GetAxisRaw ("Vertical") < 0.5f && Input.GetAxisRaw ("Vertical") > -0.5f) 
		{
			RigidPlayer.velocity = new Vector3(RigidPlayer.velocity.x ,RigidPlayer.velocity.y, 0f);
            
        }

        if (Input.GetMouseButton(0) && playerStats.gunHeat < 100 )
        {
            Shoot();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

            anim.SetBool("moving", Moving);
        anim.SetBool("shooting", shooting);
        Moving = false;
        shooting = false;
    }

    void Shoot()
    {
        cooldown -= Time.deltaTime;
        shooting = true;
        if (cooldown <= 0)
        {
            if(playerStats.gunUpgradeLvl == 1)
            {
                cooldown = setCooldown;
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                playerStats.gunHeat += 5;
            }
            else if (playerStats.gunUpgradeLvl == 2)
            {
                cooldown = setCooldown;
                Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                playerStats.gunHeat += 5;
            }
            else if (playerStats.gunUpgradeLvl == 3)
            {
                cooldown = setCooldown;
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Instantiate(bulletPrefab, firePoint4.position, firePoint4.rotation);
                Instantiate(bulletPrefab, firePoint3.position, firePoint3.rotation);
                playerStats.gunHeat += 5;
            }
            else if (playerStats.gunUpgradeLvl == 4)
            {
                cooldown = setCooldown;
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
                Instantiate(bulletPrefab, firePoint3.position, firePoint3.rotation);
                Instantiate(bulletPrefab, firePoint4.position, firePoint4.rotation);
                playerStats.gunHeat += 5;
            }
        }
    }

    IEnumerator ResetMovement()
    {
        yield return new WaitForSeconds(3);
        canMove = true;
        stunPartic.SetActive(false);
    }

    

}
