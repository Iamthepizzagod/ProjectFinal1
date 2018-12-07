using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpSpeed;
    public bool isRun;
    public GameObject throwingWeaponOne;
    private bool facingRight;
    private bool playWoosh = true;

    private Rigidbody2D myRigidBody;

    private Animator myAnim;

    public Transform groundCheck;
    public float groundCheckRadius; //Radius of groundcheck
    public LayerMask whatIsGround; //what layer can the player jump
    public bool isGrounded; //Is the player grounded
    public float fireRate;
    private double lastShot = 0.0;

    FMOD.Studio.EventInstance jumpSound;
    FMOD.Studio.EventInstance wooshSound;


    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        jumpSound = FMODUnity.RuntimeManager.CreateInstance("event:/Character/JumpTemp");
        wooshSound = FMODUnity.RuntimeManager.CreateInstance("event:/Character/Woosh");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        myAnim.SetFloat("Walking", Mathf.Abs(myRigidBody.velocity.x));
        myAnim.SetBool("Grounded", isGrounded);

        //Move Right 

        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);
            transform.localScale = new Vector2(1f, 1f);
            facingRight = true;
            //Move Left

        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, myRigidBody.velocity.y);
            transform.localScale = new Vector2(-1f, 1f);
            facingRight = false;
        }
        //No sliding 
        else
        {
            myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpSpeed);
            jumpSound.start();
        }
        else if (Input.GetButtonDown("Jump") && isGrounded && isRun)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x + 10, jumpSpeed);
        }

        //Run
        if (Input.GetButton("Run") && Input.GetAxisRaw("Horizontal") > 0f && isGrounded)
        {
            myRigidBody.velocity = new Vector2(moveSpeed + 10, myRigidBody.velocity.y);
            isRun = true;
        }
        else if (Input.GetButton("Run") && Input.GetAxisRaw("Horizontal") < 0f && isGrounded)
        {
            myRigidBody.velocity = new Vector2(-moveSpeed - 10, myRigidBody.velocity.y);
            isRun = true;
        }
        else
        {
            isRun = false;
        }

        //shoot throwing weapon
        if (Input.GetButton("Shoot"))
        {
            Fire();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "KillPlane")
        {
            SceneManager.LoadScene("1st_level");
        }

        else if (other.tag == "EndLevel")
        {
            SceneManager.LoadScene("EndScene");
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "MovingPlatform")
            transform.parent = other.transform;
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "MovingPlatform")
            transform.parent = null;
    }

    public bool isFacingRight()
    {
        return facingRight;
    }


    private void Fire()
    {
        if (Time.time > fireRate + lastShot)
        {
            if (facingRight)
                Instantiate(throwingWeaponOne, transform.position + (Vector3.up * 1) + (Vector3.right * 2), Quaternion.identity);
            else
                Instantiate(throwingWeaponOne, transform.position + (Vector3.up * 1) + (Vector3.left * 2), Quaternion.identity);
            lastShot = Time.time;
        }

    }


}




