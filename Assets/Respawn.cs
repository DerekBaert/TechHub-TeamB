using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Respawn : MonoBehaviour
{
    public void OnRespawnClick()
    {
        SceneManager.LoadScene("Timer script");
    }

    public void OnExitClick()

    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}