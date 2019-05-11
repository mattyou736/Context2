using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public GameObject pod;
    Trees podScript;

    UiManager uiMan;

    // Start is called before the first frame update
    void Start()
    {
        podScript = pod.GetComponent<Trees>();
        uiMan = GameObject.FindGameObjectWithTag("UI").GetComponent<UiManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && podScript.repairable == true)
        {
            uiMan.interactE.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                podScript.treeHP += 30;
            }
            
        }
        else
        {
            uiMan.interactE.SetActive(false);
        }

        if (other.tag == "Player" && podScript.repairable == false && podScript.protecting == false)
        {
            uiMan.interactQ.SetActive(true);
            if (Input.GetKeyUp(KeyCode.Q) && other.GetComponent<PlayerFlags>().protectors > 0)
            {
                if (podScript.protecting == false)
                {
                    other.GetComponent<PlayerFlags>().protectors -= 1;
                    podScript.protecHP = 20;
                    podScript.protecting = true;
                }
            }
        }
        else
        {
            uiMan.interactQ.SetActive(false);
        }


        if (other.tag == "Enemy" && podScript.repairable == false && podScript.shielded == false)
        {

            if (podScript.protecting == false)
            {
                podScript.treeHP -= 10 * Time.deltaTime;
            }
            else
            {
                podScript.protecHP -= 5 * Time.deltaTime;
            }
            podScript.alarmLight.SetActive(true);
        }
        else
        {

            podScript.alarmLight.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            uiMan.interactE.SetActive(false);
            uiMan.interactQ.SetActive(false);
        }
    }
}
