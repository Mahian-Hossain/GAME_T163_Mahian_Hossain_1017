using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public Color tint;
    public Vector2 direction;
    public float rotation;
    [SerializeField] float minRotation;
    [SerializeField] float maxRotation;
    [SerializeField] float tintAmount;

    // Create a private but Serialized reference to AsteroidData called data.
   // [SerializeField] AsteroidData data;

    private GameObject child;
    private int currentPhase;

    void Start()
    {
        currentPhase = 0;

        // Fill in for the Week 5 lab from the solution. Sets the data based on the ScriptableObject reference.
      //  data = Instantiate(data); // Clone the ScriptableObject to avoid modifying the original asset.
        direction = Random.insideUnitCircle.normalized;
        rotation = Random.Range(minRotation, maxRotation) * RandomSign();
        tint = new Color(Random.value * tintAmount, Random.value * tintAmount, Random.value * tintAmount);

      // Spawn child object based on the current phase.
      //  SpawnChildAsteroid();

        // Set the asteroid's color.
        GetComponent<SpriteRenderer>().color = tint;
    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime);
        //child.transform.Rotate(0f, 0f, rotation * Time.deltaTime);

        // Wrap asteroid to the other side of the screen.
        if (transform.position.x < -8.5f)
        {
            transform.position = new Vector2(8.5f, transform.position.y);
        }
        else if (transform.position.x > 8.5f)
        {
            transform.position = new Vector2(-8.5f, transform.position.y);
        }
        if (transform.position.y < -6.5f)
        {
            transform.position = new Vector2(transform.position.x, 6.5f);
        }
        else if (transform.position.y > 6.5f)
        {
            transform.position = new Vector2(transform.position.x, -6.5f);
        }
    }

    private int RandomSign()
    {
        return 1 - 2 * Random.Range(0, 2);
    }

    
}
