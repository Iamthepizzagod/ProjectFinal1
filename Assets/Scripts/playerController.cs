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

    private Rigidbody2D myRigidBody;

    private Animator myAnim;

    public Transform groundCheck;
    public float groundCheckRadius; //Radius of groundcheck
    public LayerMask whatIsGround; //what layer can the player jump
    public bool isGrounded; //Is the player grounded


    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
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
            StartCoroutine(ThrowingDelay());
            if(facingRight)
                Instantiate(throwingWeaponOne, transform.position * 1, Quaternion.identity);
            else
                Instantiate(throwingWeaponOne, transform.position * -1, Quaternion.identity);
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


    IEnumerator ThrowingDelay()
    {
        yield return new WaitForSeconds(2);
        yield return 0; 
    }
}




