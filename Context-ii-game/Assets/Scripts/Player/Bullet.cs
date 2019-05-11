using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] private float speed;
    private Rigidbody RigidBullet;

    [Header("Bullet Speeds")]
    public int normalSpeed;

    [Header("Particle on collision")]
    public GameObject hitParticle;

    [HideInInspector] public bool heavy;
    [HideInInspector] public bool p1Owner;
    // Use this for initialization
    void Start ()
    {

        speed = normalSpeed;

        RigidBullet = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        RigidBullet.velocity = transform.forward * speed;
        
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Enemy")
         {
           other.GetComponent<Enemy>().enemyHp -= 10;

            //Instantiate(hitParticle, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);             
        }

        if (other.tag == "Boss")
        {
            other.GetComponent<Boss>().enemyHp -= 5;

            //Instantiate(hitParticle, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

        

        if (other.tag == "Walls")
        {
            Destroy(gameObject);
        }
    }
}
