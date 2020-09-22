/*
* Chris Smith
* Prototype 3
* Controls player actions
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float jumpForce;
    public ForceMode jumpForceMode;
    public float gravityModifier;
    private Animator playerAnimator;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    public bool isOnGround = true;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        //set references to components
        rb = GetComponent<Rigidbody>();
        jumpForceMode = ForceMode.Impulse;
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        //start running on start
        playerAnimator.SetFloat("Speed_f", 1.0f);

        //modify gravity
        if (Physics.gravity.y > -10)
        {
            Physics.gravity *= gravityModifier;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //press space to jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerAnimator.SetTrigger("Jump_trig");
            rb.AddForce(Vector3.up * jumpForce, jumpForceMode);
            isOnGround = false;
            //stop playing dirt particle
            dirtParticle.Stop();
            //play jump sound
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            //play dirt particle
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            gameOver = true;
            //Play death animation
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);
            //Play explosion particle
            explosionParticle.Play();
            //play crash sound
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
