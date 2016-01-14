using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float jumpHeight;

    private float timeToJumpApex = .4f;

    [SerializeField]
    private float moveSpeed;

    private float gravity;

    private float jumpVelocity;

    private Vector3 playerVelocity;

    private Controller2D myController;
    private Animator myAnimator;
    private bool canJump;
    private Torso torso;
    private LegObject leg;
    private Arms arm;

    void Start()
    {
        torso = GetComponent<Torso>();
        leg = GetComponent<LegObject>();
        arm = GetComponent<Arms>();
        myAnimator = GetComponent<Animator>();
        myController = GetComponent<Controller2D>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
    }

    void Update()
    {

        if (torso.hasTorso)
        {
            myAnimator.SetBool("hasTorso", true);
        }
        if (!torso.hasTorso)
        {
            myAnimator.SetBool("hasTorso", false);
        }
        if (leg.oneLeg)
        {
            myAnimator.SetBool("hasLeg", true);
        }
        if (arm.oneArm)
        {
            myAnimator.SetBool("hasArm", true);
        }
        if (leg.twoLegs)
        {
            myAnimator.SetBool("has2Legs", true);
            myAnimator.SetBool("hasLeg", false);
        }
        if (arm.twoArms)
        {
            myAnimator.SetBool("has2Arms", true);
            myAnimator.SetBool("hasArm", false);
        }




        if (!leg.oneLeg && myAnimator.GetBool("hasLeg"))
        {
            myAnimator.SetBool("hasLeg", false);
        }
        if (!arm.oneArm && myAnimator.GetBool("hasArm"))
        {
            myAnimator.SetBool("hasArm", false);
        }
        if (!leg.twoLegs && myAnimator.GetBool("has2Legs"))
        {
            myAnimator.SetBool("has2Legs", false);
        }
        if (!arm.twoArms && myAnimator.GetBool("has2Arms"))
        {
            myAnimator.SetBool("has2Arms", false);
        }




        HandleMovments();
        HandleInputs();
    }

    private void HandleMovments()
    {
        if (myController.collisions.above || myController.collisions.below)
        {
            playerVelocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //get input from the player (left and Right Keys)

        if (Input.GetKeyDown(KeyCode.Space) && myController.collisions.below)  //if spacebar is pressed, jump
        {
            playerVelocity.y = jumpVelocity;
        }
        playerVelocity.x = input.x * moveSpeed;

        playerVelocity.y += gravity * Time.deltaTime;
        myController.Move(playerVelocity * Time.deltaTime);
        //myAnimator.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));
    }

    private void HandleInputs()
    {

    }


}
