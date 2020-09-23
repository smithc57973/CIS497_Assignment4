﻿/*
* Chris Smith
* Challenge 3
* Controls player movement and collisions
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;
    private bool lowEnough;
    public float maxHeight;
    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;
    private UIManager uiManager;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bounceSound;


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

        lowEnough = true;
        uiManager = GameObject.FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver && lowEnough)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }

        if (GameObject.FindGameObjectWithTag("Player").transform.position.y < maxHeight)
        {
            lowEnough = true;
        }
        else
        {
            lowEnough = false;
        }

        if (transform.position.y >= 15)
        {
            transform.position = new Vector3(transform.position.x, 15f, transform.position.z);
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
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            uiManager.score++;
            Destroy(other.gameObject);

        }

        //if player collides with ground, bounce
        if (other.gameObject.CompareTag("Ground"))
        {
            playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            playerAudio.PlayOneShot(bounceSound, 1.0f);
        }

    }

}
