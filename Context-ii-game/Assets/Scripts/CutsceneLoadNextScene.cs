using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneLoadNextScene : MonoBehaviour
{

    public void OneshotLoadGame()
    {
        SceneManager.LoadScene("testing-ground", LoadSceneMode.Single);
    }

    public void LoadScene(string level)
    {
        Application.LoadLevel(level);
    }
}

