using UnityEngine;

public class RandomSpawner : MonoBehaviour
{ 

    public GameObject itemPrefab;
    public float Radius = 1;
 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) SpawnObjectAtRadom();
        {
        }
        void SpawnObjectAtRadom() { }
            Vector3 randomPos = Random.insideUnitCircle * Radius;


        Instantiate(itemPrefab, randomPos, Quaternion.identity);
        {

        }
        ;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.paleGreen;

        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }

}

