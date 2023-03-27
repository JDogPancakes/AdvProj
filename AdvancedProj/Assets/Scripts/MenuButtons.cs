using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public int startScene;
    public void StartButton()
    {
        SceneManager.LoadScene(startScene);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
