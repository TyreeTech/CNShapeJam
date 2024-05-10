using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundaries
{
    // Serializable class with the borders limiting the player,
    // it probably doesn't need to be done like this but that's how I was taught -RG
    public float leftBorder, rightBorder, bottomBorder, topBorder;
}
public class PlayerControls : MonoBehaviour
{
    // variable initializations for player's speed, projectile speed,
    // how quickly the player can fire, and a delay before they can fire again
    public float speed;
    public float projectileForce;
    public float fireRate;
    public int currentLevel;
    private float nextFire;

    [SerializeField] private AudioSource powerupSFX;

    // playerHealth and score are specifically for the corresponding text objects,
    //gameOver is used as a null condition
    public int playerHealth;
    public int score;
    public bool gameOver = false;

    // variable initializations for a moveDirection vector (zeroed on init),
    // the Player, the projectile, and the boundary class
    private Vector2 moveDirection = Vector2.zero;
    public GameObject projectile;
    public Boundaries boundary;

    private Rigidbody myRigidBody;

    void Start() {
        // currentLevel and score are initialized with values on Awake
        currentLevel = 1;
        score = 0;
        myRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // moveDirection is a 2 dimensional vector
        // containing input data for the x and y axes
        // rigidbody is used to adjust player velocity
        // according to moveDirection and clamp the pos using info from Boundaries
        // as long as the game is running, the player can move
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (!gameOver) {
            myRigidBody.velocity = moveDirection * speed;
        }
        myRigidBody.position = new Vector2 (
            Mathf.Clamp(
                myRigidBody.position.x,
                boundary.leftBorder,
                boundary.rightBorder
                ),
            Mathf.Clamp(
                myRigidBody.position.y,
                boundary.bottomBorder,
                boundary.topBorder
                )
        );
    }

    void Update()
    {
        if (gameOver)
        {
            return;
        }
        // While the game is running,
        // if the player presses space or z,
        // and you can fire

        if ((Input.GetKey("space") || Input.GetKey("z")) && (Time.time > nextFire))
        {
            nextFire = Time.time + fireRate;

            /*****************************\
            |**** Add your code below ****|
            \*****************************/


            /*****************************\
            |**** Add your code above ****|
            \*****************************/
        }


        // If the playerHealth is reduced to 0,
        // the gameOver bool is set to true,
        // having various effects across multiple scripts
        if (playerHealth <= 0) {
            gameOver = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If the player is hit by anything that has the tag "Hazard",
        // the Hazard is destroyed
        // and the player health is reduced by 1
        if (other.gameObject.tag == "Hazard")
        {
            Debug.Log("Player hit!");
            Destroy(other.gameObject);
            playerHealth--;
        }
        if (other.gameObject.tag == "PowerUp")
        {
            Debug.Log("Play Sound!");
            powerupSFX.Play();
        }
    }
}
