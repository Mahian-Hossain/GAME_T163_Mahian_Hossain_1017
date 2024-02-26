using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnScript : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnInterval;

    [SerializeField] List<GameObject> activeEnemies;
    [SerializeField] List<GameObject> enemyPool;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    private void Update()
    {
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            if (activeEnemies[i].transform.position.x <= -7.5f) // if the enemy is off-screen.
            {
                // Deactivate the enemy's script.
                activeEnemies[i].GetComponent<AsteroidScript>().enabled = false;
                // Add the inactive enemy to the pool.
                enemyPool.Add(activeEnemies[i]);
                // Remove the enemy reference from the active list.
                activeEnemies.RemoveAt(i);
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemyPool.Count > 0) // Are there enemies in the pool?
        {
			// Create a temporary reference to the first enemy in the pool.
            GameObject newEnemy = enemyPool[0];
			// Set the new enemy's position to a proper random spawn position.
            newEnemy.transform.position = new Vector3(7.5f, Random.Range(-4.5f, 4.5f), 0f);
			// Enable the new enemy's behaviour script.
            newEnemy.GetComponent<AsteroidScript>().enabled = true;
            newEnemy.GetComponent<AsteroidScript>().moveSpeed = Random.Range(-3f, -1f);
            // Remove the enemy reference from the enemyPool.
            enemyPool.RemoveAt(0);
			// Add the enemy reference to the activeEnemies list.
            activeEnemies.Add(newEnemy);
        }
        else // There are no enemies waiting in the pool.
        {
			// Create the new enemy in memory.
            GameObject newEnemy = Instantiate(enemyPrefab);
			// Move the new enemy to a random position.
            newEnemy.transform.position = new Vector3(7.5f, Random.Range(-4.5f, 4.5f), 0f);
            newEnemy.GetComponent<AsteroidScript>().moveSpeed = Random.Range(-3f, -1f);
            // Add this brand new enemy to the activeEnemes list.
            activeEnemies.Add(newEnemy);
        }
    }
}