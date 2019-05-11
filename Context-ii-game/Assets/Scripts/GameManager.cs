using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool change = true;
    public int gamestage = 1;

    public GameObject area2Block, area3Block;
    public GameObject podArea2, podArea3;
    Trees podArea2Script, podArea3Script;

    public GameObject spawner;
    SpawningManager spawnerScript;

    public GameObject musicControl;
    MusicController music;
    public float state2SpawnTime, state3SpawnTime;

    public GameObject boss;
    public Transform spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        podArea2Script = podArea2.GetComponent<Trees>();
        podArea3Script = podArea3.GetComponent<Trees>();
        spawnerScript = spawner.GetComponent<SpawningManager>();
        music = musicControl.GetComponent<MusicController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gamestage == 2 && change == true)
        {
            area2Block.SetActive(false);
            podArea2Script.repairable = false;
            podArea2Script.alive = true;
            spawnerScript.spawntime = state2SpawnTime;
            music.SwitchTrack(1);
            change = false;
        }
        else if (gamestage == 3 && change == true)
        {
            area3Block.SetActive(false);
            podArea3Script.repairable = false;
            podArea3Script.alive = true;
            Instantiate(boss, spawnLocation.position, Quaternion.identity);
            spawnerScript.spawntime = state3SpawnTime;
            music.SwitchTrack(2);
            change = false;
        }
    }
}
