using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public string lvlToStart;

    private float time = 300;
    public float heat;
    public float oxigen;

    public Text timeText, batteryText,convertsText;

    private PlayerFlags player;

    public GameObject live1, live2, live3;

    public Image shipHealthImage, planetImage;
    public Image gunHeat;
    public GameObject interactQ, interactE, interactEGun;

    GameManager gameMan;
    Status endStats;

    //FMOD.Studio.EventInstance sound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFlags>();
        endStats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Status>();
        gameMan = GetComponent<GameManager>();
        StartCoroutine(GameStateChange());
    }

    // Update is called once per frame
    void Update()
    {
        shipHealthImage.fillAmount = oxigen / 1000;
        planetImage.fillAmount = oxigen / 1000;
        string timemin = ((int)time / 60).ToString();

        string timeSeconds = ((int)time % 60).ToString();

        time -= Time.deltaTime;

        timeText.text = timemin + ":" + timeSeconds;

        gunHeat.fillAmount = player.gunHeat / 100;


        batteryText.text = "x " + player.battery.ToString();
        convertsText.text = "x " + player.protectors.ToString();


        if (player.lives == 2)
        {
            live3.SetActive(false);
        }
        else if (player.lives == 1)
        {
            live2.SetActive(false);
        }
        else if (player.lives == 0)
        {
            live1.SetActive(false);
            SceneManager.LoadScene(lvlToStart);
        }

        

        if (time <= 0 || oxigen <= 0)
        {
            endStats.GrabStatus();
            SceneManager.LoadScene(lvlToStart);
        }
    }

    IEnumerator GameStateChange()
    {
        print(time);
        yield return new WaitForSeconds(150);
        gameMan.gamestage = 2;
        gameMan.change = true;
        yield return new WaitForSeconds(60);
        gameMan.gamestage = 3;
        gameMan.change = true;
    }
}
