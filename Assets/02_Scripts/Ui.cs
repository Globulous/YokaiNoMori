using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ui : MonoBehaviour
{
    public void exit()
    {
        Application.Quit();
    }

    public void Replay()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
