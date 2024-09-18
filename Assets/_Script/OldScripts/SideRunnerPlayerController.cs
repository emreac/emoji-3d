using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideRunnerPlayerController : MonoBehaviour
{
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;
    //To play clip > necessary!
    [SerializeField] private AudioSource playerAudio;
   

    private Rigidbody rb;
    [SerializeField] private float jumpForce = 10;
    public float gravityModifier;
    public bool isGrounded = true;
    public Animator animator;

    public bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !gameOver)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            animator.SetBool("Jump_b",true);
            playerAudio.PlayOneShot(jumpSound,1.0f);
            
        }
        else
        {
            animator.SetBool("Jump_b", false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
    
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }else if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerAudio.PlayOneShot(crashSound, 1.0f);
            animator.SetBool("Death_b",true) ;
            animator.SetInteger("DeathType_int", 1);
            gameOver = true;
            Debug.Log("GameOver");
        }
    }
}
