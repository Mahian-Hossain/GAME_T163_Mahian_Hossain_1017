using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject[] hitSprites;
    [SerializeField] GameObject splosion;
    [SerializeField] GameObject sprite;
    [SerializeField] AudioClip[] clips;
    private AudioSource aud;
    private int score = 0;
    private int hits = 3;
    private bool isAlive = true;
    
    void Start()
    {
        scoreText.text = "SCORE: 0";
        aud = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (!isAlive) return;
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(horizontal, vertical, 0f);
        // Keep the player within bounds.
        float clampedX = Mathf.Clamp(transform.position.x, -6f, 6f); // (Val, Min, Max)
        float clampedY = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        transform.position = new Vector3(clampedX, clampedY, 0f);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletInst = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bulletInst.GetComponent<PlayerBulletScript>().pm = this; // this is PlayerMovement script instance.
            //aud.clip = clips[0]; // Load a clip.
            //aud.Play();
            aud.PlayOneShot(clips[0]); // Allows layering of the same SFX.
        }
    }

    public void AddKill(int points = 100)
    {
        score += points;
        scoreText.text = "SCORE: " + score.ToString();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isAlive) return;
        if (collider.gameObject.tag == "EnemyBullet")
        {
            Destroy(collider.gameObject);
            //aud.clip = clips[1]; // Load a clip.
            //aud.Play();
            aud.PlayOneShot(clips[1]);
            if (hits == -1)
            {
                GameObject splosionInst = Instantiate(splosion, transform.position, Quaternion.identity);
                Destroy(splosionInst, 0.5f);
                isAlive = false;
                Invoke("GameOver", 1.5f);
                sprite.SetActive(false);
                return;
            }
            hitSprites[hits--].SetActive(false);
        }
        else if (collider.gameObject.tag == "Asteroid" || collider.gameObject.tag == "Enemy")
        {
            if (hits == -1)
            {
                GameObject splosionInst = Instantiate(splosion, transform.position, Quaternion.identity);
                Destroy(splosionInst, 0.5f);
                isAlive = false;
                Invoke("GameOver", 1.5f);
                sprite.SetActive(false);
                return;
            }
            else
            {
                AddKill(1000);
                GameObject splosionInst = Instantiate(splosion, collider.transform.position, Quaternion.identity);
                Destroy(splosionInst, 0.5f);
                collider.transform.position = new Vector3(-8f, 0, 0);
            }
            hitSprites[hits--].SetActive(false);
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene(2); 
    }
}
