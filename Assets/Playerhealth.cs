using UnityEngine;

public class Playerhealth : MonoBehaviour
{
    public float maxhealth; 
    private float currenthealth;
    void Start()
    {
        currenthealth = maxhealth; 
    }

    public void ChangeHealth(float damge)
    {
        currenthealth -= damge;
    }
  
    void Update()
    {
        
    }
}
