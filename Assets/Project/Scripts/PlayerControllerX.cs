using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;

    public AudioClip explodeSound;
    public AudioClip bounceSound;

    public float ceiling = 14f;
    public float bottomLimit = 4f; 

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }

        // Restrict the player's Y position to stay above the bottom limit
        if (transform.position.y < bottomLimit)
        {
            transform.position = new Vector3(transform.position.x, bottomLimit, transform.position.z);
            playerRb.velocity = Vector3.zero;  // Stop vertical movement
        }

        // Prevent player from going higher than the ceiling
        if (transform.position.y > ceiling)
        {
            transform.position = new Vector3(transform.position.x, ceiling, transform.position.z);
            playerRb.velocity = Vector3.zero;  // Stop upward movement
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
            SceneManager.LoadScene("Game Over");
            
        } 


        // if player collides with Ground, bounce
        else if (other.gameObject.CompareTag("Ground"))
        {
            playerAudio.PlayOneShot(bounceSound, 1.0f);
            playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }
}
