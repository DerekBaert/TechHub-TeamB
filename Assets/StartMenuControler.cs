using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public FadeAnim FadeWhenClicked;

    public void OnStartClick()
    {
        FadeWhenClicked.FadeOut();
        StartCoroutine(StartGame());
    }

    public void OnTutorialClick()
    {
        FadeWhenClicked.FadeOut();
        StartCoroutine(StartTutorial());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Selector");
    }

    IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Tutorial Scene");
    }
}
