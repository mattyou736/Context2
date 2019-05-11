using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float enemyHp;

    public GameObject playerPos;
    public GameObject powerUp, battery,convert;

    private Rigidbody myRigid;

    public bool attacker, worker;

    public float knockBackForce;
    public float closeDistance = 50;

    public Transform goRest;

    public GameObject dieparticle;

    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        goRest = GameObject.FindGameObjectWithTag("Restpoint").transform;
        playerPos = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (attacker)
        {
            agent.SetDestination(playerPos.transform.position);
        }

        if (worker)
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Plant");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance && go.GetComponent<Trees>().repairable == false && go.GetComponent<Trees>().alive)
                {
                    closest = go;
                    distance = curDistance;
                    agent.SetDestination(go.transform.position);
                    agent.stoppingDistance = 1.5f;

                }
                else if(curDistance < distance && go.GetComponent<Trees>().repairable == true)
                {
                    agent.SetDestination(goRest.transform.position);
                    agent.stoppingDistance = 0;
                }
            }
        }


        if (enemyHp <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && attacker)
        {
            if (other.GetComponent<PlayerMovement>().canMove == true)
            {
                other.GetComponent<PlayerMovement>().canMove = false;
            }
            if (other.GetComponent<PlayerFlags>().invincible == false)
            {
                other.GetComponent<PlayerFlags>().playerHP -= 5;
            }
            Destroy(this.gameObject);
        }

        if (other.tag == "Restpoint")
        {
            Destroy(this.gameObject);
        }
    }

    void Die()
    {
        Instantiate(dieparticle, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        int randomNumber = Random.Range(0, 10); 

        if(randomNumber <= 3)
        {
            Instantiate(powerUp, transform.position, transform.rotation);
            Instantiate(convert, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), transform.rotation);
            if (attacker)
            {
                Instantiate(battery, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), transform.rotation);
            }

        }

        Destroy(gameObject);
    }
}
