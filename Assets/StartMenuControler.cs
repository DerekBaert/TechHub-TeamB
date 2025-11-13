using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("Timer script");
    }

    public void OnOptionsClick()
    {
        SceneManager.LoadScene("Options Menu");
    }
}
