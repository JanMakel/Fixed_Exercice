using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver = false;

    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;
    public float limit = 14.5f;
    public int money;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
        {
            floating();
        }
        if (transform.position.y > limit)
        {
            playerRb.velocity = new Vector3(0f, 0f, 0f);
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gamesOver();
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 
        
        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
            money++;
        }
        else if (other.collider.gameObject.CompareTag("Ground"))
        {
            gamesOver();
            Debug.Log("Game Over!");
        }

    }

    private void floating()
    {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
    }
    private void gamesOver()
    {
        gameOver = true;
    }

}
