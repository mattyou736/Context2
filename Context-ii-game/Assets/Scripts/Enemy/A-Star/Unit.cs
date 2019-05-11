using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;

    public Transform target;
    public float speed = 5;
    public float turnSpeed = 3;
    public float turnDst = 1;
    public float stoppingDst = 10;

    Path path;

    public float enemyHp;

    public GameObject playerPos;
    public GameObject powerUp, battery;

    private Rigidbody myRigid;

    public bool attacker, worker, boss;

    public float knockBackForce;
    public float closeDistance = 50;

    public Transform goRest;

    void Start()
    {
        StartCoroutine(UpdatePath());
        myRigid = GetComponent<Rigidbody>();
        goRest = GameObject.FindGameObjectWithTag("Restpoint").transform;
    }

    private void Update()
    {
        if (enemyHp <= 0)
        {
            Die();
        }
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = new Path(waypoints, transform.position, turnDst, stoppingDst);

            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator UpdatePath()
    {
        if (Time.timeSinceLevelLoad < .3f)
        {
            yield return new WaitForSeconds(.3f);
        }
        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target.position;

        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
            print(((target.position - targetPosOld).sqrMagnitude) + "    " + sqrMoveThreshold);
            if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
            {
                PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
                targetPosOld = target.position;
            }
        }
    }

    IEnumerator FollowPath()
    {

        bool followingPath = true;
        int pathIndex = 0;
        transform.LookAt(path.lookPoints[0]);

        float speedPercent = 1;

        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
            while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if (pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                    break;
                }
                else
                {
                    pathIndex++;
                }
            }

            if (followingPath)
            {

                if (pathIndex >= path.slowDownIndex && stoppingDst > 0)
                {
                    speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst);
                    if (speedPercent < 0.01f)
                    {
                        followingPath = false;
                    }
                }

                Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                transform.Translate(Vector3.forward * Time.deltaTime * speed , Space.Self);
            }

            yield return null;

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
        int randomNumber = Random.Range(0, 10);

        if (randomNumber <= 3)
        {
            Instantiate(powerUp, transform.position, transform.rotation);
            if (attacker)
            {
                Instantiate(battery, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), transform.rotation);
            }

        }

        Destroy(gameObject);
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            path.DrawWithGizmos();
        }
    }
}
