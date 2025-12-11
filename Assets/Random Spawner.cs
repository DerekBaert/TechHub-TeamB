using System.Collections;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{

    public GameObject itemPrefab;
    public float Radius = 1;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            StartCoroutine(SpawnFlakes());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.paleGreen;

        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }
    IEnumerator SpawnFlakes()
    {
        yield return new WaitForSeconds(5f);
        SpawnObjectAtRadom();
        {
        }
        void SpawnObjectAtRadom() { }
        Vector3 randomPos = Random.insideUnitCircle * Radius;


        Instantiate(itemPrefab, randomPos, Quaternion.identity);
        {

        }
        ;
    }
}

