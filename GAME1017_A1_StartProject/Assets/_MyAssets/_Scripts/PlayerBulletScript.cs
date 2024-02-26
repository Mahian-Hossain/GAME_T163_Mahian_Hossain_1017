using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBulletScript : MonoBehaviour
{
    [SerializeField] GameObject splosion;
    public PlayerMovement pm; // script on player.
    private float moveSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource aud = GetComponent<AudioSource>();
        aud.PlayOneShot(aud.clip);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        // Method 1, world space check.
        if (transform.position.x > 7f) // When getting to a certain point, destroy bullet.
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            pm.AddKill();
            Destroy(this.gameObject); // This destroys bullet.
            GameObject splosionInst = Instantiate(splosion, other.transform.position, Quaternion.identity);
            Destroy(splosionInst, 0.5f);
            other.transform.position = new Vector3(-8f, 0, 0);

        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        pm.AddKill();
    //        Destroy(this.gameObject); // This destroys bullet.
    //        GameObject splosionInst = Instantiate(splosion, collision.transform.position, Quaternion.identity);
    //        Destroy(splosionInst, 0.5f);
    //        collision.transform.position = new Vector3(-8f, 0, 0);

    //    }
    //}
}
