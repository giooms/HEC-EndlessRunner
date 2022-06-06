using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    private float moveSpeedStore;
    public float speedMultiplier; //multiplicateur de vitesse

    public float speedIncreaseMilestone; //distance a partir de laquelle la vitesse augmente
    private float speedIncreaseMilestoneStore;
    
    private float speedMilestoneCount; // compteur de distance
    private float speedMilestoneCountStore;

    public float jumpForce;

    private Rigidbody2D myRigidbody;

    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius; // a quel point le radius du cercle est grand

    private Animator myAnimator;

    public GameManager theGameManager;

    //variables liees au score si sur tete:
    public float pointbonus; //combien de pts on gange apres saut sur tete
    public ScoreManager scoremanager;

    // Start is called before the first frame update
    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();

        //myCollider = GetComponent<Collider2D>();

        myAnimator = GetComponent<Animator>();

        speedMilestoneCount = speedIncreaseMilestone; // Pour que le milstone ne reste pas toujours au point 0

        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;

        scoremanager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {

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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "killbox")
        {
            theGameManager.RestartGame();
            moveSpeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;

            FindObjectOfType<ScoreManager>().HighScore();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "collidercorps")
        {
            theGameManager.RestartGame();
            moveSpeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;

            FindObjectOfType<ScoreManager>().HighScore();

        }
        if (other.gameObject.tag == "colliderhead")      // si le joueur touche la tete du mob, alors le mechant disparait et on gagne des points.
        {
            other.transform.parent.gameObject.SetActive(false); //ceci permet de recuperer le parent lie au collider touche et de le faire disparaitre
            pointbonus = 10;
            scoremanager.scoreCount += pointbonus;  //On fait le lien avec le score
        }
    }
}
