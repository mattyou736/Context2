using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    public GameObject[] enemys;

    public Transform[] spawnpoints;

    public float spawntime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnWaves()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawntime);
            Instantiate(enemys[Random.Range(0,2)], spawnpoints[Random.Range(0, 4)].position, Quaternion.identity);
        }
    }
}
