using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float speedMultiplier; //multiplicateur de vitesse

    public float speedIncreaseMilestone; //distance a partir de laquelle la vitesse augmente
    private float speedMilestoneCount; // compteur de distance

    public float jumpForce;

    private Rigidbody2D myRigidbody;

    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius; // a quel point le radius du cercle est grand

    //private Collider2D myCollider;

    private Animator myAnimator;

    // Start is called before the first frame update
    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();

        //myCollider = GetComponent<Collider2D>();

        myAnimator = GetComponent<Animator>();

        speedMilestoneCount = speedIncreaseMilestone; // Pour que le milstone ne reste pas toujours au point 0

    }

    // Update is called once per frame
    void Update()
    {

        //grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround); //si le cercle autour du groundcheck touche le sol alors grounded est vrai

        if (transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;

            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
            moveSpeed = moveSpeed * speedMultiplier;
            if(moveSpeed >= 15)
            {
                moveSpeed = 15;
            }
        } //si la position x du joueur est plus grande que que le compteur de distance

        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            }
        }

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);

    }
}
