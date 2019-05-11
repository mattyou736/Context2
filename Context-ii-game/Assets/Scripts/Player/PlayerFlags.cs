using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerFlags : MonoBehaviour
{
    public float playerHP;
    public int lives;
    public float gunHeat;
    public bool gunOverHeat;
    public float waitTime;

    public int battery;
    public int gunUpgradeLvl;

    public Transform spawnPos;

    public bool poweredUp;
    public bool invincible;
    public float poweredUpCountdownTime;
    public int powerupsPickedup;

    public Image healthImage;

    public int protectors, protectorsTotal;

    public GameObject gun1, gun2, gun3, gun4;

    MusicController muCon;
    UiManager uiMan;

    private void Start()
    {
        muCon = GetComponentInChildren<MusicController>();
        uiMan = GameObject.FindGameObjectWithTag("UI").GetComponent<UiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        healthImage.fillAmount = playerHP / 100;

        if(poweredUp)
        {
            gunHeat = 0;
        }

        if(gunHeat >= 100 && gunOverHeat == false)
        {
            StartCoroutine(Reset());
        }

        if(playerHP <= 0)
        {
            Respawn();
        }

        if (battery >= 4)
        {
            battery = 3;
        }
    }

    private void Respawn()
    {
        playerHP = 100;
        gunHeat = 0;
        lives -= 1;
        transform.position = spawnPos.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PowerUp")
        {
            poweredUp = true;
            powerupsPickedup += 1;
            if (other.GetComponent<PowerUp>().powerUpID == 1)
            {
                StartCoroutine(PowerUpInvincible());
            }
            else if (other.GetComponent<PowerUp>().powerUpID == 2)
            {
                StartCoroutine(PowerUpSpeed());
            }
            else if (other.GetComponent<PowerUp>().powerUpID == 3)
            {
                StartCoroutine(PowerUpPlantShield());
            }

            Destroy(other.gameObject);
        }

        if (other.tag == "Battery")
        {
            muCon.SwitchTrack(1);
            battery += 1;
            Destroy(other.gameObject);
        }

        if (other.tag == "RatPickUp")
        {
            protectors += 1;
            protectorsTotal += 1;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Locker" && battery >= 3)
        {
            uiMan.interactEGun.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                muCon.SwitchTrack(1);
                battery -= 3;
                UpgradeGun();
            }
        }
        else
        {
            uiMan.interactEGun.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Locker")
        {
            uiMan.interactEGun.SetActive(false);
        }
    }

    private void UpgradeGun()
    {
        muCon.SwitchTrack(0);
        if (gunUpgradeLvl <= 4)
        {
            gunUpgradeLvl += 1;
        }

        if (gunUpgradeLvl == 1)
        {
            gun1.SetActive(false);
            gun2.SetActive(true);
        }
        else if(gunUpgradeLvl == 2)
        {
            gun2.SetActive(false);
            gun3.SetActive(true);
        }
        else if (gunUpgradeLvl == 4)
        {
            gun3.SetActive(false);
            gun4.SetActive(true);
        }

    }

    IEnumerator Reset()
    {
        gunOverHeat = true;
        yield return new WaitForSeconds(waitTime);
        gunHeat = 0;
        gunOverHeat = false;
    }

    IEnumerator PowerUpInvincible()
    {
        muCon.SwitchTrack(2);
        invincible = true;
        yield return new WaitForSeconds(poweredUpCountdownTime);
        poweredUp = false;
        invincible = false;
    }

    IEnumerator PowerUpSpeed()
    {
        muCon.SwitchTrack(2);
        GetComponent<PlayerMovement>().currentSpeed = GetComponent<PlayerMovement>().poweredUpSpeed;
        yield return new WaitForSeconds(poweredUpCountdownTime);
        GetComponent<PlayerMovement>().currentSpeed = GetComponent<PlayerMovement>().normalSpeed;
        poweredUp = false;
    }

    IEnumerator PowerUpPlantShield()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Plant");
        foreach (GameObject go in gos)
        {
            go.GetComponent<Trees>().repairable = false;
            go.GetComponent<Trees>().treeHP = 101;
            go.GetComponent<Trees>().shielded = true;
            go.GetComponent<Trees>().ResetState();
        }
        yield return new WaitForSeconds(poweredUpCountdownTime);
        GetComponent<PlayerMovement>().currentSpeed = GetComponent<PlayerMovement>().normalSpeed;
        poweredUp = false;
    }
}
