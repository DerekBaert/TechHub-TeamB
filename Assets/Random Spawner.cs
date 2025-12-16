using System.Collections;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] flakeSprites;
    [SerializeField] float secondSpawn = 0.5f;
    [SerializeField] float minTras;
    [SerializeField] float maxTras;

    void Start()
    {
        StartCoroutine(FlakeSpawn());
    }

    IEnumerator FlakeSpawn()
    {
        while (true)
        {
            var wanted = Random.Range(minTras, maxTras);
            var position = new Vector3(wanted, transform.position.x, transform.position.y);
            GameObject gameObject = Instantiate(flakeSprites[Random.Range(0, flakeSprites.Length)],
            position, Quaternion.identity);
            yield return new WaitForSeconds(secondSpawn);
            Destroy(gameObject, 5f);
        }
    }
}