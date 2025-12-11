using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Startgame : MonoBehaviour
{
   public void Onstartgameclick()
    {
        SceneManager.LoadScene("Puffer Fish");
    }

public void OnGoldFishclick()
    {
        SceneManager.LoadScene("Gold Fish");
    }

    public void OnStarFishclick()
    {
        SceneManager.LoadScene("Star Fish");
    }

}
