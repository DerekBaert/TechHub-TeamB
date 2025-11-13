using UnityEngine;

public class Playerhealth : MonoBehaviour
{
    public float maxhealth; 
    private float currenthealth;
    void Start()
    {
        currenthealth = maxhealth; 
    }

    public void TakeDamge(float damge)
    {
        currenthealth -= damge;
    }
  
    void Update()
    {
        
    }
}
