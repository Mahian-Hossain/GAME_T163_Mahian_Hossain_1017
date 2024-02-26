using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private float moveSpeed = 10f;
    private AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.PlayOneShot(aud.clip);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        // Method 1, world space check.
        if (transform.position.x < -7f) // When getting to a certain point, destroy bullet.
        {
            Destroy(gameObject);
        }
        // Method 2, screen space check.
        //Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        //if (screenPosition.x > (Screen.width + 10))
        //{
        //    Destroy(gameObject);
        //}
    }
}
