using JetBrains.Annotations;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin, Backtfourth }
    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f; 
    public float speed = 1f;
    public Transform pos1, pos2; //This Is for the BackTForth Spawner
    public Transform StartPos;


    Vector3 nextpos;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float FiringRate = 1f;
    [SerializeField] private float startDelay = 10f; // Added delay before spawning starts

    public GameObject spawnedBullet;
    private float timer = 0f; // Something
    private float delayTimer = 0f; // Timer for tracking the delay

    void Start()
    {
        delayTimer = startDelay;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle the initial delay
        if (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
            return; // Don't process firing logic while delaying
        }

        if (spawnerType == SpawnerType.Spin)
            transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);

        timer += Time.deltaTime;
        if (timer >= FiringRate)
        {
            timer = 0f;
            Fire();
        }
    }

    private void Fire() {

        if (bullet)
        {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<Bullet>().speed = speed;
            spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }
    }
