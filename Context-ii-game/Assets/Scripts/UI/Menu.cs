using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string lvlToStart;
    //public GameObject controlsScreen;

    public void StartGame()
    {
        SceneManager.LoadScene(lvlToStart);
    }

    public void Controls()
    {
        //controlsScreen.SetActive(true);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        //controlsScreen.SetActive(false);
    }
}
