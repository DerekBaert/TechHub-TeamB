using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MyTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timertext;
        float elapsedTime;
    
    void Update()
    {
        elapsedTime += Time.deltaTime;
        int Minutes = Mathf.FloorToInt(elapsedTime / 60);
        int Seconds = Mathf.FloorToInt(elapsedTime % 60);
        timertext.text = string.Format("{0:00}:{1:00}", Minutes, Seconds);
    }
}
