using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private Rigidbody2D myRigidbody;

    private Animator myAnimator;

    [SerializeField]
    private float movementSpeed;

    private bool attack;

    private bool slide;

    private bool facingRight;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundpoints;
    private LayerMask whatIsGround;

	// Use this for initialization
	void Start ()
    {
        facingRight = true;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>(); 
	}
	
    void Update()
    {
        HandleInput();
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        
        HandleMovement(horizontal);

        Flip(horizontal);

        HandleAttacks();

        ResetValues();
    }

    private void HandleMovement(float horizontal)
    {
       if (!myAnimator.GetBool("slide")&& !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
       {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }
       else { 
          myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
       }

       if(slide && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            myAnimator.SetBool("slide", true);
        }
        else if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            myAnimator.SetBool("slide", false);
        }

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleAttacks()
    {
        if (attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            myAnimator.SetTrigger("attack");
            myRigidbody.velocity = Vector2.zero;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            slide = true;
        }
    }

    private void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    private void ResetValues()
    {
        attack = false;
        slide = false;
    }

    private bool IsGrounded()
    {
        if(myRigidbody.velocity.y <= 0)
        {
            foreach(Transform point in groundPoints)
            {
                Collider2D[] collider = Physics2D.OverlapCircleAll(point.position,)
            }
        }
    }
}
