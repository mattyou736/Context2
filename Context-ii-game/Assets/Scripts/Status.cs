using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public float oxigenStat;
    public int converts;
    public int gunlvl;
    public int lives;
    public int powerupsPickedup;

    PlayerFlags playerStats;
    UiManager uiMan;

    private static bool statsExists;

    // Use this for initialization
    void Start()
    {
        if (!statsExists)
        {
            statsExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
            Destroy(gameObject);

    }


    public void GrabStatus()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFlags>();
        uiMan = GameObject.FindGameObjectWithTag("UI").GetComponent<UiManager>();

        converts = playerStats.protectorsTotal;
        lives = playerStats.lives;
        gunlvl = playerStats.gunUpgradeLvl;
        powerupsPickedup = playerStats.powerupsPickedup;
        oxigenStat = uiMan.oxigen /10;
    }
}
