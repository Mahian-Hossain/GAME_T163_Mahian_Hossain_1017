using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    private float moveSpeed = -3f;
    private float bulletInterval;
    
    // Start is called before the first frame update
    void Start()
    {
        bulletInterval = Random.Range(0.5f, 2f);
        InvokeRepeating("SpawnBullet", bulletInterval, bulletInterval);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0f, 0f);
    }

    void SpawnBullet()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
