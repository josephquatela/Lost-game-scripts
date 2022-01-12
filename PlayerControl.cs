using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnimator;
    private AudioSource playerSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private ParticleSystem deathExplosion;
    [SerializeField] private ParticleSystem dirtSpray;
    [SerializeField] private int jumpForce;
    [SerializeField] private int gravModifier;

    [SerializeField] private GameObject[] gameOverElements;
    
    public bool isOnGround = true;
    public bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerSource = GetComponent<AudioSource>();
        Physics.gravity *= gravModifier;
    }

    // Update is called once per frame
    void Update()
    {
        jumpControl();
        if (gameOver) {
            foreach (GameObject element in gameOverElements) {
                element.SetActive(true);
            }
        }
    }

    private void jumpControl() {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver) {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnimator.SetTrigger("Jump_trig");
            playerSource.PlayOneShot(jumpSound, 2.0f);
            dirtSpray.Stop();
        }
    }

    private void OnCollisionEnter(Collision other) {
        //Prevent double jump
        if (other.gameObject.CompareTag("Ground")) {
            isOnGround = true;
            dirtSpray.Play();
        } 
        //Game over
        else if (other.gameObject.CompareTag("Obstacle")) {
            gameOver = true;
            playerAnimator.SetBool("Death_b", true);
            deathExplosion.Play();
            playerSource.PlayOneShot(crashSound, 2.0f);
            dirtSpray.Stop();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Prototype 3");
        Physics.gravity = new Vector3(0, -9.8f, 0);
    }
}
