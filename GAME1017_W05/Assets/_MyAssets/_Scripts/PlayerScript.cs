using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce;
    [SerializeField] float bulletLifespan;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float rotSpeed;
    [SerializeField] float moveForce;
    [SerializeField] float maxSpeed;
    [SerializeField] float magnitude;
    private Animator an;
    private Rigidbody2D rb;

    void Start()
    {
        an = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Rotate the ship.
        float rotationInput = -Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.forward * rotationInput * rotSpeed * Time.deltaTime);

        // Add forward thrust.
        float thrustInput = Input.GetAxis("Vertical");
        rb.AddForce(transform.up * thrustInput * moveForce);

        // Clamp the ship's speed.
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);

        // Spawn a bullet.
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBullet();
        }

        // Wrap player to the other side of the screen.
        WrapPlayer();

        // Print the magnitude of the ship.
        magnitude = rb.velocity.magnitude;

        // Teleport the ship.
        if (Input.GetKeyDown(KeyCode.T))
        {
            Teleport();
        }
    }

    void FixedUpdate()
    {
        // Clamp the magnitude of the ship.
        magnitude = Mathf.Clamp(magnitude, 0f, maxSpeed);
    }

    private void SpawnBullet()
    {
        // Play the Fire sound.
        if (Game.Instance != null)
            Game.Instance.SOMA.PlaySound("Fire");

        // Instantiate a bullet at the spawn point.
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);

        // Set the bullet's velocity based on the ship's rotation.
        bullet.GetComponent<Rigidbody2D>().velocity = bulletForce * transform.up;

        // Destroy the bullet after a certain time.
        Destroy(bullet, bulletLifespan);
    }

    private void WrapPlayer()
    {
        // Wrap player to the other side of the screen.
        Vector2 newPos = transform.position;

        if (transform.position.x > 7f)
            newPos.x = -7f;
        else if (transform.position.x < -7f)
            newPos.x = 7f;

        if (transform.position.y > 5f)
            newPos.y = -5f;
        else if (transform.position.y < -5f)
            newPos.y = 5f;

        transform.position = newPos;
    }

    private void Teleport()
    {
        // Set the radius for checking a safe teleport location.
        float teleportRadius = 2.0f;

        // Attempt to find a safe teleport location within the radius.
        Vector2 teleportLocation = FindSafeTeleportLocation(teleportRadius);

        // If a safe spot is found, set the position of the ship to the new location.
        if (teleportLocation != Vector2.zero)
        {
            transform.position = teleportLocation;

            // Play the Teleport sound.
            if (Game.Instance != null)
                Game.Instance.SOMA.PlaySound("Teleport");
        }
    }

    private Vector2 FindSafeTeleportLocation(float radius)
    {
        // Set the maximum number of attempts to find a safe location.
        int maxAttempts = 50;

        // Loop to find a safe location within the specified radius.
        for (int i = 0; i < maxAttempts; i++)
        {
            // Generate a random point within the radius.
            Vector2 randomPoint = (Random.insideUnitCircle * radius) + (Vector2)transform.position;

            // Check if the random point is clear of asteroids using OverlapCircle.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(randomPoint, 1.0f);

            // If no colliders are found, it's a safe location.
            if (colliders.Length == 0)
            {
                return randomPoint;
            }
        }

        // Return Vector2.zero if no safe location is found.
        return Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            Destroy(gameObject);
            if (Game.Instance != null)
                Game.Instance.SOMA.PlaySound("Explode");
        }
    }
} 