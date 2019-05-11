using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public float enemyHp;
    public int waypointNumber;

    NavMeshAgent agent;

    GameObject[] waypoints;
    public Transform[] spawnpoints;

    public GameObject worker, attacker;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {

        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(waypoints[waypointNumber].transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "waypoint")
        {
            print("colide");
            StartCoroutine(BossMachanic());
        }
    }

    IEnumerator BossMachanic()
    {
        Instantiate(attacker, spawnpoints[0].position, Quaternion.identity);
        Instantiate(attacker, spawnpoints[1].position, Quaternion.identity);
        yield return new WaitForSeconds(3);
        Instantiate(worker, spawnpoints[0].position, Quaternion.identity);
        Instantiate(worker, spawnpoints[1].position, Quaternion.identity);
        yield return new WaitForSeconds(6);
        if(waypointNumber == waypoints.Length)
        {
            waypointNumber = 0;
        }
        else
        {
            waypointNumber++;
        }
        
    }

}
