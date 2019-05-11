using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    public bool repairable;
    public float treeHP;
    public GameObject fixedVer, destroyedVer, evilVer;

    private UiManager uiMan;

    public bool shielded , protecting;
    public float protecHP;
    public float waitTime = 10;

    public GameObject protectorRat;
    public GameObject alarmLight;

    public bool alive;

    private void Awake()
    {
        uiMan = GameObject.FindGameObjectWithTag("UI").GetComponent<UiManager>();
    }

    private void Update()
    {
        if (protecting)
        {
            protectorRat.SetActive(true);
        }
        else
        {
            protectorRat.SetActive(false);
        }

        if (protecHP <= 0)
        {
            protecHP = 0;
            protecting = false;
        }

        if (treeHP >= 101)
        {

            repairable = false;
            fixedVer.SetActive(true);
            destroyedVer.SetActive(false);
            evilVer.SetActive(false);
            treeHP = 100;
        }
        else if (treeHP <= 0 && treeHP >= -99)
        {   
            fixedVer.SetActive(false);
            destroyedVer.SetActive(true);
            evilVer.SetActive(false);
        }
        else if(treeHP <= -100)
        {
            repairable = true;
            destroyedVer.SetActive(false);
            evilVer.SetActive(true);
            treeHP = -100;
        }

        if(alive)
        {
            if (repairable == true)
            {
                uiMan.oxigen -= 2 * Time.deltaTime;
            }
        }
        
    }

    public void ResetState()
    {
        StartCoroutine(Reseting());
    }

    IEnumerator Reseting()
    {
        yield return new WaitForSeconds(waitTime);
        shielded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            treeHP -= 5;
            Destroy(other.gameObject);
        }
    }


}
