using JetBrains.Annotations;
using UnityEngine;

public class spikes : MonoBehaviour
{
    public float damage = 1f;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Playerhealth>())
        {
            collision.gameObject.GetComponent<Playerhealth>().TakeDamge(damage);
        }
       
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
