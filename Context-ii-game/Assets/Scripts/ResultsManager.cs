using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultsManager : MonoBehaviour
{
    public string lvlToStart;
    Status statsScript;

    public Text oxigenStatText;
    public Text convertsText;
    public Text gunlvlText;
    public Text livesText;
    public Text powerupsPickedupText;

    public GameObject goodPlanet, badPlanet;

    // Start is called before the first frame update
    void Start()
    {
        statsScript = GameObject.FindGameObjectWithTag("Stats").GetComponent<Status>();
        oxigenStatText.text = statsScript.oxigenStat.ToString();
        convertsText.text = statsScript.converts.ToString();
        gunlvlText.text = statsScript.gunlvl.ToString();
        livesText.text = statsScript.lives.ToString();
        powerupsPickedupText.text = statsScript.powerupsPickedup.ToString();

        if(statsScript.oxigenStat <= 30)
        {
            badPlanet.SetActive(true);
        }
        else
        {
            goodPlanet.SetActive(true);
        }
    }

    public void Retry()
    {
        Destroy(GameObject.FindGameObjectWithTag("Stats"));
        SceneManager.LoadScene(lvlToStart);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
